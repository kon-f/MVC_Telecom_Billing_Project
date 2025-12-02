using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCproject.Data;
using MVCproject.Models;

namespace MVCproject.Controllers
{
    /// <summary>
    /// Provides administrative features, including seller management 
    /// and telecom program (plan) creation and modification.
    /// </summary>
    public class AdminController : Controller
    {
        private readonly MVCDBContext _context;

        public AdminController(MVCDBContext context)
        {
            _context = context;
        }

        // ---------------------------------------------------------
        // Dashboard
        // ---------------------------------------------------------

        /// <summary>
        /// Default admin dashboard view.
        /// </summary>
        public IActionResult Index() => View();


        // ---------------------------------------------------------
        // Seller Management
        // ---------------------------------------------------------

        /// <summary>
        /// Displays the form for creating a new seller account.
        /// </summary>
        public IActionResult CreateSeller()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }

        /// <summary>
        /// Creates a new seller user and associated Seller entity.
        /// </summary>
        [HttpPost]
        public IActionResult CreateSeller(string firstName, string lastName, string username, string password)
        {
            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Invalid input data.");
            }

            // Check if username is already taken
            if (_context.Users.Any(u => u.Username == username))
            {
                TempData["ErrorMessage"] = "A seller with this username already exists.";
                return RedirectToAction(nameof(CreateSeller));
            }

            // Create user record
            var user = new User
            {
                First_Name = firstName,
                Last_Name = lastName,
                Username = username,
                Password = password,                   // NOTE: stored in plain text (demo purposes only)
                Property = "Seller"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // Link user to Seller table
            var seller = new Seller
            {
                User_ID = user.User_ID
            };

            _context.Sellers.Add(seller);
            _context.SaveChanges();

            return RedirectToAction(nameof(ListSellers));
        }

        /// <summary>
        /// Shows all existing sellers along with their associated user accounts.
        /// </summary>
        public IActionResult ListSellers()
        {
            var sellers = _context.Sellers
                .Include(s => s.User)
                .ToList();

            return View(sellers);
        }


        // ---------------------------------------------------------
        // Program (Plan) Management
        // ---------------------------------------------------------

        /// <summary>
        /// Displays the form for creating a new telecom program/plan.
        /// </summary>
        public IActionResult CreateProgram()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }

        /// <summary>
        /// Creates a new telecom program (plan).
        /// </summary>
        [HttpPost]
        public IActionResult CreateProgram(string programName, string benefits, decimal charge)
        {
            if (string.IsNullOrWhiteSpace(programName) ||
                string.IsNullOrWhiteSpace(benefits) ||
                charge <= 0)
            {
                return BadRequest("Invalid input data.");
            }

            try
            {
                var program = new Plan
                {
                    ProgramName = programName,
                    Benefits = benefits,
                    Charge = charge
                };

                _context.Plans.Add(program);
                _context.SaveChanges();

                return RedirectToAction(nameof(ModifyProgram));
            }
            catch (DbUpdateException)
            {
                TempData["ErrorMessage"] = "A program with this name already exists.";
                return RedirectToAction(nameof(CreateProgram));
            }
        }

        /// <summary>
        /// Loads all programs so the admin can edit them.
        /// </summary>
        public IActionResult ModifyProgram()
        {
            var programs = _context.Plans.ToList();
            return View(programs);
        }

        /// <summary>
        /// Applies modifications to an existing program.
        /// </summary>
        [HttpPost]
        public IActionResult ModifyProgram(string oldName, string newBenefits, decimal newCharge)
        {
            var program = _context.Plans.FirstOrDefault(p => p.ProgramName == oldName);

            if (program == null)
                return NotFound();

            program.Benefits = newBenefits;
            program.Charge = newCharge;

            _context.Plans.Update(program);
            _context.SaveChanges();

            return RedirectToAction(nameof(ModifyProgram));
        }
    }
}
