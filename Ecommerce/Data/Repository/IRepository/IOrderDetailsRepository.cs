﻿using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace Ecommerce.Data.Repository.IRepository
{
    public interface IOrderDetailsRepository : IRepository<OrderDetails>
    {
        void Update(OrderDetails orderDetails);
    }
}
