namespace Ui.Wp8.Infrastructure
{
    public interface IAgresivityCalculator
    {
        void AddAcceleration(Acceleration acceleration);
        void AddGpsData(GpsData gpsData);
        int Agresivity();
    }
}