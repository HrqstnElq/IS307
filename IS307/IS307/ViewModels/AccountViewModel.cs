using IS307.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.ViewModels
{
    public class AccountViewModel
    {
        public ICommand Logout { get; set; }
        public ICommand FavoritePage { get; set; }
        public ICommand OrderPage { get; set; }
        public AccountViewModel(INavigation navigation)
        {
            Logout = new Command(() =>
            {
                App.Current.Properties["token"] = null;
                Shell.Current.GoToAsync("//LoginPage");
            });

            FavoritePage = new Command(() =>
            {
                navigation.PushAsync(new FavoritePage());
            });

            OrderPage = new Command(() =>
            {
                navigation.PushAsync(new OrderPage());
            });
        }
    }
}
