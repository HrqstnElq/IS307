using Bogus;
using IS307.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.ViewModels
{
    public class ProductsViewModel : INotifyPropertyChanged
    {
        private bool productShown;
        private bool hasCartItems;

        public ObservableCollection<ProductModel> Products { get; set; }

        public ObservableCollection<CategoryModel> Categories { get; set; }

        public bool ProductShown
        {
            get => productShown;
            set
            {
                productShown = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProductShown)));
            }
        }

        public bool HasCartItems
        {
            get => hasCartItems;
            set
            {
                hasCartItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasCartItems)));
            }
        }

        public ProductModel ShownProduct { get; set; }

        public ICommand ViewProductDetailCommand { get; set; }

        public ICommand AddToCartCommand { get; set; }

        public ICommand ViewCartCommand { get; set; }

        public ICommand GoBackCommand { get; set; }

        public ProductsViewModel(INavigation navigation)
        {
            var productFaker = new Faker<ProductModel>()
                .RuleFor(x => x.Pricing, o => o.Random.Double(10, 200))
                .RuleFor(x => x.Name, o => o.Commerce.ProductName())
                .RuleFor(x => x.PictureUrl, o => o.Image.PicsumUrl());

            var items = productFaker.Generate(200);

            Products = new ObservableCollection<ProductModel>(items);

            ViewProductDetailCommand = new Command<ProductModel>(product =>
            {
                ShownProduct = product;
                ProductShown = true;
                HasCartItems = true;
            });

            AddToCartCommand = new Command<ProductModel>(product =>
            {
                ProductShown = false;
                ShownProduct = null;
            });

            GoBackCommand = new Command(() =>
            {
                navigation.PopAsync();
            });

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
