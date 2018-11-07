using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace watchdog
{
    public class Word
    {
        public string Line { get; set; }
        public List<string> English { get; set; }
        public List<string> Chinese { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static bool IsEn(string key)
        {
            return key[0] >= 'A' && key[0] <= 'z';
        }

        public static Word ParseToWord(string line)
        {
            var arr = line
                .Split(new[] {'\t', ' ', '，'}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Split(new char[] {'.'}, StringSplitOptions.RemoveEmptyEntries).LastOrDefault());
            return new Word
            {
                Chinese = arr.Where(t => !IsEn(t)).ToList(),
                English = arr.Where(IsEn).ToList(),
                Line = line,
            };
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const string path = @"C:\Users\Administrator\Desktop\Book1.xls";

            var book = ParseExcelToArray(path);

            Console.WriteLine(string.Join(Environment.NewLine, book.SelectMany(t => t).Select(JsonConvert.SerializeObject)));
            Console.ReadLine();
        }

        private static object[][][] ParseExcelToArray(string path)
        {
            object[][][] book = null;
            using (FileStream fs = File.OpenRead(path))
            {
                HSSFWorkbook workbook = new HSSFWorkbook(fs);
                book = new object[workbook.NumberOfSheets][][];
                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    var sheet = workbook.GetSheetAt(i);
                    book[i] = new object[sheet.PhysicalNumberOfRows][];
                    for (int j = 0; j < sheet.PhysicalNumberOfRows; j++)
                    {
                        var row = sheet.GetRow(j);
                        book[i][j] = new object[row.PhysicalNumberOfCells];
                        for (int k = 0; k < row.PhysicalNumberOfCells; k++)
                        {
                            var cell = row.GetCell(k);
                            switch (cell.CellType)
                            {
                                case CellType.Numeric:
                                    book[i][j][k] = cell.NumericCellValue;
                                    break;
                                case CellType.String:
                                    book[i][j][k] = cell.StringCellValue;
                                    break;
                            }
                        }
                    }
                }
            }
            return book;
        }

        private static void TransferDictionary()
        {
            const string path = @"F:\code\Translator\《牛津英汉词典》.txt";
            var newLine = Environment.NewLine;
            var all = File.ReadAllLines(path, Encoding.GetEncoding("GBK"));
            var len = 100;
            var raw = string.Join(newLine, all
                .Skip(all.Length - len)
                .Take(len));
            var lines = raw.Split(newLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var words = lines.Select(Word.ParseToWord);
            var dic = new Dictionary<string, Word>();
            foreach (Word word in words)
            {
                foreach (var chs in word.Chinese)
                {
                    if (dic.ContainsKey(chs))
                    {
                        word.Line += dic[chs].Line;
                        dic[chs].English.AddRange(word.English);
                    }
                    else
                    {
                        dic.Add(chs, word);
                    }
                }

                foreach (var en in word.English)
                {
                    if (dic.ContainsKey(en))
                    {
                        word.Line += dic[en].Line;
                        dic[en].Chinese.AddRange(word.Chinese);
                    }
                    else
                    {
                        dic.Add(en, word);
                    }
                }
            }

            raw = string.Join(newLine, dic.Select(t => JsonConvert.SerializeObject(t)));
            var result = raw;
            Console.WriteLine(result);
        }
    }
}