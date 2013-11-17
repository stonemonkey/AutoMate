using System;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Dto;
using System.IO.IsolatedStorage;
using System.IO;

namespace Ui.Wp8.Infrastructure
{
    public class StatisticsRepository
    {
        private readonly DbProvider _dbProvider;
        private readonly Random _random = new Random();

        public StatisticsRepository(DbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task<ClientStatistics> Fetch(string email)
        {
            var filename = _dbProvider.BuildDbName(email);

            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!store.FileExists(filename))
                {
                    return null;
                }

                var reader = new StreamReader(store.OpenFile(filename, FileMode.Open, FileAccess.Read));
                
                var serializer = new XmlSerializer(typeof (ClientStatistics));
                
                return await new Task<ClientStatistics>(() =>
                {
                    var statistics = (ClientStatistics) serializer.Deserialize(reader);
                    
                    reader.Close();
                    
                    return statistics;
                });

            }
        }
        
        public async Task Persist(ClientStatistics statistics)
        {
            var encoder = new XmlEncoder();
            var filename = _dbProvider.BuildDbName(statistics.EmailAddress);

            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var writer = new StreamWriter(store.OpenFile(filename, FileMode.CreateNew, FileAccess.Write)))
                {
                    await writer.WriteAsync(encoder.Encode(statistics));                        
                }
            }
        }
    }
}