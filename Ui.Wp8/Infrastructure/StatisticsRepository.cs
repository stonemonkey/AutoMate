using Dto;

namespace Ui.Wp8.Infrastructure
{
    public class StatisticsRepository
    {
        private readonly DbProvider _dbProvider;

        public StatisticsRepository(DbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public ClientStatistics Fetch(string email)
        {
            return new ClientStatistics
            {
                AgresivityRate = 90,
                Location = "Jilava",
                EmailAddress = "test@test.com",
            };
        }
        
        public void Persist(ClientStatistics statistics)
        {
        }
    }
}