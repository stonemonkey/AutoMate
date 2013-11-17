using Dto;

namespace Ui.Wp8.Infrastructure
{
    public interface IClientStatisticsEncoder
    {
        string ContentType { get; }
        string Encode(ClientStatistics clientStatistics);
    }
}