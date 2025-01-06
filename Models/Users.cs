using Microsoft.AspNetCore.Identity;

namespace WebApplication10.Models
{
    public class Users : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
