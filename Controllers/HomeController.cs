using System.Diagnostics;
using SpendSmart.Models;
using Microsoft.AspNetCore.Mvc;

namespace SpendSmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly SpendSmartDBContext _context;

        public HomeController(ILogger<HomeController> logger, SpendSmartDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            var allExpenses = _context.Expenses.ToList();
            return View(allExpenses);
        }

        public IActionResult CreateEditExpenseForm(Expenses model)
        {

            if(model.Id == 0)
            {
                // Create
                _context.Expenses.Add(model);
            }

            else
            {
                // Editing
                _context.Expenses.Update(model);
            }
            

            _context.SaveChanges();
            
            return RedirectToAction("Expenses");
        }

        public IActionResult CreateEditExpenses(int? id)
        {
            if (id != null)
            {
                // editing -> load an expense by id
                var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);

                if (expenseInDb == null)
                {
                    // If the expense is not found, return a NotFound result
                    return NotFound();
                }

                return View(expenseInDb);
            }

            // If id is null, return a new expense model for creation
            return View(new Expenses());
        }

        public IActionResult DeleteExpense(int id)
        {
            var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);

            // Check if expense is found before trying to remove it
            if (expenseInDb == null)
            {
                return NotFound(); // 404 response if the expense is not found
            }

            _context.Expenses.Remove(expenseInDb);

            _context.SaveChanges();

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

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddDbContext<SpendSmartDBContext>(options =>
        //        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))); // Ensure Configuration is accessible
        //}
    }
}
