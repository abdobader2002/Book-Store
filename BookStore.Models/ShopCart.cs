using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class ShopCart:IEntityTypeConfiguration<ShopCart>
    {
        public int Id { get; set; }

        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
        public string userID { get; set; }
        [ForeignKey("userID")]
        [ValidateNever]
        public UserCustomer User { get; set; }

        public void Configure(EntityTypeBuilder<ShopCart> builder)
        {
            builder.HasMany(s => s.Items)
                    .WithOne(c => c.Cart)
                    .HasForeignKey(c => c.CartId);
        }
    }
}
