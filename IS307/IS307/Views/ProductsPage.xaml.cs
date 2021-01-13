using IS307.Models;
using IS307.ViewModels;
using Xamarin.Forms;

namespace IS307.Views
{
    public partial class ProductsPage : ContentPage
    {
        private bool isInit = false;

        public ProductsPage(CategoryModel category)
        {
            InitializeComponent();
            BindingContext = new ProductsViewModel(Navigation, category);
        }

        protected override void OnAppearing()
        {
            if (isInit == false)
            {
                (BindingContext as ProductsViewModel).OnAppearing();
                isInit = true;
            }
        }
    }
}