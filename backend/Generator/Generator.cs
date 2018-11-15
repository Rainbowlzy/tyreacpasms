using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Generator.Tools;
using Newtonsoft.Json;
using static System.Text.RegularExpressions.Regex;

namespace Generator
{
    public class Generator
    {
        private readonly string _ttGenerationFolder = @"TT_GENERATION_FOLDER\";

        public DateTime DomainVersionTime { get; set; }

        public Generator(string DOMAIN_FILE_PATH)
        {
            InitLocalDB();

            if (!File.Exists(DOMAIN_FILE_PATH)) throw new FileNotFoundException(DOMAIN_FILE_PATH);
            _ttGenerationFolder = Directory.GetParent(DOMAIN_FILE_PATH).FullName + _ttGenerationFolder;
            if (!Directory.Exists(_ttGenerationFolder)) Directory.CreateDirectory(_ttGenerationFolder);
            var input = File.ReadAllText(DOMAIN_FILE_PATH);
            DomainVersionTime = File.GetLastWriteTime(DOMAIN_FILE_PATH);

            var cache_table_schema2 =
                $"{_ttGenerationFolder}table_schema2_{DomainVersionTime.ToString("s").Replace("T","").Replace("-","").Replace(":","")}.txt";

            if (File.Exists(cache_table_schema2))
            {
                table_schema2 = File.ReadAllText(cache_table_schema2)
                    .Deserialize<Dictionary<string, VTableComments2>>();
                return;
            }

            var lines = input.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());

            var enumerable = lines as string[] ?? lines.ToArray();
            foreach (var line in enumerable.Where(s => !string.IsNullOrEmpty(s)
                                                       && s.IndexOf("=>", StringComparison.Ordinal) != -1)
                .AsParallel().Cast<string>())
            {
                var fieldName = line.Substring(line.IndexOf(".", StringComparison.Ordinal) + 1,
                        line.IndexOf("=>", StringComparison.Ordinal) - line.IndexOf(".", StringComparison.Ordinal) - 1)
                    .Trim();
                var fieldNameEn = PascalCase(fieldName).Trim();
                var tableName = line.Substring(0, line.IndexOf(".", StringComparison.Ordinal)).Trim();
                var tableNameEn = PascalCase(line.Substring(0, line.IndexOf(".", StringComparison.Ordinal)));
                var prefix = string.Concat(tableNameEn.Where(c => c >= 'A' && c <= 'Z')).Trim();
                var dd = new DropdownDef
                {
                    tableName = tableName,
                    tableNameEn = tableNameEn,
                    fieldName = fieldName,
                    fieldNameEn = prefix + fieldNameEn,
                    vals = Matches(
                            line.Substring(line.IndexOf("=>", StringComparison.Ordinal),
                                line.LastIndexOf("=>", StringComparison.Ordinal) -
                                line.IndexOf("=>", StringComparison.Ordinal)), @"\w+\d*").Cast<Match>()
                        .Select(p => p.Value).ToList(),
                    buttons = Matches(line.Substring(line.LastIndexOf("=>", StringComparison.Ordinal)), @"\w+\d*")
                        .Cast<Match>().Select(p => p.Value).ToList(),
                };
                dropdowns.Add(dd);
            }

            foreach (var line in enumerable.Where(s => !string.IsNullOrEmpty(s)
                                                       && s.IndexOf("->", StringComparison.Ordinal) != -1)
                .AsParallel().Cast<string>())
            {
                var rawChTableName = line.Substring(0, line.IndexOf("->", StringComparison.Ordinal));
                var pascalEnTableName = PascalCase(rawChTableName);
                var textFields = line.Substring(line.IndexOf("->", StringComparison.Ordinal) + 2);
                var fields =
                    new HashSet<string>(textFields.Split(new[] {'，', '\t'}, StringSplitOptions.RemoveEmptyEntries))
                        .ToList();
                var prefix = string.Concat(pascalEnTableName.Where(c => c >= 'A' && c <= 'Z'));
                var table = new VTableComments2
                {
                    table_name = pascalEnTableName,
                    table_name_ch = rawChTableName,
                    table_name_en = pascalEnTableName,
                    Columns = fields.Select(t =>
                    {
                        var pascal_column_name = PascalCase(t);
                        string regular_column_name = prefix + pascal_column_name;
                        string db_type = ParseType(t);
                        return new V_Column
                        {
                            pascal_column_name = pascal_column_name,
                            column_name = regular_column_name,
                            column_description = t,
                            dbtype = db_type
                        };
                    }).ToList()
                };
                if (!table_schema2.ContainsKey(pascalEnTableName))
                {
                    table_schema2.Add(pascalEnTableName, table);
                }
                else
                {
                    table_schema2[pascalEnTableName] = table;
                }
            }

            foreach (var t in table_schema2.Values)
            {
                foreach (var k in t.Columns.Where(k => k.column_description.StartsWith("@")))
                {
                    var column_en_name = k.pascal_column_name;
                    var key = column_en_name;
                    var column_name_chs = k.column_description.Trim('@');
                    var tbl = table_schema2.Values.FirstOrDefault(q => q.table_name_ch == column_name_chs);
                    if (tbl != null)
                    {
                        if (tbl.Children == null) tbl.Children = new List<VTableComments2>();
                        tbl.Children.Add((VTableComments2) t.Clone());
                        t.Parent = (VTableComments2) tbl.Clone();
                    }
                }
            }
            using (var ctx = new GeneratorContext())
            {
                if (ctx.TableSchema.Any())
                {
                    ctx.V_Column.RemoveRange(ctx.V_Column);
                    ctx.TableSchema.RemoveRange(ctx.TableSchema);
                    ctx.SaveChanges();
                }
                ctx.TableSchema.AddRange(table_schema2.Values.ToList());
                ctx.SaveChanges();
            }

