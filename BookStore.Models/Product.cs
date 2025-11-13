using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Product:IEntityTypeConfiguration<Product>
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [DisplayName("List Price")]
        [Range(1, 10000, ErrorMessage = "List Price must be between 1 and 10,000")]
        public double ListPrice { get; set; }


        [Required]
        [DisplayName("Price 1-50")]
        [Range(1, 10000, ErrorMessage = "List Price must be between 1 and 10,000")]
        public double price { get; set; }

        [Required]
        [DisplayName("Price 50+")]
        [Range(1, 10000, ErrorMessage = "List Price must be between 1 and 10,000")]
        public double Price50Plus { get; set; }

        [Required]
        [DisplayName("Price 100+")]
        [Range(1, 10000, ErrorMessage = "List Price must be between 1 and 10,000")]
        public double Price100Plus { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string ImgUrl { get; set; }

        public ICollection<CartItem> cartItems { get; set; }= new List<CartItem>();
        public ICollection<OrderItem> OrderItems { get; set; }=new List<OrderItem>();

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasMany(p => p.cartItems)
                    .WithOne(c => c.Product)
                    .HasForeignKey(c=>c.productId);

            builder.HasMany(p => p.OrderItems)
                    .WithOne(o => o.Product)
                    .HasForeignKey(o => o.ProductId);
        }
    }

}
