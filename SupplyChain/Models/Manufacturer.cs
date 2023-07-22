using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Models;

[Table("Manufacturers")]
public class Manufacturer
{
    public Manufacturer(string name)
    {
        Name = name;
    }

    public Manufacturer()
    {
        
    }

    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
}