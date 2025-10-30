using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class ApplicationIdentityUser : IdentityUser
    {
        public DateTime DateOfBirth { get; set; }
    }
}
