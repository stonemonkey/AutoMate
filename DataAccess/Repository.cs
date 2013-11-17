using System.Collections.Generic;

namespace DataAccess
{
    public class Repository : IRepository
    {
        private readonly DataContext dataContext;

        public Repository()
        {
            dataContext = new DataContext();
        }

        public void AddUserStatistics(UserStatistic userStatistics)
        {
            dataContext.UserStatistics.Add(userStatistics);
            dataContext.SaveChanges();
        }

        public IEnumerable<UserStatistic> GetUsersStatistisc()
        {
            return dataContext.UserStatistics;
        }
    }
}
