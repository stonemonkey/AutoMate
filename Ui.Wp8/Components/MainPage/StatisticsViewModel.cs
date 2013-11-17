using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using Dto;
using Ui.Wp8.Infrastructure;

namespace Ui.Wp8.Components.MainPage
{
    public class StatisticsViewModel : Screen
    {
        private readonly StatisticsRepository _statisticsRepository;
        private readonly UserContextViewModel _userContext;
        private ClientStatistics _data;
        private bool _isUnsent;

        public bool IsUnsent
        {
            get { return _isUnsent; } 
            private set
            {
                _isUnsent = value;
                NotifyOfPropertyChange(() => IsUnsent);
            }
        }

        public ClientStatistics Data
        {
            get { return _data; }
            private set
            {
                IsUnsent = true;

                _data = value;
                NotifyOfPropertyChange(() => Data);
            }
        }

        public StatisticsViewModel(StatisticsRepository statisticsRepository, UserContextViewModel userContext)
        {   
            _statisticsRepository = statisticsRepository;
            _userContext = userContext;
        }

        public void MarkAsSent()
        {
            IsUnsent = false;
        }

        public async Task Initialize()
        {
            Data = await _statisticsRepository.Fetch(_userContext.Email) ?? DefaultStatisctics();
        }

        private ClientStatistics DefaultStatisctics()
        {
            return new ClientStatistics
            {
                AgresivityRate = 1,
                Location = _userContext.Location,
                EmailAddress = _userContext.Email,
                LastModified = DateTime.Now,
            };
        }
    }
}