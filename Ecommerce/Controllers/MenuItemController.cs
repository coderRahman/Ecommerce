using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Data.Repository.IRepository;
using Ecommerce.Models;
using Ecommerce.Services;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    [Authorize(Roles = SD.ManagerRole)]
    public class MenuItemController : Controller
    {
        // GET: CategoryController

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public MenuItemController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }
        public JsonResult GetAll()
        {
            IEnumerable<MenuItem> menuItems = (IEnumerable<MenuItem>)_unitOfWork.Category.GetAll();
            return Json(new { data = menuItems });
        }
        public IActionResult Index()
        {
            return View();

        }

        // GET: CategoryController/Details/5


        // GET: CategoryController/Create
        public ActionResult Create()
        {
           var  MenuItemObj = new MenuItemVM
            {
                MenuItem = new MenuItem(),
                CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
                FoodTypeList = _unitOfWork.FoodType.GetFoodTypeListForDropDown()
            };
            return View(MenuItemObj);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItem MenuItemObj,IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string webRootPath = _hostingEnvironment.WebRootPath;
            var uploads = System.IO.Path.Combine(webRootPath, "images");
            var extension = "";
            var filePath = "";
            var fileName = "";
            if (file.Length > 0)
                {
                    extension = System.IO.Path.GetExtension(file.FileName);
                    fileName = Guid.NewGuid().ToString() + extension;
                    filePath = System.IO.Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                       await  file.CopyToAsync(stream);
                    }

                }
                
               MenuItemObj.Image = @"\images\menuitems\" + fileName + extension;
               _unitOfWork.MenuItem.Add(MenuItemObj);
              _unitOfWork.Save();
               return RedirectToAction("Index");
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                MenuItem MenuItemObj = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id);
                if (MenuItemObj == null)
                {
                    return NotFound();
                }

                return View(MenuItemObj);
            }
            return NotFound();
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        public ActionResult Edit(int id, [Bind("Id,Name,DisplayOrder,Image,Price,CategoryId,FoodTypeId")] MenuItem MenuItemObj)
        {

            if (!ModelState.IsValid)
            {
                return View(MenuItemObj);
            }
            else
            {
                _unitOfWork.MenuItem.Update(MenuItemObj);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MenuItemObj = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id);
            if (MenuItemObj == null)
            {
                return NotFound();
            }

            return View(MenuItemObj);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var MenuItemObj = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id);
            _unitOfWork.MenuItem.Remove(MenuItemObj);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

    }
}
