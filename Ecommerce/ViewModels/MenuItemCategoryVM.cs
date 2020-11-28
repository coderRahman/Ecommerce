using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels
{
    public class MenuItemCategoryVM
    {
        public IEnumerable<MenuItem> MenuItemList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
    }
}
