using IS307.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.ViewModels
{
    public class OrderDetailViewModel : BaseViewModel
    {
        public ViewOrderModel viewOrder;
        public ViewOrderModel ViewOrder
        {
            get => viewOrder;
            set => SetProperty(ref viewOrder, value);
        }

        public ICommand GoBackCommand { get; set; }
        public OrderDetailViewModel(INavigation navigation, ViewOrderModel viewOrder)
        {
            ViewOrder = viewOrder;

            GoBackCommand = new Command(() =>
            {
                navigation.PopAsync();
            });
        }
    }
}