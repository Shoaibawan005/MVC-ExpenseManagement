using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SpendSmart.Models;

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
            return View();
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
