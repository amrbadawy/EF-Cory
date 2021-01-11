using System.Threading.Tasks;
using UPD.EntityFramework;

namespace EFCory
{
    public abstract class AppService
    {
        protected readonly DatabaseContext _db;
        public AppService(DatabaseContext databaseContext)
        {
            _db = databaseContext;
        }

        protected async Task SaveChangesAsync()
        {
            _db.DisplayChanges();
            await _db.SaveChangesAsync();
        }
    }
}