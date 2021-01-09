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
            (BindingContext as INotifyPropertyChanged).PropertyChanged += CartPage_PropertyChanged;
        }

        private void CartPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var vm = BindingContext as CartPageViewModel;
            if (e.PropertyName == nameof(vm.CartItems))
            {
                if(vm.CartItems?.Count < 1)
                {
                    Cart.ItemsSource = null;
                }
            }
        }

        protected override void OnAppearing()
        {
            (BindingContext as CartPageViewModel).OnAppearing();
        }

    }
}