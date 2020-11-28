using Ecommerce.Data.Repository.IRepository;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Data.Repository
{
  
        public interface IShoppingCartRepository : IRepository<ShoppingCart>
        {
            int IncrementCount(ShoppingCart shoppingCart, int count);
            int DecrementCount(ShoppingCart shoppingCart, int count);
        }
 }
