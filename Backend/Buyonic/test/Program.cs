using Buyonic.DAL;
using Microsoft.AspNetCore.Identity;
namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Paste this in a temporary minimal API or console app and run it once:
            var hasher = new PasswordHasher<ApplicationUser>();
            var dummy = new ApplicationUser();
            Console.WriteLine(hasher.HashPassword(dummy, "Password@123"));
        }
    }
}
