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
            var vm = new CartPageViewModel(Navigation);
            BindingContext = vm;
            (BindingContext as INotifyPropertyChanged).PropertyChanged += CartPage_PropertyChanged;

            if (vm.CartItems.Count < 1)
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