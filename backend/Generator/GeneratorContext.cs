using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Generator
{
    public class GeneratorContext : DbContext
    {
        private class Configuration : DbMigrationsConfiguration<GeneratorContext>
        {
            public Configuration()
            {
                AutomaticMigrationsEnabled = true;
                AutomaticMigrationDataLossAllowed = true;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypePattern>().Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<GeneratorContext, Configuration>());
        }

        public GeneratorContext() : base("server=.;database=GeneratorDB;integrated security=true;")
        {
        }

        public DbSet<VTableComments2> TableSchema { get; set; }
        public DbSet<TypePattern> TypePatterns { get; set; }
    }
}