using Microsoft.AspNetCore.Authorization;

namespace WebApplication10.Models
{
    [Authorize]
    public class AddInventoryViewModel
    {
        public string? ItemName { get; set; }
        public int QuantityAvailable { get; set; }
    }

    public class UpdateInventoryViewModel
    {
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public int QuantityAvailable { get; set; }
    }

    public class Inventory
    {
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public int QuantityAvailable { get; set; }

        // Navigation property for many-to-many relationship with Orders
        public ICollection<OrderInventory> ?OrderInventories { get; set; }
    }
}
