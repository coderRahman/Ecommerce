using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Data.Repository.IRepository;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    [Authorize(Roles = SD.ManagerRole)]
    public class CategoryController : Controller
    {
        // GET: CategoryController

        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public JsonResult GetAll()
        {
            IEnumerable<Category> categories = (IEnumerable<Category>)_unitOfWork.Category.GetAll();
            return Json(new { data = categories });
        }
        public IActionResult Index()
        {
            return View();

        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category CategoryObj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            else
            {
                _unitOfWork.Category.Add(CategoryObj);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                Category CategoryObj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
                if (CategoryObj == null)
                {
                    return NotFound();
                }

                return View(CategoryObj);
            }
            return NotFound();
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        public ActionResult Edit(int id, [Bind("Id,Name,DisplayOrder")] Category CategoryObj)
        {
          
            if (!ModelState.IsValid)
            {
                return View(CategoryObj);
            }
            else
            {
                _unitOfWork.Category.Update(CategoryObj);
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

            var CategoryObj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (CategoryObj == null)
            {
                return NotFound();
            }

            return View(CategoryObj);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var CategoryObj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            _unitOfWork.Category.Remove(CategoryObj);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

    }
}
