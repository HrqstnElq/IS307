using IS307.Models;
using IS307.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        public ProductDetailPage()
        {
            InitializeComponent();
            BindingContext = new ProductDetailViewModel(Navigation);
        }

        public ProductDetailPage(ProductModel productModel)
        {
            InitializeComponent();
            BindingContext = new ProductDetailViewModel(Navigation, productModel);
            (BindingContext as INotifyPropertyChanged).PropertyChanged += ProductDetailPage_PropertyChanged; ;

        }

        private void ProductDetailPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ProductDetailViewModel.Increment) || e.PropertyName == nameof(ProductDetailViewModel.Decrement))
            {
                var vm = BindingContext as ProductDetailViewModel;
                quantity.Text = vm.Quantity.ToString();
            }
            if(e.PropertyName == nameof(ProductDetailViewModel.AddToCart))
            {
                DisplayAlert("Success", "Add product to cart successfuly", "Ok");
                Navigation.PopAsync();
            }
        }
    }
}