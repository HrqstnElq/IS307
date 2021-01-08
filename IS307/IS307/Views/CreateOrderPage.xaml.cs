using IS307.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateOrderPage : ContentPage
    {
        public CreateOrderPage()
        {
            InitializeComponent();
            BindingContext = new CreateOrderViewModel(Navigation);
        }
    }
}