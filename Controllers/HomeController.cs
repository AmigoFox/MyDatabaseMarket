using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyDatabaseMarket.Models;

namespace MyDatabaseMarket.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Магазин Баз Данных - Приветствие";
            return View();
        }
    }
    
}
