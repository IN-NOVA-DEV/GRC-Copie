using grc_copie.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace grc_copie.Data
{
    public class GRC_Context : DbContext

    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Sexe> Sexes { get; set; }
        public DbSet<Job> Jobs { get; set; }

        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Resume> Resumes { get; set; }


        public GRC_Context(DbContextOptions<GRC_Context> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(e =>
            {
                e.HasKey(e => e.PersonId);  
               
            });

            modelBuilder.Entity<Job>(e =>
            {
                e.HasKey(e => e.JobId);
                e.HasOne(e => e.Person)
                .WithMany(e => e.Jobs)
                .HasForeignKey(e => e.PersonId);
            });
        }
        public string GetDatabaseStructureAsJson()
        {
            var tables = Model.GetEntityTypes()
                .Where(t => t.BaseType == null) // Sélectionne uniquement les tables de base
                .Select(t => new
                {
                    TableName = t.GetTableName(),
                    Columns = t.GetProperties()
                        .Select(p => new
                        {
                            ColumnName = p.GetColumnName(),
                            DataType = p.GetColumnType(),
                            IsNullable = p.IsNullable,
                            // Autres informations sur la colonne...
                        }),
                    // Autres informations sur la table...
                })
                .ToList();

            return JsonConvert.SerializeObject(tables, Formatting.Indented);
        }
    }
}
