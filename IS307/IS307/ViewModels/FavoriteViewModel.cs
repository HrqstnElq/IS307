using IS307.Models;
using IS307.Services;
using IS307.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.ViewModels
{
    public class FavoriteViewModel : BaseViewModel
    {
        private ObservableCollection<ProductModel> products;
        public ObservableCollection<ProductModel> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }

        public ICommand ViewProductDetailCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        private readonly ProductService productService = new ProductService();

        public FavoriteViewModel(INavigation navigation)
        {
            LoadPageCommand = new Command(async () =>
            {
                Products = new ObservableCollection<ProductModel>(await productService.GetUserFavoriteProduct(App.Current.Properties["token"].ToString()));
                IsBusy = false;
            });

            ViewProductDetailCommand = new Command<ProductModel>(product =>
            {
                navigation.PushAsync(new ProductDetailPage(product));
            });

            GoBackCommand = new Command(() =>
            {
                navigation.PopAsync();
            });

        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

    }
}
