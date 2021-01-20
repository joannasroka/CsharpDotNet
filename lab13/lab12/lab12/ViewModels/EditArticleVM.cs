using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace lab12.ViewModels
{
    public class EditArticleVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, double.PositiveInfinity, ErrorMessage = "Price must be positive.")]
        [RegularExpression(@"^\d+([\.\,]\d{0,2})?$", ErrorMessage = "Price must have max 2 decimal places, e.g. 12.99.")]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

    }
}
