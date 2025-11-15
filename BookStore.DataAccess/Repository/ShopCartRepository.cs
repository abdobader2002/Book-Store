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
    public class ShopCartRepository : Repository<ShopCart>, IShopCartRepository
    {

        private ApplicationDbContext _db;
        public ShopCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ShopCart obj)
        {
            _db.ShopCarts.Update(obj);
        }
    }
}
