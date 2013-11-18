using System.Collections.Generic;

namespace DataAccess.Ef
{
    public interface IRepository
    {
        void AddUserStatistics(UserStatistic userStatistics);

        IEnumerable<UserStatistic> GetUsersStatistisc();
    }
}
