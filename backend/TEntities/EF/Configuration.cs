using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace TEntities.EF
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
