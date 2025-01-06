using Microsoft.AspNetCore.Mvc;
using WebApplication10.Data;
using WebApplication10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication10.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Payment
        public async Task<IActionResult> Index()
        {
            var payments = await _context.Payments.Include(p => p.Order).ToListAsync();
            return View(payments);
        }

        // GET: Payment/Create
        public async Task<IActionResult> Create()
        {
            var orders = await _context.Orders.ToListAsync();
            var model = new AddPaymentViewModel
            {
                Orders = orders.Select(o => new SelectListItem
                {
                    Value = o.Id.ToString(),
                    Text = $"Order #{o.Id}"
                })
            };
            return View(model);
        }

        // POST: Payment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddPaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var payment = new Payment
                {
                    OrderId = model.OrderId,
                    AmountPaid = model.AmountPaid,
                    PaymentMethod = model.PaymentMethod,
                    PaymentDate = model.PaymentDate
                };
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Payment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound();

            var model = new UpdatePaymentViewModel
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                AmountPaid = payment.AmountPaid,
                PaymentMethod = payment.PaymentMethod,
                PaymentDate = payment.PaymentDate,
                Orders = (await _context.Orders.ToListAsync()).Select(o => new SelectListItem
                {
                    Value = o.Id.ToString(),
                    Text = $"Order #{o.Id}"
                })
            };

            return View(model);
        }

        // POST: Payment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdatePaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var payment = await _context.Payments.FindAsync(model.Id);
                if (payment == null)
                    return NotFound();

                payment.OrderId = model.OrderId;
                payment.AmountPaid = model.AmountPaid;
                payment.PaymentMethod = model.PaymentMethod;
                payment.PaymentDate = model.PaymentDate;

                _context.Payments.Update(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Payment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var payment = await _context.Payments.Include(p => p.Order).FirstOrDefaultAsync(p => p.Id == id);
            if (payment == null)
                return NotFound();

            return View(payment);
        }

        // POST: Payment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
