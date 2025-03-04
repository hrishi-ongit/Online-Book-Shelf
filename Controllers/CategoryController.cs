using Microsoft.AspNetCore.Mvc;
using HKnetMvcApp.Models;
using HKnetMvcApp.Data;

namespace HKnetMvcApp;

public class CategoryController : Controller 
{
    //We need to call the ApplicationDbContext as it works with the database
    private readonly ApplicationDbContext _db;

    //We need the initializer in the class that can retriev the database with the help of DI

    //Whatever is registered in the DI container be accessed with ApplicationDbContext
    //it has all the connection strings and table access that can be used in class ctor local initializations 
    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var objCategoryList = _db.Categories.ToList();
        System.Console.WriteLine(objCategoryList);
        return View();
    }
}