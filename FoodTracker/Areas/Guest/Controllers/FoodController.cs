﻿using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.Food;
using FoodTracker.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{
    [Area("Guest")]
    [Authorize]
    public class FoodController(IUnitOfWork unitOfWork) : Controller
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public FoodVM FoodVM { get; set; }

        public IActionResult Index()
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            FoodVM = new()
            {
                FoodList = _unitOfWork.Food.GetAll(f => f.AppUserId == userId || f.Global == true, includeProperties: "Fodmap"),
                Food = new Food() { Id = 0 },
                FodmapList = _unitOfWork.Fodmap.GetAll(includeProperties: "Aliases,Category,Color,MaxUseUnits"),
            };

            return View(FoodVM);
        }

        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return Json(new { success = false });
            }
            var foodToRemove = _unitOfWork.Food.Get(f => f.Id == id);
            if (foodToRemove != null)
            {
                _unitOfWork.Food.Remove(foodToRemove);
                _unitOfWork.Save();
            }

            return Json(new { success = true });
        }


        [HttpPost]
        public IActionResult Index(Food food)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                food.AppUserId = userId;

                if (food.Id == 0)
                {
                    _unitOfWork.Food.Add(food);
                }
                else
                {
                    _unitOfWork.Food.Update(food);
                }
            }
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult GetFoodDetails(string foodName)
        {

            // can be either identified or not

            // if identified, prepopulate with info and food id

            // if not, should be empty with id = 0

            return PartialView("_AddFoodPartial", FoodVM);
        }
    }
}
