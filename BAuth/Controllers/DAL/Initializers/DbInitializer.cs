using System.Data.Entity;

namespace BAuth.DAL.Initializers
{
    internal class DbInitializer: MigrateDatabaseToLatestVersion<BAuthDBContext, BAuth.DAL.Migration.Configuration>
	{
	}
}
