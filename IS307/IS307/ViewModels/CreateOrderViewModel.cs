using IS307.Models;
using IS307.Services;
using System.Linq;
using System.Text.RegularExpressions;
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

            OrderCommand = new Command(async () =>
            {
                if (string.IsNullOrEmpty(Order.phone) || string.IsNullOrEmpty(Order.address))
                {
                    await App.Current.MainPage.DisplayAlert("Lỗi !", "Vui lòng điền đầy đủ thông tin", "Ok");
                }
                else
                {
                    var regex = new Regex(@"^(84|0[3|2|5|7|8|9])+([0-9]{8})$");
                    if (regex.IsMatch(Order.phone))
                    {
                        try
                        {
                            orderService.PostOrder(token, Order);
                            await App.Current.MainPage.DisplayAlert("Thành công !", "Create order completed", "Ok");
                            await App.Database.ClearCartItem();
                            await Shell.Current.GoToAsync("//OrderPage");
                        }
                        catch
                        {
                            await App.Current.MainPage.DisplayAlert("Lổi !", "Không có kết nối mạng", "Ok");
                        }
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Lỗi !", "Số điện thoại không hợp lệ", "Ok");
                }
            });
        }
    }
}