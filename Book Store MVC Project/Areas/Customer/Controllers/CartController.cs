using BookStore.DataAccess.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Store_MVC_Project.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")] 
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var cart = await _context.ShopCarts
                .Include(c => c.Items)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.userID == user.Id);

            if (cart == null || !cart.Items.Any())
            {
                ViewBag.Message = "Your cart is empty.";
                return View();
            }

            return View(cart);
        }


        [HttpPost]
        public async Task<IActionResult> Add(int productId, int quantity = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();


            var cart = await _context.ShopCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.userID == user.Id);

            if (cart == null)
            {
                cart = new ShopCart
                {
                    userID = user.Id
                };
                _context.ShopCarts.Add(cart);
                await _context.SaveChangesAsync(); 
            }


            var cartItem = cart.Items.FirstOrDefault(ci => ci.productId == productId);
            if (cartItem != null)
            {
                cartItem.productCount += quantity;
                _context.CartItems.Update(cartItem);
            }
            else
            {
                var product = await _context.Products.FindAsync(productId);
                if (product == null)
                    return NotFound();

                cartItem = new CartItem
                {
                    productId = productId,
                    productCount = quantity,
                    CartId = cart.Id
                };
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Remove(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
