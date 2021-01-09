using Bogus;
using IS307.Models;
using IS307.Services;
using IS307.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private readonly ProductService ProductService = new ProductService();

        public ProductsViewModel()
        {
        }

        public ProductsViewModel(INavigation navigation, CategoryModel category)
        {
            LoadPageCommand = new Command(async () =>
            {
                Category = category;
                Products = new ObservableCollection<ProductModel>(await ProductService.GetProductInCategory(category.name));
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
