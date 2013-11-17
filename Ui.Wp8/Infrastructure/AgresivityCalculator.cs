using Dto;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ui.Wp8.Components.MainPage;
using Windows.Devices.Sensors;

namespace Ui.Wp8.Infrastructure
{
    public class AgresivityCalculator: IAgresivityCalculator
    {
        public int Agresivity { get; private set; }

        StatisticsRepository _repository;
        UserContextViewModel _userContext;  

        List<AccelerometerReading> _accelerations;

        private readonly double _maxAcceleration = 2;
        private readonly TimeSpan _accelerationCheckInterval = TimeSpan.FromSeconds(5);
        private readonly double _speedLimit = 36;
        private readonly TimeSpan _timeRequiredToDecreaseAgresivity = TimeSpan.FromMinutes(10);
        private readonly int _agresivityDecreaseAmount = 1;

        public AgresivityCalculator(StatisticsRepository repository, UserContextViewModel userContext)
        {
            _accelerations = new List<AccelerometerReading>();
            _repository = repository;
            _userContext = userContext; 
        }

        public void AddGpsData(GeoCoordinate geoCoordinate)
        {
            if (geoCoordinate.Speed > _speedLimit)
            {
                Agresivity += GetSpeedingPenalty(geoCoordinate.Speed);
                PersistData();
            }
        }

        public void AddAcceleration(AccelerometerReading acceleration)
        {
            if (AbsoluteValue(acceleration) < _maxAcceleration)
            {
                return;
            }

            _accelerations.Add(acceleration);
            if (IsEraticAccelerationPeriodExceeded())
            {
                Agresivity += GetEraticAccelerationPenalty(acceleration);
                _accelerations.Clear();
                PersistData();
            }
        }

        private double AbsoluteValue(AccelerometerReading acceleration)
        {
            return acceleration.AccelerationX * acceleration.AccelerationX + 
                acceleration.AccelerationY * acceleration.AccelerationY + 
                acceleration.AccelerationZ * acceleration.AccelerationZ;
        }

        private bool IsEraticAccelerationPeriodExceeded()
        {
            return _accelerations.First().Timestamp - _accelerations.Last().Timestamp >= _accelerationCheckInterval;
        }

        private int GetEraticAccelerationPenalty(AccelerometerReading acceleration)
        {
            return 10;
        }

        private int GetSpeedingPenalty(double speed)
        {
            return 10;
        }

        private async void PersistData(){
            var clientStatistics = new ClientStatistics
            {
                EmailAddress = _userContext.Email,
                Location = _userContext.Location,
                AgresivityRate = Agresivity
            };

            await _repository.Persist(clientStatistics);
        }
    }
}