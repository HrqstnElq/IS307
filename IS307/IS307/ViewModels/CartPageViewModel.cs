using Bogus;
using IS307.Models;
using IS307.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.ViewModels
{
    public class CartPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CartItemModel> CartItems { get; set; }
        public double TotalPrice { get; set; }
        public ICommand Increment { get; set; }
        public ICommand Decrement { get; set; }
        public ICommand RemoveCartItem { get; set; }
        public ICommand Order{ get; set; }


        public CartPageViewModel(INavigation navigation)
        {
            ProductDetailViewModel.AddProductEvent += ProductDetailViewModel_AddProductEvent;

            CartItems = new ObservableCollection<CartItemModel>(App.Database.GetCart().Result);
            TotalPrice = CartItems.Sum(x => x.price * x.quantity);

            Increment = new Command<CartItemModel>(async (item) =>
            {
                item.quantity = item.quantity + 1;
                await App.Database.SaveCart(item);
                CartItems = new ObservableCollection<CartItemModel>(await App.Database.GetCart());
                TotalPrice = CartItems.Sum(x => x.price * x.quantity);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Increment)));
            });

            Decrement = new Command<CartItemModel>(async (item) =>
            {
                item.quantity = item.quantity < 2 ? item.quantity : item.quantity -  1;
                await App.Database.SaveCart(item);
                CartItems = new ObservableCollection<CartItemModel>(await App.Database.GetCart());
                TotalPrice = CartItems.Sum(x => x.price * x.quantity);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Increment)));
            });

            RemoveCartItem = new Command<CartItemModel>(async (item) =>
            {
                await App.Database.DeleteCartItem(item);
                CartItems = new ObservableCollection<CartItemModel>(await App.Database.GetCart());
                TotalPrice = CartItems.Sum(x => x.price * x.quantity);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Increment)));
            });

            Order = new Command(() =>
            {
                navigation.PushAsync(new CreateOrderPage());
            });
        }

        private void ProductDetailViewModel_AddProductEvent(object sender, EventArgs e)
        {
            CartItems = new ObservableCollection<CartItemModel>(App.Database.GetCart().Result);
            TotalPrice = CartItems.Sum(x => x.price * x.quantity);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Increment)));
        }
    }
}
