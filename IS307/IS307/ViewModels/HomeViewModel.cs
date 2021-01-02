using MvvmHelpers;
using System.Collections.ObjectModel;

namespace IS307.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<int> Products { get; set; }
        public HomeViewModel()
        {
            Title = "Home";
            var data = new ObservableCollection<int>();
            data.Add(1);
            data.Add(1);
            data.Add(1);
            data.Add(1);
            data.Add(1);
            data.Add(1);
            data.Add(1);
            data.Add(1);
            data.Add(1);
            data.Add(1);
            data.Add(1);
            data.Add(1);
            data.Add(1);
            data.Add(1);
            data.Add(1);
            data.Add(1);
            data.Add(1);
            data.Add(1);
            data.Add(1);

            Products = data;
        }
    }
}
