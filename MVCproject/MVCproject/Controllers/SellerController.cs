using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCproject.Data;
using MVCproject.Models;

namespace MVCproject.Controllers
{
    public class SellerController : Controller
    {
        private readonly MVCDBContext _context;

        public SellerController(MVCDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegisterClient()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterClient(string firstName, string lastName, string username, string password, string afm, string phoneNumber)
        {
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(afm) && !string.IsNullOrEmpty(phoneNumber))
            {
                try
                {
                    // Check if username is unique
                    if (_context.Users.Any(u => u.Username == username))
                    {
                        ModelState.AddModelError("Username", "Username already exists.");
                        return View();
                    }

                    // Check if number is unique
                    if (_context.Phones.Any(p => p.PhoneNumber == phoneNumber))
                    {
                        ModelState.AddModelError("PhoneNumber", "Phone number already exists.");
                        return View();
                    }

                    // Create new user
                    var user = new User
                    {
                        First_Name = firstName,
                        Last_Name = lastName,
                        Username = username,
                        Password = password,
                        Property = "Client",
                    };

                    _context.Users.Add(user);
                    _context.SaveChanges();

                    // Add him in Phones table
                    var phone = new Phone
                    {
                        PhoneNumber = phoneNumber,
                        Program_Name = _context.Plans.FirstOrDefault()?.ProgramName ?? "Basic Program" // Ensure valid default
                    };

                    _context.Phones.Add(phone);
                    _context.SaveChanges();

                    // Add him in Clients table
                    var client = new Client
                    {
                        User_ID = user.User_ID,
                        AFM = afm,
                        PhoneNumber = phoneNumber
                    };

                    _context.Clients.Add(client);
                    _context.SaveChanges();                    

                    return RedirectToAction("ChangeClientPlan");
                }
                catch (Exception ex)
                {
                    // Catch errors
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                    return View();
                }
            }

            return View();
        }

        public IActionResult IssueBill()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IssueBill(string phoneNumber, decimal costs)
        {
            var client = _context.Clients.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
            if (client != null)
            {
                var bill = new Bill
                {
                    PhoneNumber = client.PhoneNumber,
                    Costs = costs,
                    Paid = false // Originally not paid
                };

                _context.Bills.Add(bill);
                _context.SaveChanges();
                return View("IssueBill");
            }

            return NotFound("Client with the specified phone number not found.");
        }

        public IActionResult ChangeClientPlan()
        {
            //All clients in a list
            var clients = _context.Clients.ToList();
            //Users
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

        [HttpPost]
        public IActionResult ChangeClientPlan(string phoneNumber, string planName)
        {
            // Find client's phone number based on phoneNumber given
            var phone = _context.Phones.FirstOrDefault(p => p.PhoneNumber == phoneNumber);

            if (phone != null)
            {
                // Update program
                phone.Program_Name = planName;

                // Update changes
                _context.Phones.Update(phone);
                _context.SaveChanges();

                // Return success message
                TempData["SuccessMessage"] = "Client plan updated successfully!";
                return RedirectToAction("ChangeClientPlan");
            }

            // If phone is not found
            return NotFound("Phone number not found.");
        }
    }
}
