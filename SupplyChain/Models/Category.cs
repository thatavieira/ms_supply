using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Models;

[Table("Categories")]
public class Category
{
    public Category(string name)
    {
        Name = name;
    }
    
    public Category()
    {
        
    }
    

    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
}