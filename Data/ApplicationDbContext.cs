using System.Security.Cryptography.X509Certificates;
using HKnetMvcApp.Models;
using Microsoft.EntityFrameworkCore;
namespace HKnetMvcApp.Data;
public class ApplicationDbContext : DbContext
{
    //here  - pass the db param options received in ctor to base class
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    //here - we created the Category table in database
    //for that we crete a db set
    public DbSet<Category> Categories {get; set;}//table name = Categories
    // docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YourStrong!Passw0rd' \
    //    -p 1433:1433 --name sqlserver \
    //    -d mcr.microsoft.com/mssql/server:2022-latest

        

}