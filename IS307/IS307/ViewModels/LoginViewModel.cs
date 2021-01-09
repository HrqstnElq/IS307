using IS307.Models;
using IS307.Services;
using IS307.Views;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;


namespace IS307.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public LoginModel LoginData { get; set; }

        public ICommand ShowRegister { get; set; }
        public ICommand Login { get; set; }

        private readonly AccountService AccountService;

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel(INavigation navigation)
        {
            AccountService = new AccountService();

            LoginData = new LoginModel()
            {
                username = "",
                password = ""
            };

            ShowRegister = new Command(async () =>
            {
                await navigation.PushAsync(new RegisterPage());
            });

            Login = new Command<LoginModel>(async (data) =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Loading"));
                if (string.IsNullOrEmpty(data.username) || string.IsNullOrEmpty(data.password))
                {
                    await Application.Current.MainPage.DisplayAlert("Waring !", "Username or password is required", "OK");
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Complete"));
                }
                else
                {
                    var token = await AccountService.LoginService(data);
                    if (string.IsNullOrEmpty(token))
                    {
                        await Application.Current.MainPage.DisplayAlert("Error !", "Login fail", "Again");
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Complete"));
                    }
                    else
                    {
                        App.Current.Properties["token"] = token;
                        App.Current.MainPage = new AppShell();
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Complete"));
                    };
                }
            });
        }
    }
}
