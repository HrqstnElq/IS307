using IS307.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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