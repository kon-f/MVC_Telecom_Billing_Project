using Microsoft.AspNetCore.Mvc;
using MVCproject.Data;
using MVCproject.Models;
using System.Linq;

namespace MVCproject.Controllers
{
    public class ClientController : Controller
    {
        private readonly MVCDBContext _context;

        public ClientController(MVCDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewCallHistory()
        {
            int clientId = GetLoggedInClientId();
            var client = _context.Clients.FirstOrDefault(c => c.User_ID == clientId);

            if (client == null)
            {
                return NotFound("Client not found.");
            }

            // Find Bill_ID for PhoneNumber of client
            var billIds = _context.Bills
                .Where(b => b.PhoneNumber == client.PhoneNumber)
                .Select(b => b.Bill_ID)
                .ToList();

            // Find Call_ID from BillsCalls table
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

        public IActionResult ViewBills()
        {
            int clientId = GetLoggedInClientId();
            if (clientId == 0)
            {
                return RedirectToAction("Login", "Login");
            }

            var client = _context.Clients.FirstOrDefault(c => c.User_ID == clientId);

            if (client == null)
            {
                return NotFound("Client not found.");
            }

            var bills = _context.Bills.Where(b => b.PhoneNumber == client.PhoneNumber).ToList();
            return View(bills);
        }


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

            return RedirectToAction("ViewBills");
        }

        private int GetLoggedInClientId()
        {
            // Get User_ID from session
            var userIdString = HttpContext.Session.GetString("UserId");
            if (int.TryParse(userIdString, out int userId))
            {
                return userId;
            }

            return 0;
        }
    }
}
