using Bogus;
using IS307.Models;
using IS307.Services;
using IS307.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.ViewModels
{
    class HomeViewModel
    {
        public ObservableCollection<ProductModel> Products { get; set; }
        public ObservableCollection<CategoryModel> Categories { get; set; }
   
        public ICommand ViewProductDetailCommand { get; set; }
        public ICommand ViewProductInCategoryCommand { get; set; }

        private readonly CategoryService CategoryService = new CategoryService();
        private readonly ProductService ProductService = new ProductService();

        public HomeViewModel(INavigation navigation)
        {
            Categories = new ObservableCollection<CategoryModel>(CategoryService.GetAllCategory());
            Products = new ObservableCollection<ProductModel>(ProductService.GetTopProduct());
            ViewProductDetailCommand = new Command<ProductModel>(product =>
            {
                 navigation.PushAsync(new ProductDetailPage(product));
            });

            ViewProductInCategoryCommand = new Command<CategoryModel>(category =>
            {
                navigation.PushAsync(new ProductsPage(category));
            });
        }


    }
}
