using System.ComponentModel.DataAnnotations;
using lab12.Models;
using Microsoft.AspNetCore.Http;

namespace lab12.ViewModels
{
    public class CartItemVM
    {
        [Required]
        public int ArticleId { get; set; }
        [Required]
        public Article Article { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be positive.")]
        public int Quantity { get; set; }
    }
}
