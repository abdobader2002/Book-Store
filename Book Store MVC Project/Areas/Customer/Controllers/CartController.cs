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
                if (quantity > 0)
                {
                    cartItem.productCount = quantity;
                }
                else
                {
                    cartItem.productCount += quantity;
                }
                    
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
        [HttpGet]
        public IActionResult Checkout()
        {
            var userId = _userManager.GetUserId(User);

            var cart = _context.ShopCarts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.userID == userId);
            CustomerAddress customerAddress = _context.CustomerAddresses
                .FirstOrDefault(ca => ca.UserId == userId && ca.IsDefault);

            TempData["CustomerAddress"] = customerAddress != null
                ? $"{customerAddress.StreetAddress}, {customerAddress.City}, {customerAddress.State}, {customerAddress.PostalCode}"
                : " ";
            if (cart == null || !cart.Items.Any())
                return RedirectToAction("Index"); 

            return View(cart);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckoutPost()
        {
            var userId = _userManager.GetUserId(User);

            var cart = _context.ShopCarts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.userID == userId);

            if (cart == null || !cart.Items.Any())
                return RedirectToAction("Index");

            var order = new Order
            {
                CustomerUserId = userId,
                OrderDate = DateTime.Now,
                Status = "Pending"
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            double total = 0;

            foreach (var item in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.productId,
                    Quantity = item.productCount,
                    ProductUnitPrice = item.Product.price
                };

                total += orderItem.ProductUnitPrice * orderItem.Quantity;

                _context.OrderItems.Add(orderItem);
            }

            order.TotalAmount = total;
            _context.SaveChanges();

            _context.CartItems.RemoveRange(cart.Items);
            _context.SaveChanges();

            return RedirectToAction("OrderConfirmation", new { id = order.Id });
        }
        public IActionResult OrderConfirmation(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        public IActionResult OrderHistory()
        {
            var userId = _userManager.GetUserId(User);
            var orders = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.CustomerUserId == userId)
                .ToList();
            return View(orders);
        }
        public IActionResult OrderDetails(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound();
            return View(order);
        }



    }
}
