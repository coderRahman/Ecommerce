using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Data.Repository.IRepository
{
    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        void Update(MenuItem menuItem);
    }
}
