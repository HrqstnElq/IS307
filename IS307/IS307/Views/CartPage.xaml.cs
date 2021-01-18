using IS307.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace IS307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
            BindingContext = new CartPageViewModel(Navigation);
            (BindingContext as CartPageViewModel).PropertyChanged += CartPage_PropertyChanged;
        }

        private void CartPage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "ChangeCart")
            {
                //Total.Text = (BindingContext as CartPageViewModel).CartItems.Sum(x => x.quantity * x.price).ToString("0:#,###,###.000đ");
            }
        }

        protected override void OnAppearing()
        {
            (BindingContext as CartPageViewModel).OnAppearing();
        }


    }
}