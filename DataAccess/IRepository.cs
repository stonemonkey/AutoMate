using System.Collections.Generic;

namespace DataAccess
{
    public interface IRepository
    {
        void AddUserStatistics(UserStatistic userStatistics);

        IEnumerable<UserStatistic> GetUsersStatistisc();
    }
}
