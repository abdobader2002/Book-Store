using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BookStore.DataAccess.Data
{
    public class ApplicationDbContext :IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserCustomer> Customers { get; set; }
        public DbSet<ShopCart> ShopCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Fortune of Time",
                    Author = "Billy Spark",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ISBN = "SWD9999001",
                    ListPrice = 99.00,
                    price = 90.00,
                    Price50Plus = 85.00,
                    Price100Plus = 80.00,
                    CategoryId = 2,
                    ImgUrl=""
                    },
                new Product 
                                {
                    Id = 2,
                    Title = "Dark Skies",
                    Author = "Nancy Hoover",
                    Description = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                    ISBN = "CAW777777701",
                    ListPrice = 40.00,
                    price = 30.00,
                    Price50Plus = 25.00,
                    Price100Plus = 20.00,
                    CategoryId = 1,
                    ImgUrl=""
                },
                new Product
                {
                    Id = 3,
                    Title = "Vanish in the Sunset",
                    Author = "Julian Button",
                    Description = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                    ISBN = "RITO5555501",
                    ListPrice = 55.00,
                    price = 50.00,
                    Price50Plus = 40.00,
                    Price100Plus = 35.00,
                    CategoryId = 1,
                    ImgUrl=""
                },
                new Product
                {
                    Id = 4,
                    Title = "Cotton Candy",
                    Author = "Abby Muscles",
                    Description = "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    ISBN = "WS3333333301",
                    ListPrice = 70.00,
                    price = 65.00,
                    Price50Plus = 60.00,
                    Price100Plus = 55.00,
                    CategoryId = 2,
                    ImgUrl=""
                },
                new Product
                {
                    Id = 5,
                    Title = "Rock in the Ocean",
                    Author = "Ron Parker",
                    Description = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam.",
                    ISBN = "SOTJ1111111101",
                    ListPrice = 30.00,
                    price = 27.00,
                    Price50Plus = 25.00,
                    Price100Plus = 20.00,
                    CategoryId = 2,
                    ImgUrl = ""
                }
                );

        }


    }
}
