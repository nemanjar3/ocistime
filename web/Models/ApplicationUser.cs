using Microsoft.AspNetCore.Identity;

namespace web.Models;

public class ApplicationUser : IdentityUser
{
     public string? FirstName { get; set; }
     public string? LastName { get; set; }

     public override string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();


     
}