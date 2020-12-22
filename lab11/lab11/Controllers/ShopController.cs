using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lab11.DataContext;
using lab11.ViewModels;

namespace lab11.Controllers
{
    public class ShopController : Controller
    {
        private IDataContext _dataContext;

        public ShopController(IDataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public ActionResult Index()
        {
            return View(_dataContext.GetShops());
        }

        // GET: ShopController/Details/5
        public ActionResult Details(int id)
        {
            return View(_dataContext.GetShop(id)); 
        }

        // GET: ShopController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShopController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShopViewModel shop)
        {
            try
            {
                if (ModelState.IsValid)                
                    _dataContext.AddShop(shop);  
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShopController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_dataContext.GetShop(id));
        }

        // POST: ShopController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ShopViewModel shop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    shop.Id = id; 
                    _dataContext.UpdateShop(shop); 
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShopController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_dataContext.GetShop(id)); 
        }

        // POST: ShopController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection) 
        {
            try
            {
                _dataContext.RemoveShop(id); 
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
