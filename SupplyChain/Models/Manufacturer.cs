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
    
    [Required(ErrorMessage = "Por favor, digite um nome para o Fabricante.")]
    [StringLength(30, MinimumLength = 5, ErrorMessage = "O nome deve ser maior que 5 caracteres.")]
    public string Name { get; set; }
    
}