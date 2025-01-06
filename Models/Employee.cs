namespace WebApplication10.Models
{
    public class AddEmployeeViewModel
    {
        public string ?Name { get; set; }
        public string? Role { get; set; }
        public string ?Phone { get; set; }
    }

    public class UpdateEmployeeViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string ?Role { get; set; }
        public string? Phone { get; set; }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string ?Name { get; set; }
        public string ?Role { get; set; }
        public string? Phone { get; set; }

        // Navigation property
        public ICollection<Order> ?Orders { get; set; }
    }
}
