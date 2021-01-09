using IS307.Models;
using IS307.Services;
using IS307.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.ViewModels
{
    public class ProductDetailViewModel : INotifyPropertyChanged
    {
        public static event EventHandler<EventArgs> AddProductEvent;
        public static event EventHandler<EventArgs> UnFavoriteEvent;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand GoBackCommand { get; set; }
        public ICommand Increment { get; set; }
        public ICommand Decrement { get; set; }
        public ICommand AddToCart { get; set; }
        public ICommand Favorite { get; set; }


        public ProductModel Product { get; set; }
        public int Quantity { get; set; } = 1;
        public bool isFavorite { get; set; }

        private readonly ProductService productService = new ProductService();
        public ProductDetailViewModel(INavigation navigation)
        {
        }

        public ProductDetailViewModel(INavigation navigation, ProductModel productModel)
        {
            var token = Application.Current.Properties["token"].ToString();
            
            Product = productModel;
            isFavorite = productService.IsFavoriteProduct(Product._id, token);

            GoBackCommand = new Command(() =>
            {
                navigation.PopAsync();
            });
            
            Increment = new Command(() =>
            {
                Quantity = Quantity + 1;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Increment)));
            });


            Decrement = new Command(() =>
            {
                Quantity = Quantity < 2 ? Quantity : Quantity - 1;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Decrement)));
            });

            AddToCart = new Command(() =>
            {
                App.Database.SaveCart(new CartItemModel()
                {
                    productId = Product._id,
                    name = Product.name,
                    pictureUrl = Product.pictureUrl,
                    price = Product.price,
                    quantity = Quantity
                });

                AddProductEvent?.Invoke(null, new EventArgs());
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddToCart)));
            });

            Favorite = new Command(async () =>
            {
                if (isFavorite)
                {
                    UnFavoriteEvent?.Invoke(this, new EventArgs());
                    await productService.UnFavoriteProduct(Product._id, token);
                }
                else
                    productService.FavoriteProduct(Product._id, token);
                isFavorite = !isFavorite;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Favorite)));
            });
        }

    }
}
