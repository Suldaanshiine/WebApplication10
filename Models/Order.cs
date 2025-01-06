namespace WebApplication10.Models
{
    public class AddOrderViewModel
    {
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateOrderViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Quantity { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer ?Customer { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Quantity { get; set; }
        public ICollection<OrderInventory> ?OrderInventories { get; set; }
    }
}
