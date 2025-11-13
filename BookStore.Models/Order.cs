using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Order:IEntityTypeConfiguration<Order>
    {
        public int Id { get; set; }

        [Required]
        public string CustomerUserId { get; set; }   
        public UserCustomer CustomerUser { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public double TotalAmount { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


        public string Status { get; set; } = "Pending";

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(o => o.OrderItems)
                    .WithOne(i => i.Order)
                    .HasForeignKey(i => i.OrderId);
        }
    }
}
