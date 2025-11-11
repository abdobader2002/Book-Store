using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository;
using BookStore.DataAccess.Repository.IReopsitory;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Book_Store_MVC_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            

            return View(objProductList);
        }
        public IActionResult Create()
        {
            //ViewBag.Categories = Categories;
            ProductViewModel productViewModel = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(c =>
                new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }
                ),
                Product = new Product()
            };
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Create(Product obj)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
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
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            //Product? productFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Product? productFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
       

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                 TempData["success"] = "Product Updated successfully";
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
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            //Product? productFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Product? productFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
             TempData["success"] = "Product Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
