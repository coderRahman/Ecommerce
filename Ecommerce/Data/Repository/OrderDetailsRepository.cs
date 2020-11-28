using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecommerce.Data;
using Ecommerce.Data.Repository;
using Ecommerce.Data.Repository.IRepository;
using Ecommerce.Models;
namespace Ecommerce.Data.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderDetails orderDetails)
        {
            var orderDetailsFromDb = _db.OrderDetails.FirstOrDefault(m => m.Id == orderDetails.Id);
            _db.OrderDetails.Update(orderDetailsFromDb);

            _db.SaveChanges();

        }
    }
}
