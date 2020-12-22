using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace lab11.ViewModels
{
    public enum ShopType
    {
        Bakery, Market, ComputerShop, Bookshop, ClothesShop
    }
    public class ShopViewModel
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter valid email address, e.g. address@domain.com")]
        public string Email { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Too short name, minimum number of characters is {1}")]
        [MaxLength(40, ErrorMessage = " Too long name, maximum number of characters is {1}")]
        public string Name { get; set; }

        [RegularExpression(@"[0-9]{2}-[0-9]{3}", ErrorMessage = "Zip code must match a pattern, e.g. 00-000")]
        public string ZipCode { get; set; }

        [Required]
        public ShopType Type { get; set; }

        public ShopViewModel()
        { }

        public ShopViewModel(int id, string email, string name, string zipCode, ShopType type)
        {
            this.Id = id;
            this.Email = email;
            this.Name = name;
            this.ZipCode = zipCode;
            this.Type = type;
        }

    }
}
