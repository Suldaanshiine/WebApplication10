using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace WebApplication10.Models
{
    // AddPaymentViewModel for creating a new payment
    public class AddPaymentViewModel
    {
        public int OrderId { get; set; } // The order related to the payment
        public decimal AmountPaid { get; set; } // Amount paid by the customer
        public string? PaymentMethod { get; set; } // Payment method (e.g., Credit Card, Cash)
        public DateTime PaymentDate { get; set; } // Date of payment

        // This will be used for populating the select dropdown
        public IEnumerable<SelectListItem>? Orders { get; set; }
    }

    // UpdatePaymentViewModel inherits from AddPaymentViewModel
    public class UpdatePaymentViewModel : AddPaymentViewModel
    {
        public int Id { get; set; } // Unique ID for updating
    }

    // Payment Model used in the database
    public class Payment
    {
        public int Id { get; set; } // Unique payment ID
        public int OrderId { get; set; } // The order associated with the payment
        public Order? Order { get; set; }  // Reference to the Order class
        public DateTime PaymentDate { get; set; } // Payment date
        public decimal AmountPaid { get; set; } // Amount paid by the customer
        public string? PaymentMethod { get; set; } // Payment method (Credit Card, Cash, etc.)
    }
}
