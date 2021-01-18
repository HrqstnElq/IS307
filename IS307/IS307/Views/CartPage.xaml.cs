using IS307.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

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