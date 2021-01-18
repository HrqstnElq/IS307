using IS307.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new RegisterViewModel(Navigation);
            (BindingContext as RegisterViewModel).PropertyChanged += RegisterPage_PropertyChanged; ;
        }

        private void RegisterPage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Loading")
            {
                Loading.IsVisible = true;
            }
            else if(e.PropertyName == "Complete")
            {
                Loading.IsVisible = false;
            }
        }
    }
}