using System.Data.Entity.Migrations;

namespace BAuth.DAL.Migration
{

    internal sealed class Configuration : DbMigrationsConfiguration<BAuthDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BAuthDBContext context)
        {
            base.Seed(context);

        }
    }
}
