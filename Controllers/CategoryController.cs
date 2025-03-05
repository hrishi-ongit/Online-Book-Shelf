using Microsoft.AspNetCore.Mvc;
using HKnetMvcApp.Models;
using HKnetMvcApp.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authentication.Cookies;

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

    

    /**************     Create GET and Create POST  Start    ******************/
    //Getting category records from db
    public IActionResult Index()
    {
        try
        {
            List<Category> CategoryList = _db.Categories.ToList();
            // CategoryList.Sort((l1,l2) => {
            //     if (l1.DisplayOrder > l2.DisplayOrder) return 1;
            //     else if(l1.DisplayOrder < l2.DisplayOrder) return -1;
            //     else return 0;
            // });
            CategoryList.Sort((l1,l2) => l1.DisplayOrder.CompareTo(l2.DisplayOrder));
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
        //Custom validations in all summary & Field -
        if(obj.Name == obj.DisplayOrder.ToString()){
            // ModelState.AddModelError("Input Error", "The Name cannot be same as Dispaly Order.");
            //Partial validation script wont avoid the page load here


            //Since the error model is custome (Input error), the msg will be displaed in ther summary
            //If at all it is required to be displayed in the field hightled us
            ModelState.AddModelError("name","The Name cannot be same as Dispaly Order.");
        }
        if(ModelState.IsValid)
        {
            //If valid, proceed with saving the record:-
            System.Console.WriteLine("ModelState "+ModelState.IsValid);
            _db.Categories.Add(obj);
            _db.SaveChanges();//Save changes in db
            return RedirectToAction("Index");
            // Redirects to the Index view where the categories are populated into table
            //and the views as well are updated             
        }
        return View();
        
    }

/**************     Create GET and Create POST  End    ******************/



/**************     Edit GET and Edit POST  Start    ******************/
    
    //GET -
    public IActionResult Edit(int? id)
        {
            if(id == null || id == 0){
                return NotFound();
            }
            var dbCategoryObj = _db.Categories.Find(id);
            //Find - finds element based on primary key
            //SingleOrDefault - returns only one element, returns empty if no element & throws excp if there are more than 1 elememt
            //Single() throws exception when there is not match found to return
            //FirstOrDefault - returns 1st found element from multiple
            // var dbCategoryObj2 = _db.Categories.FirstOrDefault(obj => obj.Id == id);
            // var dbCategoryObj3 = _db.Categories.SingleOrDefault(obj => obj.Id == id);
            
            if(dbCategoryObj == null) return NotFound();
            return View(dbCategoryObj);
        }

    // POST  -
    [HttpPost]
    [ValidateAntiForgeryToken]//prevent cross site request forgery
    public IActionResult Edit(Category obj)
    {
        if(obj.Name == obj.DisplayOrder.ToString()){
            ModelState.AddModelError("name","The Name cannot be same as Dispaly Order.");
        }
        if(ModelState.IsValid)
        {
            //If valid, proceed with saving the record:-
            _db.Categories.Update(obj); //EF core meth based in Pkey does the update operation
            _db.SaveChanges();//Save changes in db
            return RedirectToAction("Index");
            // Redirects to the Index view where the categories are populated into table
            //and the views as well are updated             
        }
        return View();
        
    }

/**************     Edit GET and Edit POST  Start    ******************/


/**************     Delete GET and Delete POST  Start    **************/

//GET -
//I'll skip this one
    // public IActionResult Delete(int? id)
    //     {
    //         if(id == null || id == 0){
    //             return NotFound();
    //         }
    //         var dbCategoryObj = _db.Categories.Find(id);            
    //         if(dbCategoryObj == null) return NotFound();
    //         return View(dbCategoryObj);
    //     }

    //POST -

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        if(ModelState.IsValid)
        {
            var obj = _db.Categories.Find(id);
            if(obj == null) return NotFound();
            _db.Categories.Remove(obj); //EF core meth based in Pkey does the update operation
            _db.SaveChanges();
        }
        return RedirectToAction("Index");

    }


/**************     Delete GET and Delete POST  End    *****************/

}