using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace Ecommerce.Data.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
    }
}
