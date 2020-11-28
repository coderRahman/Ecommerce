using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.ViewModels
{
    public class MenuItemVM
    {
        public MenuItem MenuItem { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }
    }
}
