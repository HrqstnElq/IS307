using IS307.Models;
using IS307.Services;
using IS307.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.ViewModels
{
    class HomeViewModel : BaseViewModel
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
            set=>SetProperty(ref categories, value);
        }
        public ICommand ViewProductDetailCommand { get; set; }
        public ICommand ViewProductInCategoryCommand { get; set; }

        private readonly CategoryService CategoryService = new CategoryService();
        private readonly ProductService ProductService = new ProductService();

        public HomeViewModel(INavigation navigation)
        {
            LoadPageCommand = new Command(async () =>
            {
                Categories = new ObservableCollection<CategoryModel>(await CategoryService.GetAllCategory());
                Products = new ObservableCollection<ProductModel>(await ProductService.GetTopProduct());
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
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
