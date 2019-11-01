using ConvetPdfToLayoutAlta.FluentApi;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ConvetPdfToLayoutAlta.Models
{
    public class DbConnEntity: DbContext
    {
        public DbConnEntity():base("strCnn")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DbConnEntity>());
        }

        public DbSet<Parcela> Parcelas { get; set; }
        public DbSet<HistoricoParcela> HistoricoParcelas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FluentApiParcelas());
            modelBuilder.Configurations.Add(new FluentApiHistoricoParcelas());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder); 
        }
    }
}
