using IS307.ViewModels;
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
            BindingContext = new FavoriteViewModel(Navigation);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as FavoriteViewModel).OnAppearing();
        }
    }
}