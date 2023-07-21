using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Models;

[Table("Manufacturers")]
public class Manufacturer
{
    public Manufacturer(int id, string name)
    {
        Id = id;
        Name = name;
    }

    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
}