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
    public class ProductsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ProductModel> Products { get; set; }

        public CategoryModel Category { get; set; }

        public ICommand ViewProductDetailCommand { get; set; }

        public ICommand GoBackCommand { get; set; }
        private readonly ProductService ProductService = new ProductService();

        public ProductsViewModel()
        {
        }

        public ProductsViewModel(INavigation navigation, CategoryModel category)
        {
            Category = category;

           
            Products = new ObservableCollection<ProductModel>(ProductService.GetProductInCategory(category.name));

            ViewProductDetailCommand = new Command<ProductModel>(product =>
            {
                navigation.PushAsync(new ProductDetailPage(product));
            });

            GoBackCommand = new Command(() =>
            {
                navigation.PopAsync();
            });

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
