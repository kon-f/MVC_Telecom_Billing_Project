using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCproject.Data;
using MVCproject.Models;

namespace MVCproject.Controllers
{
    public class AdminController : Controller
    {
        private readonly MVCDBContext _context;

        public AdminController(MVCDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateSeller()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }

        [HttpPost]
        public IActionResult CreateSeller(string firstName, string lastName, string username, string password)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(username))
            {
                return BadRequest("Invalid input data.");               
            }

            // Check if there is a seller with the same name
            if (_context.Users.Any(u => u.Username == username))
            {
                TempData["ErrorMessage"] = "A seller with this username already exists.";
                return RedirectToAction("CreateSeller");
            }

            var user = new User
            {
                First_Name = firstName,
                Last_Name = lastName,
                Username = username,
                Property = "Seller",
                Password = password,
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var seller = new Seller
            {
                User_ID = user.User_ID
            };

            _context.Sellers.Add(seller);
            _context.SaveChanges();

            return RedirectToAction("ListSellers");
        }

        public IActionResult ListSellers()
        {
            var sellers = _context.Sellers
                .Include(s => s.User) // Load related data from Users table
                .ToList();

            return View(sellers);
        }

        public IActionResult CreateProgram()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }

        [HttpPost]
        public IActionResult CreateProgram(string programName, string benefits, decimal charge)
        {
            if (string.IsNullOrWhiteSpace(programName) || string.IsNullOrWhiteSpace(benefits) || charge <= 0)
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

                return RedirectToAction("ModifyProgram");
            }
            catch (DbUpdateException)
            {
                TempData["ErrorMessage"] = "A program with this name already exists.";
                return RedirectToAction("CreateProgram");
            }            
        }     

        public IActionResult ModifyProgram()
        {
            var programs = _context.Plans.ToList();
            return View(programs);
        }

        [HttpPost]
        public IActionResult ModifyProgram(string oldName, string newBenefits, decimal newCharge)
        {
            // Find program based on it's name
            var program = _context.Plans.FirstOrDefault(p => p.ProgramName == oldName);
            if (program != null)
            {              
                // Update values
                program.Benefits = newBenefits;
                program.Charge = newCharge;

                _context.Plans.Update(program);
                _context.SaveChanges();

                return RedirectToAction("ModifyProgram");
            }

            return NotFound();
        }
    }
}
