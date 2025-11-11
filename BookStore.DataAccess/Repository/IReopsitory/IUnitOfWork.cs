using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository.IReopsitory
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get;  }
        IProductRepository Product { get; }
        void Save();
    }
}
