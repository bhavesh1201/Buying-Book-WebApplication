using BulkyWeb.Models;
using BulkyWeb.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =StaticDataRoles.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _db;

        public CategoryController(IUnitOfWork db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            var cs = _db.Category.GetAll();

            return View(cs);
        }
        public IActionResult Create()
        {

            return View();

        }
        [HttpPost]
        public IActionResult Create(Category ct)
        {

            if (ct.Name == ct.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display order cannot be same");
            }

            if (ModelState.IsValid)
            {
                _db.Category.Add(ct);
                _db.Save();
                TempData["Success"] = "Inserted Successfully";

                return RedirectToAction("Index");

            }





            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var vx = _db.Category.Get(a => a.Id == id);
            if (vx == null) return NotFound();




            return View(vx);

        }
        [HttpPost]
        public IActionResult Edit(Category ct)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Update(ct);
                _db.Save();
                TempData["SuccessD"] = "Updated Successfully";


                return RedirectToAction("Index");

            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var vx = _db.Category.Get(a => a.Id == id);
            if (vx == null) return NotFound();
            return View(vx);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteP(int? id)
        {

            Category? vx = _db.Category.Get(a => a.Id == id);
            if (vx == null) return NotFound();

            _db.Category.Delete(vx);
            _db.Save();
            TempData["SuccessD"] = "Deleted Successfully";

            //if (ModelState.IsValid)
            //{
            //    _db.categories.Update(ct);
            //    _db.SaveChanges();
            //    return RedirectToAction("Index");

            //}
            return RedirectToAction("Index");
        }



    }
}
