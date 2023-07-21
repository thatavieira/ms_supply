using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Models;

[Table("Products")]
public class Product
{
    public Product(int id, string name, string description, string productNumber, int categoryId, int manufacturerId)
    {
        Id = id;
        Name = name;
        Description = description;
        ProductNumber = productNumber;
        CategoryId = categoryId;
        ManufacturerId = manufacturerId;
    }

    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public string ProductNumber { get; set; }
    
    [Required]
    public int CategoryId  { get; set; }
    
    [ForeignKey("CategoryId")]
    public  Category Category { get; set; }
    
    [Required]
    public int ManufacturerId { get; set; }
    
    [ForeignKey("ManufacturerId")]
    public  Manufacturer Manufacturer { get; set; }
    
}