using BulkyWeb.Models;
using BulkyWeb.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;

namespace BulkyWeb.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = StaticDataRoles.Role_Admin)]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _context;
        private readonly IWebHostEnvironment _webHost;

        public ProductController(IUnitOfWork context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }
        public IActionResult Index()
        {


            var vz = _context.Product.GetAll(prop:"category");

            IEnumerable<SelectListItem> CategoryList = _context.Category.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()

            });




            return View(vz);
        }

        public IActionResult Create()
        {

            IEnumerable<SelectListItem> CategoryList = _context.Category.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()

            });

            ProductVM pp = new ProductVM();
            pp.CategoryList = CategoryList;


            ViewBag.Category = CategoryList;

            return View(pp);
        }

        [HttpPost]
        public IActionResult Create(ProductVM prod, IFormFile formFile)
        {

            if (ModelState.IsValid)
            {


                string wwwRoot = _webHost.WebRootPath;

                if (formFile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                    string productPath = Path.Combine(wwwRoot, @"images\product");


                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.OpenOrCreate))
                    {

                        formFile.CopyTo(fileStream);

                    }


                    prod.product.imageUrl = @"images\product\" + fileName;





                }

                _context.Product.Add(prod.product);
                _context.Save();
                return RedirectToAction("Index");

            }
            else
            {
                IEnumerable<SelectListItem> CategoryList = _context.Category.GetAll().Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()

                });

                ProductVM pp = new ProductVM();
                pp.CategoryList = CategoryList;

                return View(pp);


            }



        }

        public IActionResult Edit(int? Id)
        {





            if (Id == null || Id == 0) return NotFound();

            var vz = _context.Product.Get(A => A.Id == Id);

            IEnumerable<SelectListItem> CategoryList = _context.Category.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()

            });

            ViewBag.CategoryList = CategoryList;

            if (vz == null) return NotFound();




            return View(vz);
        }

        [HttpPost]
        public IActionResult Edit(Product prod, IFormFile? formFile)
        {

            string wwwRoot = _webHost.WebRootPath;

            if (formFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                string productPath = Path.Combine(wwwRoot, @"images\product");
                if (!string.IsNullOrEmpty(prod.imageUrl))
                {
                    var oldPath = Path.Combine(wwwRoot, prod.imageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }

                }


                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.OpenOrCreate))
                {

                    formFile.CopyTo(fileStream);

                }


                prod.imageUrl = @"images\product\" + fileName;
            }

            _context.Product.Update(prod);
            _context.Save();
            return RedirectToAction("Index");
        }

        //public IActionResult Delete(int? Id)
        //{

        //    if (Id == null || Id == 0) return NotFound();

        //    var vz = _context.Product.Get(A => A.Id == Id);

        //    if (vz == null) return NotFound();


        //    return View(vz);
        //}

        //[HttpPost]
        //public IActionResult Delete(Product prod)
        //{

        //    _context.Product.Delete(prod);
        //    _context.Save();
        //    return RedirectToAction("Index");
        //}
        #region

        [HttpGet]
        public IActionResult GetAll()
        {
            var vz = _context.Product.GetAll(prop:"category");
            return Json(new {data=vz});
        }



        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var temp= _context.Product.Get(A => A.Id == id);
            if(temp == null)
            {
                return NotFound();
          }
            var oldPath = Path.Combine(_webHost.WebRootPath, temp.imageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }
            _context.Product.Delete(temp);
            _context.Save();

           
            return Json(new {success= true , Message= "deleted successfully" });
        }
        #endregion 

    }
}
