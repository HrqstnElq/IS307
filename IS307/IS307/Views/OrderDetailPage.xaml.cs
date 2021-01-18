using IS307.Models;
using IS307.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailPage : ContentPage
    {
        public OrderDetailPage()
        {
            InitializeComponent();
        }

        public OrderDetailPage(ViewOrderModel viewOrder)
        {
            InitializeComponent();
            BindingContext = new OrderDetailViewModel(Navigation, viewOrder);
        }
    }
}