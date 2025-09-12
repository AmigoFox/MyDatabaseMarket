using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyDatabaseMarket;
using MyDatabaseMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static MyDatabaseMarket.Database;

namespace MyDatabaseMarket.Controllers
{
    public class AccountController : Controller
    {
        private readonly Database.AppDbContext _context;

        public AccountController(Database.AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: /Account
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Pay(int id)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.Id == id);

            if (payment != null && payment.Status != "Оплачен")
            {
                payment.Status = "Оплачен";
                payment.NextPaymentDate = DateTime.Now.AddMonths(1)
                    .ToString("yyyy-MM-dd HH:mm:ss");

                var order = _context.Orders.FirstOrDefault(o => o.Id == payment.OrderId);
                if (order != null)
                {
                    order.Status = "Работает";
                }

                _context.SaveChanges();
            }

            return RedirectToAction("Payments");
        }

        [HttpPost]
        public IActionResult OrderDelete(int id)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.Id == id);

            if (payment != null)
            {
                // сначала удаляем сам заказ
                var order = _context.Orders.FirstOrDefault(o => o.Id == payment.OrderId);
                if (order != null)
                {
                    _context.Orders.Remove(order);
                }

                // потом удаляем платеж
                _context.Payments.Remove(payment);

                _context.SaveChanges();
            }

            return RedirectToAction("Payments");
        }

        
        [HttpPost]
        public IActionResult changeorder(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound();

            return RedirectToAction("Calculator", new { id = order.Id });
        }


        [HttpGet]
        public IActionResult Profile()
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (userId == null) return RedirectToAction("Index");

            var user = _context.Users.FirstOrDefault(u => u.Id == int.Parse(userId));
            if (user == null) return RedirectToAction("Index");

            return View(user);
        }

        [HttpGet]
        public IActionResult Orders()
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (userId == null) return RedirectToAction("Index");

            var orders = _context.Orders.Where(o => o.UserId == int.Parse(userId)).ToList();
            return View(orders);
        }

        [HttpGet]
        public IActionResult Payments()
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (userId == null) return RedirectToAction("Index");

            var pays = _context.Payments.Where(p => p.UserId == int.Parse(userId)).ToList();
            return View(pays);
        }



        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Логин и пароль обязательны";
                return View("Index");
            }

            string hashedPassword = ComputeSha256Hash(password);
            var user = _context.Users.FirstOrDefault(u => u.Login == login && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim("UserId", user.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                TempData["Message"] = $"Добро пожаловать, {user.Login}!";
                return RedirectToAction("Index", "Account");
            }

            ViewBag.Error = "Неверный логин или пароль";
            return View("Index");
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Message"] = "Вы успешно вышли из системы";
            return RedirectToAction("Index", "Account");
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Login == model.Login))
                {
                    ModelState.AddModelError("Login", "Такой логин уже существует");
                    return View(model);
                }

                string hashedPassword = ComputeSha256Hash(model.Password);

                var user = new Database.User
                {
                    Login = model.Login,
                    Email = model.Email,
                    PasswordHash = hashedPassword,
                    RegistrationDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim("UserId", user.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Account");
            }

            return View(model);
        }

        // POST: /Account/SaveCalculator
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCalculatorData([FromForm] CalculatorDataViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var userId = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);

            var data = new Database.Order
            {
                UserId = userId,
                DatabaseType = model.DatabaseType,
                SizeGB = model.SizeGB,
                StorageType = model.StorageType,
                IOPS = model.IOPS,
                Scalability = model.Scalability,
                Country = string.Join(", ", model.SelectedCountries),
                PriceRUB = model.PriceRUB,
                PriceUSD = model.PriceUSD,
                OrderDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                Status = "Active"
            };

            _context.Orders.Add(data);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Данные калькулятора успешно сохранены!";
            return RedirectToAction("Orders");
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                var builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }

    // ViewModel для передачи данных из Blazor-калькулятора
    public class CalculatorDataViewModel
    {
        public string DatabaseType { get; set; } = null!;
        public int SizeGB { get; set; }
        public string StorageType { get; set; } = null!;
        public string IOPS { get; set; } = "None";
        public string Scalability { get; set; } = null!;
        public List<string> SelectedCountries { get; set; } = new();
        public decimal PriceRUB { get; set; }
        public decimal PriceUSD { get; set; }
    }
}
