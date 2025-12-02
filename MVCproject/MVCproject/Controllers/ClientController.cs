using Microsoft.AspNetCore.Mvc;
using MVCproject.Data;
using System.Linq;
using MVCproject.Models;

namespace MVCproject.Controllers
{
    /// <summary>
    /// Handles client functionalities such as viewing call history,
    /// checking bills, and paying charges.
    /// </summary>
    public class ClientController : Controller
    {
        private readonly MVCDBContext _context;

        public ClientController(MVCDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Default client dashboard.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays the client's call history based on their associated bills.
        /// </summary>
        public IActionResult ViewCallHistory()
        {
            int clientId = GetLoggedInClientId();

            var client = _context.Clients.FirstOrDefault(c => c.User_ID == clientId);
            if (client == null)
            {
                return NotFound("Client not found.");
            }

            // All bills matching this client's phone number
            var billIds = _context.Bills
                .Where(b => b.PhoneNumber == client.PhoneNumber)
                .Select(b => b.Bill_ID)
                .ToList();

            // All call IDs linked to the client's bills
            var callIds = _context.BillsCalls
                .Where(bc => billIds.Contains(bc.Bill_ID))
                .Select(bc => bc.Call_ID)
                .ToList();

            // Finally find calls from Calls table
            var calls = _context.Calls
                .Where(c => callIds.Contains(c.Call_ID))
                .ToList();

            return View(calls);
        }

        /// <summary>
        /// Displays all bills associated with the logged-in client.
        /// </summary>
        public IActionResult ViewBills()
        {
            int clientId = GetLoggedInClientId();
            if (clientId == 0)
            {
                return RedirectToAction(nameof(LoginController.Login), "Login");
            }

            var client = _context.Clients.FirstOrDefault(c => c.User_ID == clientId);

            if (client == null)
            {
                return NotFound("Client not found.");
            }

            var bills = _context.Bills.Where(b => b.PhoneNumber == client.PhoneNumber).ToList();
            return View(bills);
        }

        /// <summary>
        /// Allows a client to pay a bill they owe.
        /// </summary>
        [HttpPost]
        public IActionResult PayBill(int billId)
        {
            int clientId = GetLoggedInClientId();
            var client = _context.Clients.FirstOrDefault(c => c.User_ID == clientId);

            if (client == null)
            {
                return NotFound("Client not found.");
            }

            // Find the bill
            var bill = _context.Bills.FirstOrDefault(b => b.Bill_ID == billId && b.PhoneNumber == client.PhoneNumber);

            if (bill == null)
            {
                return NotFound("Bill not found or doesn't belong to this client.");
            }

            if (!bill.Paid)
            {
                bill.Paid = true;
                _context.Bills.Update(bill);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(ViewBills));
        }

        /// <summary>
        /// Retrieves the logged-in user's ID from the session.
        /// Returns 0 if the user is not authenticated.
        /// </summary>
        private int GetLoggedInClientId()
        {            
            var userIdString = HttpContext.Session.GetString("UserId");
            if (int.TryParse(userIdString, out int userId))
            {
                return userId;
            }

            return 0;
        }
    }
}
