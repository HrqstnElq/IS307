using IS307.Models;
using IS307.Services;
using IS307.Views;
using System.Windows.Input;
using Xamarin.Forms;


namespace IS307.ViewModels
{
    public class LoginViewModel
    {
        public LoginModel LoginData { get; set; }

        public ICommand ShowRegister { get; set; }
        public ICommand Login { get; set; }

        public LoginViewModel(INavigation navigation)
        {
            LoginData = new LoginModel()
            {
                Username = "",
                Password = ""
            };

            ShowRegister = new Command(() =>
            {
                Shell.Current.GoToAsync("//RegisterPage").Wait();
            });

            Login = new Command<LoginModel>(data =>
            {
                if (string.IsNullOrEmpty(data.Username) || string.IsNullOrEmpty(data.Password))
                {
                    Application.Current.MainPage.DisplayAlert("Waring !", "Username or password is required", "OK");
                }
                else
                {
                    var result = AccountService.LoginService(data);
                    if (result)
                    {
                        Shell.Current.GoToAsync("//HomePage").Wait();
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Error !", "Login fail", "Again");
                    };
                }
            });
        }
    }
}
