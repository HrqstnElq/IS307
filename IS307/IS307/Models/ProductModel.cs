using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS307.Models
{
    public class ProductModel
    {
        public int _id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string pictureUrl { get; set; }
        public string category { get; set; }
    }
}
