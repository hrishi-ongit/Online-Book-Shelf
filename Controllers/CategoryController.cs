using Microsoft.AspNetCore.Mvc;
using HKnetMvcApp.Models;

namespace HKnetMvcApp;

public class CategoryController : Controller 
{
    public IActionResult Index()
    {
        return View();
    }
}