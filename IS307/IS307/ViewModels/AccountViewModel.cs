using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.ViewModels
{
    public class AccountViewModel
    {
        public ICommand Logout { get; set; }

        public AccountViewModel(INavigation navigation)
        {
            Logout = new Command(() =>
            {
                App.Current.Properties["token"] = null;
                Shell.Current.GoToAsync("//LoginPage");
            });
        }
    }
}