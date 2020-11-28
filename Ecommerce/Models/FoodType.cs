using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class FoodType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Food Type Name")]
        public string Name { get; set; }
    }
}
