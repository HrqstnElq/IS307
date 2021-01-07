using IS307.Models;
using IS307.Services;
using System.Windows.Input;
using Xamarin.Forms;


namespace IS307.ViewModels
{
    public class RegisterViewModel
    {
        public RegisterModel RegisterModel { get; set; }

        public ICommand ShowLogin { get; set; }
        public ICommand Register { get; set; }

        private readonly AccountService AccountService;

        public RegisterViewModel(INavigation navigation)
        {
            AccountService = new AccountService();

            RegisterModel = new RegisterModel()
            {
                name = "",
                username = "",
                password = "",
                passwordConfirm = ""
            };

            ShowLogin = new Command(async () =>
            {
                await Shell.Current.GoToAsync("//LoginPage");
            });

            Register = new Command<RegisterModel>(async (data) =>
            {
                if (string.IsNullOrEmpty(data.name) || string.IsNullOrEmpty(data.username) ||
                    string.IsNullOrEmpty(data.password) || string.IsNullOrEmpty(data.passwordConfirm)
                )
                {
                    await Application.Current.MainPage.DisplayAlert("Waring !", "Required value", "Ok");
                }
                else if (data.passwordConfirm != data.password)
                {
                    await Application.Current.MainPage.DisplayAlert("Waring !", "Password and password confirm not same", "Ok");
                }
                else
                {
                    var result = await AccountService.RegisterService(data);
                    if (result == 1)
                    {
                        await Application.Current.MainPage.DisplayAlert("Waring !", "Username already exists", "Ok");
                    }
                    else if (result == -1)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error !", "Bad request", "Ok");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Success !", "Register successfully", "Login");
                        await Shell.Current.GoToAsync("//LoginPage");
                    }
                }
            });
        }
    }
}
