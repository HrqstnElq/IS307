using IS307.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        public AccountPage()
        {
            InitializeComponent();
            var vm = new AccountViewModel(Navigation);
            BindingContext = vm;
        }
    }
}