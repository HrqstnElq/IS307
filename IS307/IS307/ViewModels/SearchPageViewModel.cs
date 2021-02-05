using IS307.Models;
using IS307.Services;
using IS307.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
        private ObservableCollection<ProductModel> products;

        public ObservableCollection<ProductModel> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }

        private string search;
        public string Search
        {
            get => search;
            set => SetProperty(ref search, value);
        }

        private readonly ProductService ProductService = new ProductService();

        public SearchPageViewModel()
        {
            LoadPageCommand = new Command(async () =>
            {
                try
                {
                    Products = new ObservableCollection<ProductModel>(await ProductService.SearchProduct(Search));
                }
                catch
                {
                    await App.Current.MainPage.DisplayAlert("Lổi !", "Không có kết nối mạng", "Ok");
                    await Shell.Current.GoToAsync("//LoginPage");
                }
                IsBusy = false;
            });

        }

        public void LoadSearch()
        {
            IsBusy = true;
        }
    }
}
