namespace WebApplication10.Models
{
    public class AddCustomerViewModel
    {
        public string ?Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }

    public class UpdateCustomerViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string ?Phone { get; set; }
        public string? Email { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string ?Name { get; set; }
        public string ?Address { get; set; }
        public string ?Phone { get; set; }
        public string ?Email { get; set; }

        // Navigation property
        public ICollection<Order>? Orders { get; set; }
    }
}
