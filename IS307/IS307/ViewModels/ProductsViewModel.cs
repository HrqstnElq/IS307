using IS307.Models;
using IS307.Services;
using IS307.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        public ObservableCollection<ProductModel> products;

        public ObservableCollection<ProductModel> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }

        public CategoryModel category;

        public CategoryModel Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }

        public ICommand ViewProductDetailCommand { get; set; }

        public ICommand GoBackCommand { get; set; }
        public ICommand AddToCart { get; set; }
        private readonly ProductService ProductService = new ProductService();

        public ProductsViewModel()
        {
        }

        public ProductsViewModel(INavigation navigation, CategoryModel category)
        {
            LoadPageCommand = new Command(async () =>
            {
                try
                {
                    Category = category;
                    Products = new ObservableCollection<ProductModel>(await ProductService.GetProductInCategory(category.name));
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

            GoBackCommand = new Command(() =>
            {
                navigation.PopAsync();
            });

            AddToCart = new Command<ProductModel>(async (product) =>
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
            });
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}