using System.ComponentModel.DataAnnotations;

namespace SupplyChain.Enums;

public enum MovementType
{
    [Display(Name = "Entrada")]
    Inbound,
    
    [Display(Name = "Saída")]
    Outbound
}