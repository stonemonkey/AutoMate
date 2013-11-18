using Dto;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using Ui.Wp8.Components.MainPage;
using Windows.Devices.Sensors;

namespace Ui.Wp8.Infrastructure
{
    public class AgresivityCalculator: IAgresivityCalculator
    {
        private const double SpeedLimit = 36;
        private const double MaxAcceleration = 2;
        private const int AgresivityDecreaseAmount = 1;

        private readonly StatisticsRepository _repository;
        private readonly UserContextViewModel _userContext;

        private readonly List<AccelerometerReading> _accelerations;

        private readonly TimeSpan _accelerationCheckInterval = TimeSpan.FromSeconds(3);
        private readonly TimeSpan _timeRequiredToDecreaseAgresivity = TimeSpan.FromMinutes(10);
        private DateTime _lastIncidentDate;

        public int Agresivity { get; private set; }

        public AgresivityCalculator(StatisticsRepository repository, UserContextViewModel userContext)
        {
            _accelerations = new List<AccelerometerReading>();
            _repository = repository;
            _userContext = userContext; 
        }

        public void AddGpsData(GeoCoordinate geoCoordinate)
        {
            if (geoCoordinate.Speed < SpeedLimit)
            {
                DecreaseAgresivityIfDrivingGoodForRequiredAmountOfTime();
                return;
            }

            Agresivity += GetSpeedingPenalty();
            _lastIncidentDate = DateTime.Now;
            PersistData();
        }

        public void AddAcceleration(AccelerometerReading acceleration)
        {
            if (AbsoluteValue(acceleration) < MaxAcceleration)
            {
                DecreaseAgresivityIfDrivingGoodForRequiredAmountOfTime();
                return;
            }

            _accelerations.Add(acceleration);
            if (IsEraticAccelerationPeriodExceeded())
            {
                Agresivity += GetEraticAccelerationPenalty();
                _accelerations.Clear();
                _lastIncidentDate = DateTime.Now;
                PersistData();
            }
        }

        private static double AbsoluteValue(AccelerometerReading acceleration)
        {
            return acceleration.AccelerationX * acceleration.AccelerationX + 
                acceleration.AccelerationY * acceleration.AccelerationY + 
                acceleration.AccelerationZ * acceleration.AccelerationZ;
        }

        private bool IsEraticAccelerationPeriodExceeded()
        {
            return _accelerations.Last().Timestamp - _accelerations.First().Timestamp >= _accelerationCheckInterval;
        }

        private bool IsGoodDrivingPeriodReached()
        {
            return DateTime.Now - _lastIncidentDate > _timeRequiredToDecreaseAgresivity;
        }

        private static int GetEraticAccelerationPenalty()
        {
            return 10;
        }

        private static int GetSpeedingPenalty()
        {
            return 10;
        }

        private void DecreaseAgresivityIfDrivingGoodForRequiredAmountOfTime()
        {
            if (IsGoodDrivingPeriodReached())
            {
                Agresivity -= AgresivityDecreaseAmount;
            }
        }

        private async void PersistData()
        {
            Agresivity = (int) Math.Abs(Agresivity);
            if (Agresivity <= 0)
                Agresivity = 1;
            if (Agresivity > 100)
                Agresivity = 100;

            var clientStatistics = new ClientStatistics
            {
                EmailAddress = _userContext.Email,
                Location = _userContext.Location,
                AgresivityRate = Agresivity,
            };

            await _repository.Persist(clientStatistics);
        }        
    }
}