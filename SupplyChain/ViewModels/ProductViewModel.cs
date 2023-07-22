namespace SupplyChain.ViewModels;
using System.ComponentModel.DataAnnotations;

public class ProductViewModel
{
    
    public ProductViewModel()
    {
        
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
    
    
    [Required]
    public int ManufacturerId { get; set; }
    
    
}