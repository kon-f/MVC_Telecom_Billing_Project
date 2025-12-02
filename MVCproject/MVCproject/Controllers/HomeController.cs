using Microsoft.AspNetCore.Mvc;

namespace MVCproject.Controllers
{
    /// <summary>
    /// Routes authenticated users to the appropriate dashboard
    /// based on their assigned role (Admin, Client, Seller).
    /// </summary>
    public class HomeController : Controller
    {
        // Role constants for consistency and typo safety
        private const string AdminRole = "Admin";
        private const string ClientRole = "Client";
        private const string SellerRole = "Seller";

        /// <summary>
        /// Entry point after login. Determines which dashboard to display
        /// according to the user's role stored in session data.
        /// </summary>
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(role))
            {
                // Session missing → user not authenticated
                return RedirectToAction("Login", "Login");
            }

            return role switch
            {
                AdminRole => View("AdminMenu"),
                ClientRole => View("ClientMenu"),
                SellerRole => View("SellerMenu"),
                _ => RedirectToAction("Login", "Login") // Undefined role fallback
            };
        }
    }
}
