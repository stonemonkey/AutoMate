using System.Data.Entity;

namespace DataAccess.Ef
{
    public class DataContext : DbContext
    {
        public DbSet<UserStatistic> UserStatistics { get; set; }

        public DataContext()
        {
            Database.SetInitializer<DataContext>(null);
        }
    }
}
