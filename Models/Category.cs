using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace HKnetMvcApp.Models;
public class Category {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }    
    public int DisplayOrder { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;//default date

     
}