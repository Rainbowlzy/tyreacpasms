using System.Data.Entity.Migrations;
using TEntities.EF;

namespace TENtities.EF
{
    public class Configuration: DbMigrationsConfiguration<DefaultContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}
