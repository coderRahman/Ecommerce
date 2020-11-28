using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Data.Repository.IRepository
{
    public interface IFoodTypeRepository : IRepository<FoodType>
    {
        IEnumerable<SelectListItem> GetFoodTypeListForDropDown();

        void Update(FoodType foodType);
    }
}
