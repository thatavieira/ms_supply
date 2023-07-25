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
    
    [Required(ErrorMessage = "Por favor, digite um nome para categoria.")]
    [StringLength(30, MinimumLength = 10, ErrorMessage = "O nome deve ser maior que 10 caracteres.")]
    public string Name { get; set; }
    
}