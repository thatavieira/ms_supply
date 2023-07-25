using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Models;

[Table("Products")]
public class Product
{
    public Product(string name, string description, string productNumber, int categoryId, int manufacturerId)
    {
        Name = name;
        Description = description;
        ProductNumber = productNumber;
        CategoryId = categoryId;
        ManufacturerId = manufacturerId;
    }

    public Product()
    {
        
    }

    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Por favor, digite um nome para o produto.")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "O nome deve ser maior que 3 caracteres.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Por favor, digite uma descrição.")]
    [StringLength(60, MinimumLength = 10, ErrorMessage = "O nome deve ser maior que 10 caracteres.")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Por favor, digite um código para o produto")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "O nome deve ser maior que 3 caracteres.")]
    public string ProductNumber { get; set; }
    
    [Required(ErrorMessage = "Por favor, digite uma CategoryID.")]
    [StringLength(60, MinimumLength = 10, ErrorMessage = "O nome deve ser maior que 10 caracteres.")]
    public int CategoryId  { get; set; }
    
    [ForeignKey("CategoryId")]
    public  Category Category { get; set; }
    
    [Required(ErrorMessage = "Por favor, digite uma ManufacturerId.")]
    [StringLength(60, MinimumLength = 10, ErrorMessage = "O nome deve ser maior que 10 caracteres.")]
    public int ManufacturerId { get; set; }
    
    [ForeignKey("ManufacturerId")]
    public  Manufacturer Manufacturer { get; set; }
    
}