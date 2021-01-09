using IS307.Models;
using IS307.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.ViewModels
{
    public class CreateOrderViewModel
    {
        public OrderModel Order { get; set; }
        public double TotalPrice { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand OrderCommand { get; set; }

        private readonly OrderService orderService = new OrderService();
        public CreateOrderViewModel(INavigation navigation)
        {
            var token = App.Current.Properties["token"].ToString();
            Order = new OrderModel()
            {
                products = App.Database.GetCart().Result,
                phone = "",
                address = ""
            };

            TotalPrice = Order.products.Sum(x => x.price * x.quantity);

            GoBackCommand = new Command(() =>
            {
                navigation.PopAsync();
            });

            OrderCommand = new Command(() =>
            {
                if(string.IsNullOrEmpty(Order.phone) || string.IsNullOrEmpty(Order.address))
                {
                    App.Current.MainPage.DisplayAlert("Waring !", "Phone number or address is required", "Ok");
                }
                else
                {
                    orderService.PostOrder(token, Order);
                    App.Current.MainPage.DisplayAlert("Success !", "Create order completed", "Ok");
                    App.Database.ClearCartItem();
                    App.Current.MainPage = new AppShell();
                }
            });
        }
    }
}
