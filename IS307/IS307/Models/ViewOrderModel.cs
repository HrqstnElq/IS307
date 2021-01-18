using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS307.Models
{
    public class ViewOrderModel
    {
        public string _id { get; set; }
        public DateTime dateCreated { get; set; }
        public string userId { get; set; }
        public List<ProductInOrderModel> products { get; set; }
        public double total { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public bool isReceive { get; set; }

    }

    public class ProductInOrderModel
    {
        public ProductModel Product { get; set; }
        public int quantity { get; set; }
    }
}
