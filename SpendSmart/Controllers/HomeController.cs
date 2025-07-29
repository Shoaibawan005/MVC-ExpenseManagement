using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpendSmart.Models;
using System.Diagnostics;

namespace SpendSmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ExpensesDbContext _expensesDbContext;
        public HomeController(ILogger<HomeController> logger, ExpensesDbContext expensesDbContext)
        {
            _logger = logger;
            _expensesDbContext = expensesDbContext;
        }

        public IActionResult Index()
        {
            SeedDummyExpenses();
            return View();
        }

        private void SeedDummyExpenses()
        {
            if (!_expensesDbContext.Expenses.Any())
            {
                var dummyExpenses = new List<Expenses>
            {
                new Expenses { Value = 254.36m, Description = "Lunch at Cafe" },
                new Expenses { Value = 250.45m, Description = "Grocery Shopping" },
                new Expenses { Value = 19.67m, Description = "Mobile Recharge" },
                new Expenses { Value = 16.49m, Description = "Fuel" },
                new Expenses { Value = 15.98m, Description = "Electricity Bill" },
                new Expenses { Value = 49.91m,  Description = "Snacks & Tea" },
                new Expenses { Value = 8.09m, Description = "Movie Night" },
                new Expenses { Value = 54.29m, Description = "Monthly Rent" },
                new Expenses { Value = 18.99m,  Description = "Laundry Service" },
                new Expenses { Value = 9.99m, Description = "Gym Membership" }
            };

                _expensesDbContext.Expenses.AddRange(dummyExpenses);
                _expensesDbContext.SaveChanges();
            }
        }

        public IActionResult Expenses()
        {
            var allExpenses = _expensesDbContext.Expenses.ToList();
            var totalExpense = allExpenses.Sum(x => x.Value);
            ViewBag.Expenses = totalExpense;
            return View(allExpenses);
        }
        public IActionResult CreateEditExpense(int? id)
        {
            if (id != null) {
                //editing -> load item by id 

                var item = _expensesDbContext.Expenses.SingleOrDefault(x => x.Id == id);
                return View(item);

            }

            return View();
        }

        public IActionResult DeleteExpense(int id)
        {
            var item = _expensesDbContext.Expenses.SingleOrDefault(x => x.Id == id);
            _expensesDbContext.Expenses.Remove(item);
            _expensesDbContext.SaveChanges();
            return RedirectToAction("Expenses");
        }
        public IActionResult CreateEditExpenseForm(Expenses model)
        {
            if (model.Id == 0)
            {
                _expensesDbContext.Expenses.Add(model);

            }
            else
            {
                _expensesDbContext.Expenses.Update(model);

            }
            _expensesDbContext.SaveChanges();
            return RedirectToAction("Expenses");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
