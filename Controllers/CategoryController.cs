using Microsoft.AspNetCore.Mvc;
using HKnetMvcApp.Models;
using HKnetMvcApp.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

    

    //Getting category records from db
    public IActionResult Index()
    {
        try
        {
            List<Category> CategoryList = _db.Categories.ToList();
            CategoryList.Sort((l1,l2) => {
                if (l1.DisplayOrder > l2.DisplayOrder) return 1;
                else if(l1.DisplayOrder < l2.DisplayOrder) return -1;
                else return 0;
            });
            IEnumerable<Category> EnumerableCategory = CategoryList;
            return View(EnumerableCategory);
            
        }
        catch (System.Exception)
        {
            Console.WriteLine("Something went wrong");
            throw;
        }
    }

    //GET -
    public IActionResult Create()
    {
        return View();
    }

    //POST  -
    [HttpPost]
    [ValidateAntiForgeryToken]//prevent cross site request forgery
    public IActionResult Create(Category obj)
    {
        try
        {
            if(ModelState.IsValid)
            {
                //Custom validations in all summary & Field -
                if(obj.Name == obj.DisplayOrder.ToString()){
                    ModelState.AddModelError("Input Error", "The Name cannot be same as Dispaly Order.");
                    //Since the error model is custome (Input error), the msg will be displaed in ther summary
                    //If at all it is required to be displayed in the field hightled us
                    // ModelState.AddModelError("name","The Name cannot be same as Dispaly Order.");
                    return View(obj);

                }

                //If valid, proceed with saving the record:-
                System.Console.WriteLine("ModelState "+ModelState.IsValid);
                _db.Categories.Add(obj);
                _db.SaveChanges();//Save changes in db
                return RedirectToAction("Index");
                // Redirects to the Index view where the categories are populated into table
                //and the views as well are updated 
            }
            else return View(obj);
            
        }
        catch (System.Exception ecp)
        {
            System.Console.WriteLine("Got Exception : "+ecp);
            return View(obj);
            
        }
        
    }
}