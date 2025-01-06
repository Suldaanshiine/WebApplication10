using Microsoft.AspNetCore.Mvc;
using WebApplication10.Data;
using WebApplication10.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication10.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var customers = _context.Customers.ToList();
            return View(customers);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddCustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    Name = model.Name,
                    Address = model.Address,
                    Phone = model.Phone,
                    Email = model.Email
                };
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Update(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();

            var model = new UpdateCustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                Phone = customer.Phone,
                Email = customer.Email
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(UpdateCustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _context.Customers.Find(model.Id);
                if (customer == null) return NotFound();

                customer.Name = model.Name;
                customer.Address = model.Address;
                customer.Phone = model.Phone;
                customer.Email = model.Email;

                _context.Customers.Update(customer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
