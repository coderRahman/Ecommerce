using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.ViewModels
{
    public class OrderDetailsVM
    {
        public OrderHeader OrderHeader { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
