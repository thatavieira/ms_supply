using System.ComponentModel.DataAnnotations;
using SupplyChain.Enums;

namespace SupplyChain.ViewModels;

public class ShippingViewModel
{
    public ShippingViewModel()
    {
        Type = MovementType.Outbound;
    }

    [Key]
    public int Id { get; set; }
    
    [Required]
    public int ProductId { get; set; }
    
    [Required]
    public string Local { get; set; }
    
    [Required]
    public MovementType Type { get; set; }
    
    [Required]
    public int Quantity { get; set; }
}