﻿using IS307.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private bool IsInit = false;

        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            if (IsInit == false)
            {
                base.OnAppearing();
                (BindingContext as HomeViewModel).OnAppearing();
                IsInit = true;
            }
        }
    }
}