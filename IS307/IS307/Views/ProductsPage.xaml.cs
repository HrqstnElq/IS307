using IS307.Models;
using IS307.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace IS307.Views
{
    public partial class ProductsPage : ContentPage
    {
        public ProductsPage()
        {
            InitializeComponent();
            BindingContext = new ProductsViewModel();
        }

        public ProductsPage(CategoryModel category)
        {
            InitializeComponent();
            BindingContext = new ProductsViewModel(Navigation,category);
        }
    }
}
