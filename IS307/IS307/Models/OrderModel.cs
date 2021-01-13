using System.Collections.Generic;

namespace IS307.Models
{
    public class OrderModel
    {
        public List<CartItemModel> products { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
    }
}