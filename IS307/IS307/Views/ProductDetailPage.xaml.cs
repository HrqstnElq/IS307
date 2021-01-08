using IS307.Models;
using IS307.ViewModels;
using System.ComponentModel;
using IS307.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        public ProductDetailPage()
        {
            InitializeComponent();
            BindingContext = new ProductDetailViewModel(Navigation);
        }

        public ProductDetailPage(ProductModel productModel)
        {
            InitializeComponent();
            BindingContext = new ProductDetailViewModel(Navigation, productModel);
            (BindingContext as INotifyPropertyChanged).PropertyChanged += ProductDetailPage_PropertyChanged;

            var vm = BindingContext as ProductDetailViewModel;
            if (vm.isFavorite == false)
                Heart.Text = IconFont.HeartOutline;
            else
                Heart.Text = IconFont.HeartMinus;

        }

        private void ProductDetailPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var vm = BindingContext as ProductDetailViewModel;
            if (e.PropertyName == nameof(ProductDetailViewModel.Increment) || e.PropertyName == nameof(ProductDetailViewModel.Decrement))
            {
                quantity.Text = vm.Quantity.ToString();
            }
            else if (e.PropertyName == nameof(ProductDetailViewModel.AddToCart))
            {
                DisplayAlert("Success", "Add product to cart successfuly", "Ok");
                Navigation.PopAsync();
            }
            else if(e.PropertyName == nameof(ProductDetailViewModel.Favorite))
            {
                if (vm.isFavorite == false)
                    Heart.Text = IconFont.HeartOutline;
                else
                    Heart.Text = IconFont.HeartMinus;
            }
        }
    }
}