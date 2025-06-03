using Microsoft.AspNetCore.Mvc;

namespace MVCproject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");

            switch (role)
            {
                case "Admin":
                    return View("AdminMenu");
                case "Client":
                    return View("ClientMenu");
                case "Seller":
                    return View("SellerMenu");
                default:
                    return RedirectToAction("Login", "Login");
            }
        }
    }
}
