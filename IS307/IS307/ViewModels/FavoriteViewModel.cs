using IS307.Models;
using IS307.Services;
using IS307.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.ViewModels
{
    public class FavoriteViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ProductModel> Products { get; set; }

        public ICommand ViewProductDetailCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        private readonly ProductService productService = new ProductService();

        public FavoriteViewModel(INavigation navigation)
        {
            var token = App.Current.Properties["token"].ToString();
            Products = new ObservableCollection<ProductModel>(productService.GetUserFavoriteProduct(token));
            ViewProductDetailCommand = new MvvmHelpers.Commands.Command<ProductModel>(product =>
            {
                navigation.PushAsync(new ProductDetailPage(product));
            });

            GoBackCommand = new Command(() =>
            {
                navigation.PopAsync();
            });

            ProductDetailViewModel.UnFavoriteEvent += ProductDetailViewModel_UnFavoriteEvent;
        }

        private void ProductDetailViewModel_UnFavoriteEvent(object sender, System.EventArgs e)
        {
            var token = App.Current.Properties["token"].ToString();
            Products = new ObservableCollection<ProductModel>(productService.GetUserFavoriteProduct(token));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UnFavorite"));
        }
    }
}
