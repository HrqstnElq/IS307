using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS307.Models
{
    public class OrderModel
    {
        public List<CartItemModel> products { get; set; }
        public string address { get; set; }
    }
}
