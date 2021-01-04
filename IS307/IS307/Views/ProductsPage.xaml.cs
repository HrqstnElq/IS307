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

            BindingContext = new ProductsViewModel(Navigation);
            (BindingContext as INotifyPropertyChanged).PropertyChanged += ProductsPage_PropertyChanged;
        }

        private void ProductsPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ProductsViewModel.ProductShown))
            {
                var vm = BindingContext as ProductsViewModel;

                if (originalY < 0)
                {
                    originalY = itemContainer.Y;
                }

                if (vm.ProductShown == true)
                {
                    (itemContainer.Parent as VisualElement).IsVisible = true;
                    itemContainer.TranslateTo(itemContainer.X, originalY);
                }
                else
                {
                    itemContainer.TranslateTo(itemContainer.X, originalY + itemContainer.Height * 1.1);
                    (itemContainer.Parent as VisualElement).IsVisible = false;
                }
            }
        }

        double originalY = -1;
        double lastPannedY = 0;

        void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            Debug.WriteLine("Scroll: " + e.StatusType + ":" + e.TotalY);

            if (originalY < 0)
            {
                originalY = itemContainer.TranslationY;
            }

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    return;
                case GestureStatus.Canceled:
                case GestureStatus.Completed:
                    if (lastPannedY < itemContainer.Height / 2)
                    {
                        itemContainer.TranslateTo(itemContainer.X, originalY);
                    }
                    else
                    {
                        itemContainer.TranslateTo(itemContainer.X, originalY + itemContainer.Height * 1.1);

                        (BindingContext as ProductsViewModel).ProductShown = false;
                        (BindingContext as ProductsViewModel).ShownProduct = null;
                    }
                    return;
                default:
                    break;
            }

            lastPannedY = e.TotalY;

            var newY = originalY + Math.Max(e.TotalY, 0);
            itemContainer.TranslateTo(itemContainer.X, newY);
        }
    }
}
