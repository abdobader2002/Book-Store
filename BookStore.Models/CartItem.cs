using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Range(0, 1000, ErrorMessage = "1-1000 items")]
        public int productCount { get; set; }

        public int productId { get; set; }
        [ForeignKey("productId")]
        [ValidateNever]
        public Product Product { get; set; }

        public int CartId { get; set; }
        [ForeignKey("CartId")]
        [ValidateNever]
        public ShopCart Cart { get; set; }

    }
}
