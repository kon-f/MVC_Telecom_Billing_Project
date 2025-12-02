using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCproject.Data;
using MVCproject.Models;

namespace MVCproject.Controllers
{
    /// <summary>
    /// Handles all seller operations such as registering clients,
    /// issuing bills, and modifying existing client plans.
    /// </summary>
    public class SellerController : Controller
    {
        private readonly MVCDBContext _context;

        public SellerController(MVCDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Default seller dashboard.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays the client registration form.
        /// </summary>
        public IActionResult RegisterClient()
        {
            return View();
        }

        /// <summary>
        /// Registers a new client (User + Phone + Client records).
        /// Performs username and phone-number uniqueness checks.
        /// </summary>
        [HttpPost]
        public IActionResult RegisterClient(string firstName, string lastName, string username, string password, string afm, string phoneNumber)
        {
            // Basic validation
            if (string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(afm) ||
                string.IsNullOrEmpty(phoneNumber))
            {
                return View();
            }

            try
            {
                // Username uniqueness check
                if (_context.Users.Any(u => u.Username == username))
                {
                    ModelState.AddModelError("Username", "Username already exists.");
                    return View();
                }

                // Phone number uniqueness check
                if (_context.Phones.Any(p => p.PhoneNumber == phoneNumber))
                {
                    ModelState.AddModelError("PhoneNumber", "Phone number already exists.");
                    return View();
                }

                // Create User entity
                var user = new User
                {
                    First_Name = firstName,
                    Last_Name = lastName,
                    Username = username,
                    Password = password, // Password hashing intentionally omitted (demo project)
                    Property = "Client"
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                // Create Phone entry with default program
                var phone = new Phone
                {
                    PhoneNumber = phoneNumber,
                    Program_Name = _context.Plans.FirstOrDefault()?.ProgramName
                                   ?? "Basic Program"
                };

                _context.Phones.Add(phone);
                _context.SaveChanges();

                // Create Client entry linked to user & phone
                var client = new Client
                {
                    User_ID = user.User_ID,
                    AFM = afm,
                    PhoneNumber = phoneNumber
                };

                _context.Clients.Add(client);
                _context.SaveChanges();

                return RedirectToAction(nameof(ChangeClientPlan));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View();
            }
        }

        /// <summary>
        /// Displays the bill issuing form.
        /// </summary>
        public IActionResult IssueBill()
        {
            return View();
        }

        /// <summary>
        /// Issues a new bill for a specific client based on phone number.
        /// </summary>
        [HttpPost]
        public IActionResult IssueBill(string phoneNumber, decimal costs)
        {
            var client = _context.Clients.FirstOrDefault(c => c.PhoneNumber == phoneNumber);

            if (client == null)
            {
                return NotFound("Client with the specified phone number not found.");
            }

            var bill = new Bill
            {
                PhoneNumber = client.PhoneNumber,
                Costs = costs,
                Paid = false     // Default: unpaid bill
            };

            _context.Bills.Add(bill);
            _context.SaveChanges();

            return View(nameof(IssueBill));
        }

        /// <summary>
        /// Lists all clients along with their current program
        /// and related user information. Used for plan editing.
        /// </summary>
        public IActionResult ChangeClientPlan()
        {
            //All clients in a list
            var clients = _context.Clients.ToList();
            //Users in a dictionary
            var users = _context.Users.ToDictionary(u => u.User_ID, u => new { u.First_Name, u.Last_Name });
            // And Phones table content
            var phones = _context.Phones.ToDictionary(p => p.PhoneNumber, p => p.Program_Name);

            // Create ViewModel with info for user and program
            var clientDetails = clients.Select(c => new
            {
                c.Client_ID,
                c.PhoneNumber,
                FirstName = users.ContainsKey(c.User_ID) ? users[c.User_ID].First_Name : "Unknown",
                LastName = users.ContainsKey(c.User_ID) ? users[c.User_ID].Last_Name : "Unknown",
                CurrentPlan = phones.ContainsKey(c.PhoneNumber) ? phones[c.PhoneNumber] : "No Plan"
            }).ToList();

            // We get the available programs
            var plans = _context.Plans.ToList();

            // We send the data to View
            ViewBag.Plans = plans;
            return View(clientDetails);
        }

        /// <summary>
        /// Updates the selected client's program.
        /// </summary>
        [HttpPost]
        public IActionResult ChangeClientPlan(string phoneNumber, string planName)
        {
            // Find client's phone number based on phoneNumber given
            var phone = _context.Phones.FirstOrDefault(p => p.PhoneNumber == phoneNumber);

            if (phone == null)
            {
                return NotFound("Phone number not found.");
            }

            phone.Program_Name = planName;

            _context.Phones.Update(phone);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Client plan updated successfully!";
            return RedirectToAction(nameof(ChangeClientPlan));
        }
    }
}
