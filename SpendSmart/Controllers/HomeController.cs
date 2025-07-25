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
            return View(allExpenses);
        }
        public IActionResult CreateEditExpense(int id)
        {
            var item = _expensesDbContext.Expenses.FirstOrDefault(x  => x.Id == id);
            return View();
        }

        public IActionResult DeleteExpense(int id)
        {
           return RedirectToAction("Expenses");
        }
        public IActionResult CreateEditExpenseForm(Expenses model)
        {

            _expensesDbContext.Expenses.Add(model);
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
