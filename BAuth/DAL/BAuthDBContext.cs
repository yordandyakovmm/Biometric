using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BAuth.DAL
{
	
    public class BAuthDBContext : DbContext
    {

        public BAuthDBContext() : base("BAuthConnectionString")
        {
            Database.SetInitializer<BAuthDBContext>(null);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }

        //public DbSet<Form> Forms { get; set; }
        //public DbSet<Measure> Measures { get; set; }
        //public DbSet<Study> Studys { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

}
