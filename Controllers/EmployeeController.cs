using Microsoft.AspNetCore.Mvc;
using WebApplication10.Data;
using WebApplication10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace WebApplication10.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employee
        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            return View(employees);
        }

        // GET: Employee/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: Employee/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Name = model.Name,
                    Role = model.Role,
                    Phone = model.Phone
                };
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Employee/Update/5
        public IActionResult Update(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            var model = new UpdateEmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Role = employee.Role,
                Phone = employee.Phone
            };
            return View(model);
        }

        // POST: Employee/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UpdateEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _context.Employees.Find(model.Id);
                if (employee == null)
                {
                    return NotFound();
                }

                employee.Name = model.Name;
                employee.Role = model.Role;
                employee.Phone = model.Phone;

                _context.Update(employee);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Employee/Delete/5
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}