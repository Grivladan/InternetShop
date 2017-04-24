using System.Data.Entity;

namespace DataAccess.EF
{
    class DbInitializer :  DropCreateDatabaseAlways<ApplicationDbContext>
    {
    }
}
