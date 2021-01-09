using IS307.Models;
using IS307.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.ViewModels
{
    public class CartPageViewModel : BaseViewModel
    {
        private ObservableCollection<CartItemModel> cartItems;
        public ObservableCollection<CartItemModel> CartItems
        {
            get => cartItems;
            set => SetProperty(ref cartItems, value);
        }
        private double totalPrice;
        public double TotalPrice
        {
            get => totalPrice;
            set => SetProperty(ref totalPrice, value);
        } 
        public ICommand Increment { get; set; }
        public ICommand Decrement { get; set; }
        public ICommand RemoveCartItem { get; set; }
        public ICommand Order { get; set; }


        public CartPageViewModel(INavigation navigation)
        {
            LoadPageCommand = new Command(async () =>
            {
                CartItems = new ObservableCollection<CartItemModel>(await App.Database.GetCart());
                TotalPrice = CartItems.Sum(x => x.price * x.quantity);
                OnPropertyChanged(nameof(CartItems));
                IsBusy = false;
            });

            Increment = new Command<CartItemModel>(async (item) =>
            {
                item.quantity = item.quantity + 1;
                await App.Database.SaveCart(item);
                CartItems = new ObservableCollection<CartItemModel>(await App.Database.GetCart());
                TotalPrice = CartItems.Sum(x => x.price * x.quantity);
            });

            Decrement = new Command<CartItemModel>(async (item) =>
            {
                item.quantity = item.quantity < 2 ? item.quantity : item.quantity - 1;
                await App.Database.SaveCart(item);
                CartItems = new ObservableCollection<CartItemModel>(await App.Database.GetCart());
                TotalPrice = CartItems.Sum(x => x.price * x.quantity);
            });

            RemoveCartItem = new Command<CartItemModel>(async (item) =>
            {
                await App.Database.DeleteCartItem(item);
                CartItems = new ObservableCollection<CartItemModel>(await App.Database.GetCart());
                TotalPrice = CartItems.Sum(x => x.price * x.quantity);
            });

            Order = new Command(async () =>
            {
                if (CartItems.Count > 0)
                    await navigation.PushAsync(new CreateOrderPage());
                else
                    await App.Current.MainPage.DisplayAlert("Error !", "Cart empty", "Cancel");
            });
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
