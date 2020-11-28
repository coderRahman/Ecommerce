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
    public class FoodTypeController : Controller
    {
        // GET: CategoryController

        private readonly IUnitOfWork _unitOfWork;

        public FoodTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public JsonResult GetAll()
        {
            IEnumerable<FoodType> foodTypes = (IEnumerable<FoodType>)_unitOfWork.FoodType.GetAll();
            return Json(new { data = foodTypes });
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
        public ActionResult Create(FoodType FoodTypeObj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            else
            {
                _unitOfWork.FoodType.Add(FoodTypeObj);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                FoodType FoodTypeObj = _unitOfWork.FoodType.GetFirstOrDefault(u => u.Id == id);
                if (FoodTypeObj == null)
                {
                    return NotFound();
                }

                return View(FoodTypeObj);
            }
            return NotFound();
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        public ActionResult Edit(int id, [Bind("Id,Name,Description,")] FoodType FoodTypeObj)
        {

            if (!ModelState.IsValid)
            {
                return View(FoodTypeObj);
            }
            else
            {
                _unitOfWork.FoodType.Update(FoodTypeObj);
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

            var FoodTypeObj = _unitOfWork.FoodType.GetFirstOrDefault(u => u.Id == id);
            if (FoodTypeObj == null)
            {
                return NotFound();
            }

            return View(FoodTypeObj);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var FoodTypeObj = _unitOfWork.FoodType.GetFirstOrDefault(u => u.Id == id);
            _unitOfWork.FoodType.Remove(FoodTypeObj);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

    }
}
