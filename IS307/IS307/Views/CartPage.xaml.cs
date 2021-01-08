using IS307.Models;
using IS307.ViewModels;
using System.Collections.ObjectModel;
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
            (BindingContext as INotifyPropertyChanged).PropertyChanged += CartPage_PropertyChanged;
            Models.CreateOrderViewModel.OnOrderSuccess += OrderViewModel_OnOrderSuccess;
        }

        private void OrderViewModel_OnOrderSuccess(object sender, System.EventArgs e)
        {
            DisplayAlert("Success", "Order successfuly", "Ok");
            Cart.ItemsSource = null;
        }

        private void CartPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var vm = BindingContext as CartPageViewModel;
            if (e.PropertyName == nameof(CartPageViewModel.Increment) || e.PropertyName == nameof(CartPageViewModel.Decrement))
            {
                Cart.ItemsSource = vm.CartItems;
            }
        }
    }
}