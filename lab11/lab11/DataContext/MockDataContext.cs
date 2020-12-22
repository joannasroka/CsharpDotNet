using lab11.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lab11.DataContext
{
    public class MockDataContext : IDataContext
    {
        Dictionary<int, ShopViewModel> shops = new Dictionary<int, ShopViewModel>
        {
            [0] = new ShopViewModel(0, "croissant@gmail.com", "Croissant", "12-345", ShopType.Bakery),
            [1] = new ShopViewModel(1, "keyboardandmouse@onet.pl", "Keyboard and Mouse", "45-880", ShopType.ComputerShop),
            [2] = new ShopViewModel(2, "freshfood@gmail.com", "Fresh Food", "99-000", ShopType.Market)
        };
        public void AddShop(ShopViewModel shop)
        {
            int nextId;
            if (shops.Count == 0) nextId = 0;
            else
            {
                nextId = shops.Keys.Max() + 1;
            }
            shop.Id = nextId;
            shops.Add(nextId, shop);
        }

        public ShopViewModel GetShop(int id)
        {
            return shops.GetValueOrDefault(id);
        }

        public List<ShopViewModel> GetShops()
        {
            return shops.Values.OrderBy(s => s.Id).ToList();
        }

        public void RemoveShop(int id)
        {
            if (shops.ContainsKey(id))
            {
                shops.Remove(id);
            }

        }

        public void UpdateShop(ShopViewModel shop)
        {
            if (shops.ContainsKey(shop.Id))
            {
                shops[shop.Id] = shop;
            }
        }
    }
}
