using System.Threading.Tasks;
using Dto;
using System.IO.IsolatedStorage;
using System.IO;

namespace Ui.Wp8.Infrastructure
{
    public class StatisticsRepository
    {
        private readonly DbProvider _dbProvider;

        public StatisticsRepository(DbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task<ClientStatistics> Fetch(string email)
        {
            // TODO: remove when finished
            await Task.Delay(1000);

            return new ClientStatistics
            {
                AgresivityRate = 90,
                Location = "Jilava",
                EmailAddress = "test@test.com",
            };
        }
        
        public async Task Persist(ClientStatistics statistics)
        {
            // TODO: remove when finished
            await Task.Delay(1500);

            var encoder = new XmlEncoder();
            var filename = _dbProvider.BuildDbName(statistics.EmailAddress);

            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(filename, FileMode.Create, store))
                {
                    await encoder.Encode(statistics, stream);
                }                  
            }
        }
    }
}