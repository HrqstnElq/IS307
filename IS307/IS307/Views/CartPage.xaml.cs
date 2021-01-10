using IS307.ViewModels;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
            BindingContext = new CartPageViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            (BindingContext as CartPageViewModel).OnAppearing();
        }

    }
}