using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        public ICommand Logout { get; set; }

        public AccountViewModel(INavigation navigation)
        {
            Logout = new Command(async () =>
            {
                OnPropertyChanged("Loading");
                await Task.Delay(500);
                App.Current.Properties["token"] = null;
                await Shell.Current.GoToAsync("//LoginPage");
                OnPropertyChanged("Complete");
            });
        }
    }
}