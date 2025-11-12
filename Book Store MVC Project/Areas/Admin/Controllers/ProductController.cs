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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            

            return View(objProductList);
        }
        public IActionResult Upsert(int? Id)
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
            if (Id == null || Id == 0)
            {
                return View(productViewModel);
            }
            else
            {
                productViewModel.Product = _unitOfWork.Product.Get(p=>p.Id == Id);
                return View(productViewModel);
            }
            
        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel productVM,IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if(file != null)
            {
                string FileName = Guid.NewGuid().ToString()+ Path.GetExtension(file.FileName);
                string ProductPath = Path.Combine(wwwRootPath, @"images\products");
                if (!string.IsNullOrEmpty(productVM.Product.ImgUrl))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImgUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var FileStream = new FileStream(Path.Combine(ProductPath+FileName),FileMode.Create))
                {
                    file.CopyTo(FileStream);
                }
                string imagPath= @"\images\products" + FileName;
                productVM.Product.ImgUrl = imagPath;
            }
            
            if (ModelState.IsValid)
            {
                if(productVM.Product.Id==0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }

                    _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(c =>
                new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }
                );
                return View(productVM);
            }
            

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
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new {data =objProductList});
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                                        productToBeDeleted.ImgUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Success" });

        }
    }
}
