using Microsoft.AspNetCore.Mvc;

namespace Book_Store_MVC_Project.Areas.Identitty.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
