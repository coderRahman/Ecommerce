using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.ViewModels
{
    public class OrderDetailsCartVM
    { 
        public List<ShoppingCart> listCart { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
