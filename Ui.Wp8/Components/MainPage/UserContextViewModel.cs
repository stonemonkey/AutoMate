using Caliburn.Micro;

namespace Ui.Wp8.Components.MainPage
{
    public class UserContextViewModel : Screen
    {
        public string Email { get; set; }

        public string Location { get; set; }

        public UserContextViewModel()
        {
            Email = "test@test.com";
            Location = "Selimbar";
        }
    }
}