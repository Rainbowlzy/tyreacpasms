using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEntities.EF;
using TENtities.EF;

namespace SetData
{
    class Program
    {
        private static void Run()
        {
            try
            {
                using (var ctx = new DefaultContext())
                {
                    var transactionId = Guid.NewGuid().ToString();
                    ctx.UserInformation.AddRange(Enumerable.Range(1000, 1050).Select(i => new UserInformation
                    {
                        id = i,
                        UILoginName = i.ToString(),
                        UICode = "123",
                        IsDeleted = 0,
                        TransactionID = transactionId,
                        CreateBy = "JOB",
                        UpdateBy = "JOB",
                        CreateOn = DateTime.Now,
                        UpdateOn = DateTime.Now
                    }));
                    ctx.SaveChanges();
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException exception)
            {
                Console.WriteLine(string.Join("\r\n",
                    exception.EntityValidationErrors.SelectMany(e =>
                        e.ValidationErrors.Select(v => $"{v.PropertyName} {v.ErrorMessage}\r\n"))));
            }

            Console.ReadLine();
        }

        private static void Create()
        {
#if DEBUG
            Run();
#else
            try
            {
                Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception?.Message);
                Console.WriteLine(exception?.StackTrace);
                Console.WriteLine(exception?.InnerException?.Message);
                Console.WriteLine(exception?.InnerException?.StackTrace);
                Console.ReadLine();
            }
#endif
        }

        static void Main(string[] args)
        {
            Create();
        }
    }
}