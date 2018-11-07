using EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEntities.EF;

namespace SetData
{
    class Program
    {
        private static void Create()
        {
            try
            {
                using (var ctx = new DefaultContext())
                {
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception?.Message);
                Console.WriteLine(exception?.StackTrace);
                Console.WriteLine(exception?.InnerException?.Message);
                Console.WriteLine(exception?.InnerException?.StackTrace);
                Console.ReadLine();
            }
        }

        static void Main(string[] args)
        {
            Create();
        }
    }
}