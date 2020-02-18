using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenBjones.Aandeelbeheer.Data
{
    public class AandeelbeheerContext : DbContext
    {
        public AandeelbeheerContext() : base("dbStevenBjonesAandeelbeheer")
        {
            //2 verschillende database initialators.
            Database.SetInitializer(new AandeelbeheerContextDropCreateDatabaseAlways());
            //Database.SetInitializer(new AandeelbeheerContextDropCreateDatabaseIfModelChanges());
        }

        //De database sets
        public DbSet<Portefeuille> Portefeuilles { get; set; }
        public DbSet<Bedrijf> Bedrijven { get; set; }
        public DbSet<Aandeel> Aandelen { get; set; }
    }
}
 