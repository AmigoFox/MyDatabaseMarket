using Microsoft.AspNetCore.Mvc;

namespace MyDatabaseMarket.Controllers
{
    public class AccountController : Controller
    {
        // /Account
        public IActionResult Index() => View();

        // /Account/Profile
        public IActionResult Profile() => View();

        // /Account/Orders
        public IActionResult Orders() => View();

        // /Account/Payments
        public IActionResult Payments() => View();
    }
}
