using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class UserCustomer:ApplicationUser
    {
        public string Role { get; set; } = "Customer";
        public ICollection<CustomerAddress> Addresses { get; set; } = new List<CustomerAddress>();
    }
}
