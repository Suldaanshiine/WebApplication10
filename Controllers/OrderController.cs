using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication10.Models;
using WebApplication10.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication10.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        private List<SelectListItem> GetCustomerSelectList()
        {
            return _context.Customers
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
        }

        private List<SelectListItem> GetEmployeeSelectList()
        {
            return _context.Employees
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                }).ToList();
        }

        public IActionResult Index()
        {
            var orders = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .ToList();
            return View(orders);
        }

        public IActionResult Add()
        {
            ViewBag.Customers = GetCustomerSelectList();
            ViewBag.Employees = GetEmployeeSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    CustomerId = model.CustomerId,
                    EmployeeId = model.EmployeeId,
                    DeliveryDate = model.DeliveryDate,
                    Quantity = model.Quantity
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customers = GetCustomerSelectList();
            ViewBag.Employees = GetEmployeeSelectList();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            var model = new UpdateOrderViewModel
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                DeliveryDate = order.DeliveryDate,
                Quantity = order.Quantity
            };
            ViewBag.Customers = GetCustomerSelectList();
            ViewBag.Employees = GetEmployeeSelectList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = _context.Orders.Find(model.Id);
                if (order != null)
                {
                    order.CustomerId = model.CustomerId;
                    order.EmployeeId = model.EmployeeId;
                    order.DeliveryDate = model.DeliveryDate;
                    order.Quantity = model.Quantity;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Customers = GetCustomerSelectList();
            ViewBag.Employees = GetEmployeeSelectList();
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
