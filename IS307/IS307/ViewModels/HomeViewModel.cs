using IS307.Models;
using IS307.Services;
using IS307.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.ViewModels
{
    internal class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<ProductModel> products;

        public ObservableCollection<ProductModel> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }

        private ObservableCollection<CategoryModel> categories;

        public ObservableCollection<CategoryModel> Categories
        {
            get => categories;
            set => SetProperty(ref categories, value);
        }

        public ICommand ViewProductDetailCommand { get; set; }
        public ICommand ViewProductInCategoryCommand { get; set; }
        public ICommand AddToCart { get; set; }

        private readonly CategoryService CategoryService = new CategoryService();
        private readonly ProductService ProductService = new ProductService();

        public HomeViewModel(INavigation navigation)
        {
            LoadPageCommand = new Command(async () =>
            {
                try
                {
                    Categories = new ObservableCollection<CategoryModel>(await CategoryService.GetAllCategory());
                    Products = new ObservableCollection<ProductModel>(await ProductService.GetTopProduct());
                }
                catch
                {
                    await App.Current.MainPage.DisplayAlert("Lổi !", "Không có kết nối mạng", "Ok");
                    await Shell.Current.GoToAsync("//LoginPage");
                }
                IsBusy = false;
            });

            ViewProductDetailCommand = new Command<ProductModel>(product =>
            {
                navigation.PushAsync(new ProductDetailPage(product));
            });

            ViewProductInCategoryCommand = new Command<CategoryModel>(category =>
            {
                navigation.PushAsync(new ProductsPage(category));
            });

            AddToCart = new Command<ProductModel>(async (product) =>
            {
                try
                {
                    await App.Database.SaveCart(new CartItemModel()
                    {
                        productId = product._id,
                        name = product.name,
                        pictureUrl = product.pictureUrl,
                        price = product.price,
                        quantity = 1
                    });
                    await App.Current.MainPage.DisplayAlert("Thành công !", "Đã thêm vào giỏ hàng ", "Ok");
                }
                catch
                {

                }
            });
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}