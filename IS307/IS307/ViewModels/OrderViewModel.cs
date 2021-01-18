using IS307.Models;
using IS307.Services;
using IS307.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        public ObservableCollection<ViewOrderModel> orders;
        public ObservableCollection<ViewOrderModel> Orders
        {
            get => orders;
            set => SetProperty(ref orders, value);
        }

        public ICommand OnClickCommand { get; set; }

        private readonly OrderService orderService = new OrderService();
        public OrderViewModel(INavigation navigation)
        {
            LoadPageCommand = new Command(async () =>
            {
                IsBusy = true;
                var token = App.Current.Properties["token"]?.ToString();
                if(token != null)
                {
                    Orders = new ObservableCollection<ViewOrderModel>(await orderService.GetOrders(token));
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Lổi !", "Không có kết nối mạng", "Ok");
                    await Shell.Current.GoToAsync("//LoginPage");
                }
                IsBusy = false;
            });

            OnClickCommand = new Command<ViewOrderModel>(async (order) =>
            {
                await navigation.PushAsync(new OrderDetailPage(order));
            });
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}