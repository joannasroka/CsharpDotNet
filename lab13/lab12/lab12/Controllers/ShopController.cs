using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab12.Data;
using lab12.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace lab12.Controllers
{
    public class ShopController : Controller
    {
        public const int EXPIRATION_DAYS = 7;

        private readonly ApplicationDbContext _context;
        private Dictionary<int, CartItemVM> basket;

        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appDbContext = await _context.Articles.Include(a => a.Category).ToListAsync();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Current = null;
            return View(appDbContext);
        }
       public async Task<IActionResult> Category(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articles = await _context.Articles
                .Include(a => a.Category)
                .Where(a => a.CategoryId == id)
                .ToListAsync();

            ViewBag.Current = id;
            ViewBag.Categories = _context.Categories.ToList();
            return View("Index", articles);
        }

         // GET: Basket
        public Dictionary<int, CartItemVM> GetBasket()
        {
            string basketString;
            Request.Cookies.TryGetValue("basket", out basketString);
            if (basketString != null)
            {
                basket = JsonConvert.DeserializeObject<Dictionary<int, CartItemVM>>(Request.Cookies["basket"]);
            }
            else {
                basket = new Dictionary<int, CartItemVM>();
            }
            return basket;
        }

        public void SaveBasket(Dictionary<int, CartItemVM> basket)
        {
            string basketToString = JsonConvert.SerializeObject(basket);
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(EXPIRATION_DAYS);
            Response.Cookies.Append("basket", basketToString, options);
        }

        public decimal GetTotal()
        {
            basket = GetBasket();
            decimal? total = decimal.Zero;
            if (basket == null)
            {
                return decimal.Zero;
            }
            foreach (KeyValuePair<int, CartItemVM> item in basket)
            {
                total += (decimal?)item.Value.Article.Price * item.Value.Quantity;
            }
            return total ?? decimal.Zero;
        }

        public async Task<IActionResult> Basket()
        {
            basket = GetBasket();
            ViewBag.Total = GetTotal();
            List<CartItemVM> basketList = new List<CartItemVM>();
           
            var keys = basket.Keys.ToList();
            var articles = await _context.Articles.Include(a => a.Category).Where(a => keys.Contains(a.Id)).ToListAsync();

            foreach (var article in articles)
            {
                basketList.Add(new CartItemVM
                {
                    ArticleId = article.Id,
                    Article = article,
                    Quantity = basket[article.Id].Quantity
                });
            }

            return View(basketList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int id)
        {
            basket = GetBasket();
            if (CartItemExists(id)) 
            {
                basket[id].Quantity++;
            }
            else
            {            
                CartItemVM cartItem = new CartItemVM
                {
                    ArticleId = id,
                    Article = _context.Articles.SingleOrDefault(
                   a => a.Id == id),
                    Quantity = 1
                };
                basket.Add(id, cartItem);
            }
            SaveBasket(basket);

            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReduceCartItem(int id)
        {
            basket = GetBasket();

            if (CartItemExists(id))
            {
                if (basket[id].Quantity <= 1) basket.Remove(id);
                else basket[id].Quantity--;
            }

            SaveBasket(basket);
            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            basket = GetBasket();

            if (CartItemExists(id))
            {
                basket.Remove(id);
                SaveBasket(basket);
            }
            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }
        private bool CartItemExists(int id)
        {
            basket = GetBasket();
            if (basket == null) return false;
            return basket.ContainsKey(id);
        }
    }
}

