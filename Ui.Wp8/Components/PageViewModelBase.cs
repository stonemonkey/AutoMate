using Caliburn.Micro;

namespace Ui.Wp8.Components
{
    public class PageViewModelBase : Screen
    {
        protected readonly INavigationService NavigationService;

        public PageViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        /// <summary>
        /// Override it when you want to do something before going back to calling page.
        /// </summary>
        public virtual void OnGoBack()
        {
        }
    }
}