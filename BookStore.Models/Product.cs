using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }= string.Empty;
        public string Description { get; set; }= string.Empty;
        [Required]
        public string ISBN { get; set; }= string.Empty;

        [Required]
        public string Author { get; set; }= string.Empty;

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
        public Category Category { get; set; }

    }
}
