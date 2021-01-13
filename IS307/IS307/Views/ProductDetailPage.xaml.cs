using IS307.Models;
using IS307.Resources;
using IS307.ViewModels;
using System.ComponentModel;
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
        }

        public ProductDetailPage(ProductModel productModel)
        {
            InitializeComponent();
            BindingContext = new ProductDetailViewModel(Navigation, productModel);
            (BindingContext as INotifyPropertyChanged).PropertyChanged += ProductDetailPage_PropertyChanged;
        }

        private void ProductDetailPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var vm = BindingContext as ProductDetailViewModel;
            if (e.PropertyName == nameof(ProductDetailViewModel.AddToCart))
            {
                DisplayAlert("Thành công", "Đã thêm vào giỏ hàng", "Ok");
                Navigation.PopAsync();
            }
            else if (e.PropertyName == nameof(ProductDetailViewModel.Favorite))
            {
                if (vm.isFavorite == false)
                    Heart.Text = IconFont.HeartOutline;
                else
                    Heart.Text = IconFont.HeartMinus;
            }
            else if (e.PropertyName == "Loading")
            {
                Loading.IsVisible = true;
            }
            else if (e.PropertyName == "Complete")
            {
                Loading.IsVisible = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as ProductDetailViewModel).OnAppearing();
        }
    }
}