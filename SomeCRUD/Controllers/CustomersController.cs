using Microsoft.AspNetCore.Mvc;
using SomeCRUD.Data;
using SomeCRUD.Models;

namespace SomeCRUD.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CustomersController(ApplicationDbContext db) 
        { 
            _db = db; 
        }

       
        public IActionResult Index(string? searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var customers = _db.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                customers = customers.Where(c => c.CustomerName.ToLower().Contains(searchString.ToLower())
                                            || c.City.Contains(searchString.ToLower()));
            }

            return View(customers.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var customer = _db.Customers.Find(id);
            if (customer == null) return RedirectToAction("Index");
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            _db.Customers.Update(customer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var customer = _db.Customers.Find(id);
            if (customer != null)
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}