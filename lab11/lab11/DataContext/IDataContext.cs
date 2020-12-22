using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab11.ViewModels;

namespace lab11.DataContext
{
    public interface IDataContext
    {
        List<ShopViewModel> GetShops();
        ShopViewModel GetShop(int id);
        void AddShop(ShopViewModel shop);
        void RemoveShop(int id);
        void UpdateShop(ShopViewModel shop);
    }
}