            File.WriteAllText(cache_table_schema2, table_schema2.ToJson());
        }

        private static void InitLocalDB()
        {
            try
            {
                using (var ctx = new GeneratorContext())
                {
                    if (ctx.TypePatterns.Any()) return;
                    ctx.TypePatterns.AddRange(DefaultTypePatterns());
                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception("本地数据库初始化失败", e);
            }
        }

        /// <summary>
        /// 默认类型模式
        /// </summary>
        /// <returns></returns>
        private static TypePattern[] DefaultTypePatterns()
        {
            return new[]
            {
                new TypePattern
                {
                    Length = 50,
                    RegexPattern =
                        @"(图片)|(照片)|(上传)|(摘要)|(备注)|(报告)|(头像)|(内容)|(标题)|(地址)|(企业名称)|(身份证)|(链接)|(理由)|(原因)|(说明)",
                    DbType = "NVARCHAR"
                },
                new TypePattern
                {
                    RegexPattern = @"(是否)|(开关)", DbType = "BIT"
                },
                new TypePattern
                {
                    RegexPattern = @"(金额)|(\w*租金)", DbType = "CURRENCY"
                },
                new TypePattern
                {
                    RegexPattern = @"(时间)|(日期)", DbType = "DATETIME"
                },
                new TypePattern
                {
                    RegexPattern = @"(小数)|(金额)|(\w*租金)|(面积)", DbType = "DECIMAL"
                },
                new TypePattern
                {
                    RegexPattern = @"(id)|(年龄)|(顺序)|(排序)", DbType = "INT"
                },
                new TypePattern
                {
                    Length = 4000, RegexPattern = @"(备注)|(内容)|(正文)", DbType = "TEXT"
                },

            };
        }

        private List<DropdownDef> dropdowns { get; set; } = new List<DropdownDef>();

        public Dictionary<string, VTableComments2> table_schema2 { get; set; } =
            new Dictionary<string, VTableComments2>();

        private string ParseType(string field)
        {
            var mapping = new
            {
                nvarchar = @"(图片)|(照片)|(上传)|(摘要)|(备注)|(报告)|(头像)|(内容)|(标题)|(地址)|(企业名称)|(身份证)|(链接)|(理由)|(原因)|(说明)",
                @int = @"(id)|(年龄)|(顺序)|(排序)|(工号)|(编号)|(容量)|(数量)",
                @decimal = @"小数",
                @real = @"面积",
                money = @"(金额)|(\w*租金)",
                text = @"(备注)|(内容)|(正文)",
                datetime = @"(时间)|(日期)"
            };
            return IsMatch(field, mapping.nvarchar) ? "NVARCHAR(50)" :
                IsMatch(field, mapping.@int) ? "INT" :
                IsMatch(field, mapping.@decimal) ? "DECIMAL" :
                IsMatch(field, mapping.real) ? "REAL" :
                IsMatch(field, mapping.money) ? "MONEY" :
                IsMatch(field, mapping.text) ? "TEXT" :
                IsMatch(field, mapping.datetime) ? "DATETIME" : "NVARCHAR(50)";
        }

        private static readonly string nottranslate = "PM2.5，PM10，SO2，NO2，O3，CO";

        private HttpWebResponse CreateGetHttpResponse(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            return request.GetResponse() as HttpWebResponse;
        }

        private string CamelCase(string key)
        {
            if (nottranslate.IndexOf(key, StringComparison.Ordinal) != -1) return key;
            return HttpGet($"http://localhost/Translator/api/CamelCase?key={key}").Trim('"');
        }

        private string PascalCase(string key)
        {
            key = key?.Trim("@,?.|\\/".ToCharArray());
            if (nottranslate.IndexOf(key, StringComparison.Ordinal) != -1) return key;
            return HttpGet($"http://localhost/Translator/api/PascalCase?key={key}")
                .Trim('"', '.', '-', '+', ',', '\'', '?', '(', ')')
                .Replace("'", string.Empty)
                .Replace('(', '\0')
                .Replace(')', '\0');
        }

        private string HttpGet(string url)
        {
            try
            {
                Stream responseStream = CreateGetHttpResponse(url).GetResponseStream();
                string buf = string.Empty;
                if (responseStream != null)
                {
                    using (StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8")))
                    {
                        buf = reader.ReadToEnd();
                    }

                    responseStream.Close();
                }

                return buf;
            }
            catch (Exception e)
            {
                File.WriteAllText(
                    $"D:\\log\\Generator_Err_{DateTime.Now.ToString("yyyy MMMM dd")}_{DateTime.Now.Ticks}.log",
                    JsonConvert.SerializeObject(new
                    {
                        project = "Generator",
                        method = nameof(HttpGet),
                        url,
                        e
                    }));
                return string.Empty;
            }
        }
    }
}