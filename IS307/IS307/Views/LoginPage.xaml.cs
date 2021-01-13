using IS307.ViewModels;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
            (BindingContext as INotifyPropertyChanged).PropertyChanged += LoginPage_PropertyChanged; ;
        }

        private void LoginPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Loading")
            {
                Loading.IsVisible = true;
            }
            else if (e.PropertyName == "Complete")
            {
                Loading.IsVisible = false;
            }
        }
    }
}