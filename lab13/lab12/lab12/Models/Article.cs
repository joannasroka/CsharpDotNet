using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lab12.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be positive.")]
        [RegularExpression(@"^\d+([\.\,]\d{1,2})?$", ErrorMessage = "Price must has max. 2 decimal places, e.g. 12.99.")]
        public decimal Price { get; set; }

        public String Filename { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
