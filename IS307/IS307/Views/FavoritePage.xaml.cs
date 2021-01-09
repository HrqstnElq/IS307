using IS307.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritePage : ContentPage
    {
        public FavoritePage()
        {
            InitializeComponent();
            var vm = new FavoriteViewModel(Navigation);
            BindingContext = vm;
            (BindingContext as INotifyPropertyChanged).PropertyChanged += FavoritePage_PropertyChanged; ;
        }

        private void FavoritePage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var vm = BindingContext as FavoriteViewModel;
            if (e.PropertyName == "UnFavorite")
            {
                Products.ItemsSource = vm.Products;
            }
        }
    }
}