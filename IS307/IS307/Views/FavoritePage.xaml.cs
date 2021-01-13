using IS307.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IS307.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritePage : ContentPage
    {
        private bool isInit = false;

        public FavoritePage()
        {
            InitializeComponent();
            BindingContext = new FavoriteViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            if (isInit == false)
            {
                (BindingContext as FavoriteViewModel).OnAppearing();
                isInit = true;
            }
        }
    }
}