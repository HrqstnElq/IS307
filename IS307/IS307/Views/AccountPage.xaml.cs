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
            BindingContext = new AccountViewModel(Navigation);
            (BindingContext as AccountViewModel).PropertyChanged += Vm_PropertyChanged;
        }

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Loading")
            {
                Loading.IsVisible = true;
            }else if(e.PropertyName == "Complete")
            {
                Loading.IsVisible = false;
            }
        }
    }
}