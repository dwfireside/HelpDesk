using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Shared.System
{
    public class AppContext : DbContext
    {
        public AppContext() { }

        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
    }
}
