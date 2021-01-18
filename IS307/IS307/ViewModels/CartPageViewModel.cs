using IS307.Models;
using IS307.Views;
using System.Collections.ObjectModel;
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

        public bool hasItem = false;

        public bool HasItem
        {
            get => hasItem;
            set => SetProperty(ref hasItem, value);
        }

        public ICommand Increment { get; set; }
        public ICommand Decrement { get; set; }
        public ICommand RemoveCartItem { get; set; }
        public ICommand Order { get; set; }

        public CartPageViewModel(INavigation navigation)
        {
            LoadPageCommand = new Command(async () =>
            {
                try
                {
                    CartItems = new ObservableCollection<CartItemModel>(await App.Database.GetCart());
                    TotalPrice = CartItems.Sum(x => x.price * x.quantity);
                    IsBusy = false;
                    if (cartItems.Count > 0)
                        HasItem = true;
                    else
                        HasItem = false;
                }
                catch
                {
                    await App.Current.MainPage.DisplayAlert("Lổi !", "Không có kết nối mạng", "Ok");
                    await Shell.Current.GoToAsync("//LoginPage");
                }
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
                if (item.quantity > 1)
                {
                    item.quantity = item.quantity - 1;
                    await App.Database.SaveCart(item);
                    CartItems = new ObservableCollection<CartItemModel>(await App.Database.GetCart());
                    TotalPrice = CartItems.Sum(x => x.price * x.quantity);
                }
            });

            RemoveCartItem = new Command<CartItemModel>(async (item) =>
            {
                await App.Database.DeleteCartItem(item);
                CartItems = new ObservableCollection<CartItemModel>(await App.Database.GetCart());
                TotalPrice = CartItems.Sum(x => x.price * x.quantity);
                if (cartItems.Count > 0)
                    HasItem = true;
                else
                    HasItem = false;
            });

            Order = new Command(async () =>
            {
                if (CartItems.Count > 0)
                    await navigation.PushAsync(new CreateOrderPage());
                else
                    await App.Current.MainPage.DisplayAlert("Lổ !", " Giỏ hàng trống", "Hủy");
            });
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}