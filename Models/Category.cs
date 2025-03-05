using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace HKnetMvcApp.Models;
public class Category {
    [Key]
    public int Id { get; set; }
    [Required]

    [DisplayName("Book Name")] 
    public string? Name { get; set; }
    
    [DisplayName("Display Order")] 
    // [Range(1,100)] //Takes min & max range in inp, beyond the range, gives validation error
    // [Range(1,100,ErrorMessage = "Your custom message!")]
    //[DisplayName("Display Order")] -
    // This will be used by the tag helper to display the label over field input, if not provided, it pickes the propert name as it is   
    public int DisplayOrder { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;//default date

     
}