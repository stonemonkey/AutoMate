using Dto;

namespace Ui.Wp8.Infrastructure
{
    public class StatisticsRepository
    {
         public ClientStatistics Fetch(string email)
         {
             return new ClientStatistics
             {
                 AgresivityRate = 90,
                 Location = "Jilava",
                 EmailAddress = "test@test.com",
             };
         }
    }
}