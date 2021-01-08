using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IS307.Models
{
    public class CreateOrderViewModel
    {
        public static event EventHandler OnOrderSuccess;

        private List<CartItemModel> cartItems; 
        public double TotalPrice { get; set; }
        public string Address { get; set; }

        public ICommand GoBackCommand { get; set; }
        public ICommand OrderCommand { get; set; }

        public CreateOrderViewModel(INavigation navigation)
        {
            cartItems = App.Database.GetCart().Result;
            TotalPrice = cartItems.Sum(x => x.price * x.quantity);
            
            GoBackCommand = new Command(() =>
            {
                navigation.PopAsync();
            });

            OrderCommand = new Command(() =>
            {
                //order service
                OnOrderSuccess?.Invoke(this, new EventArgs());
                navigation.PopAsync();
            });
        }

    }
}
