using Microsoft.AspNetCore.Identity;

namespace task_1135.Domain.Models
{
    public class ApplicationIdentityUser : IdentityUser
    {
        public DateTime DateOfBirth { get; set; }
    }
}
