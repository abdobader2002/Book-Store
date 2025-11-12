using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IReopsitory;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            Product objFromDb = _db.Products.FirstOrDefault(p => p.Id == obj.Id);
            if (objFromDb != null)
            { 
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.price = obj.price;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price50Plus = obj.Price50Plus;
                objFromDb.Price100Plus = obj.Price100Plus;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.Author=obj.Author;

                if(objFromDb.ImgUrl != null)
                {
                    objFromDb.ImgUrl = obj.ImgUrl;
                }
            }
            _db.Products.Update(objFromDb);
        }
    }
}
