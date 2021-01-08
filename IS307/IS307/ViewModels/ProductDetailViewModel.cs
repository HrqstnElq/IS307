using IS307.Models;
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
        public ICommand GoBackCommand { get; set; }
        public ICommand Increment { get; set; }
        public ICommand Decrement { get; set; }
        public ICommand AddToCart { get; set; }


        public ProductModel Product { get; set; }
        public static event EventHandler<EventArgs> AddProductEvent;
        public int Quantity { get; set; } = 1;

        public event PropertyChangedEventHandler PropertyChanged;
        public ProductDetailViewModel(INavigation navigation)
        {
        }

        public ProductDetailViewModel(INavigation navigation, ProductModel productModel)
        {
            Product = productModel;

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
                    name = Product.name,
                    pictureUrl = Product.pictureUrl,
                    price = Product.price,
                    quantity = Quantity
                });

                var a = navigation;
                AddProductEvent?.Invoke(null, new EventArgs());
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddToCart)));
            });

        }

    }
}
