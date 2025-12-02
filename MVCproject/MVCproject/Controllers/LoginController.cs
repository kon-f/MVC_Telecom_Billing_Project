using Microsoft.AspNetCore.Mvc;
using MVCproject.Data;
using MVCproject.Models;

namespace MVCproject.Controllers
{
    /// <summary>
    /// Handles user authentication and session-based login/logout functionality.
    /// </summary>
    public class LoginController : Controller
    {
        private readonly MVCDBContext _context;

        public LoginController(MVCDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays the login page.
        /// </summary>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Attempts to authenticate the user using the provided credentials.
        /// </summary>
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Username and password are required.";
                return View();
            }

            // Lookup user using basic credential matching 
            // (Note: In a real application, passwords should be hashed/salted. This was a very small project that did not require complete authentication. 
            // More appropriate encryption has been implemented in other projects)
            var user = _context.Users
                .FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            // Store essential user info in session
            HttpContext.Session.SetString("Role", user.Property);  // 'Property' used as role indicator
            HttpContext.Session.SetString("UserId", user.User_ID.ToString());

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Logs out the current user and clears all session data.
        /// </summary>
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}
