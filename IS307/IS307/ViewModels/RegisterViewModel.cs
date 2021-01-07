using IS307.Models;
using IS307.Services;
using IS307.Views;
using System.Windows.Input;
using Xamarin.Forms;


namespace IS307.ViewModels
{
    public class RegisterViewModel
    {
        public RegisterModel RegisterModel { get; set; }

        public ICommand ShowLogin { get; set; }
        public ICommand Register { get; set; }

        public RegisterViewModel(INavigation navigation)
        {
            RegisterModel = new RegisterModel()
            {
                Name = "",
                Username = "",
                Password = "",
                PasswordConfirm = ""
            };

            ShowLogin = new Command(() =>
            {
                Shell.Current.GoToAsync("//LoginPage").Wait();
            });

            Register = new Command<RegisterModel>((data) =>
            {
                if(string.IsNullOrEmpty(data.Name) || string.IsNullOrEmpty(data.Username) ||
                    string.IsNullOrEmpty(data.Password) || string.IsNullOrEmpty(data.PasswordConfirm)
                )
                {
                    Application.Current.MainPage.DisplayAlert("Waring !", "Required value", "Ok");
                }
                else
                {
                    var result = AccountService.RegisterService(data);
                    if(result == 1)
                    {
                        Application.Current.MainPage.DisplayAlert("Waring !", "Username already exists", "Ok");
                    }
                    else if(result == -1)
                    {
                        Application.Current.MainPage.DisplayAlert("Error !", "Bad request", "Ok");
                    }
                    else
                    {
                        Shell.Current.GoToAsync("//HomePage").Wait();
                    }
                }
            });
        }
    }
}
