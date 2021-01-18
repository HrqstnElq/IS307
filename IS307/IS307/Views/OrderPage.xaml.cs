using IS307.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : CarouselPage
    {
        public OrderPage()
        {
            InitializeComponent();
            order.BindingContext = new OrderViewModel(Navigation, false);
            orderIsReceive.BindingContext = new OrderViewModel(Navigation, true);
        }

        protected override void OnAppearing()
        {
            if(this.CurrentPage == order)
            {
                (order.BindingContext as OrderViewModel).OnAppearing();
            }
            else
            {
                (orderIsReceive.BindingContext as OrderViewModel).OnAppearing();
            }
        }

    }
}