using IS307.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
        public OrderPage()
        {
            InitializeComponent();
            BindingContext = new OrderViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            (BindingContext as OrderViewModel).OnAppearing();
        }
    }
}