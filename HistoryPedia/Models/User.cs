using Microsoft.AspNetCore.Identity;

namespace CustomIdentityApp.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}