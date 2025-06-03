using Microsoft.AspNetCore.Mvc;
using MVCproject.Models;
using System.Linq;
using MVCproject.Data;

namespace MVCproject.Controllers
{
    public class LoginController : Controller
    {
        private readonly MVCDBContext _context;

        public LoginController(MVCDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Find user with credentials
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Find user's role from Property column
                HttpContext.Session.SetString("Role", user.Property); // Save it in Session
                HttpContext.Session.SetString("UserId", user.User_ID.ToString());

                return RedirectToAction("Index", "Home"); // Redirect to Home page
            }

            // Show error if credentials are incorrect
            ViewBag.Error = "Invalid username or password";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Delete session data
            return RedirectToAction("Login", "Login"); // Redirect to login
        }

    }
}

