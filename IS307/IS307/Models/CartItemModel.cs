using SQLite;

namespace IS307.Models
{
    public class CartItemModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string productId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string pictureUrl { get; set; }
        public int quantity { get; set; }
    }
}