using IS307.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IS307
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<CartItemModel>().Wait();
        }

        public Task<List<CartItemModel>> GetCart() => _database.Table<CartItemModel>().ToListAsync();

        public Task<int> SaveCart(CartItemModel cartItem)
        {
            if (cartItem.ID == 0)
            {
                var item = _database.FindAsync<CartItemModel>(x => x.productId == cartItem.productId).Result;
                if (item != null)
                {
                    item.quantity += cartItem.quantity;
                    return _database.UpdateAsync(item);
                }
                return _database.InsertAsync(cartItem);
            }
            else
            {
                if (cartItem.quantity == 0)
                {
                    return _database.DeleteAsync(cartItem);
                }
                else
                    return _database.UpdateAsync(cartItem);
            }
        }

        public Task<int> DeleteCartItem(CartItemModel cartItem) => _database.DeleteAsync(cartItem);

        public Task<int> ClearCartItem() => _database.DeleteAllAsync<CartItemModel>();
    }
}