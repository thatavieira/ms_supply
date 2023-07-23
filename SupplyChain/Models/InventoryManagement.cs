using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SupplyChain.Enums;

namespace SupplyChain.Models;

[Table("InventoryManagements")]
public class InventoryManagement
{

    public InventoryManagement(int productId, string local, MovementType type, int quantity)
    {
        ProductId = productId;
        CreateAt = DateTime.UtcNow;
        UpdateAt = DateTime.UtcNow;
        Quantity = quantity;
        Local = local;
        Type = type;

    }

    public InventoryManagement()
    {
        CreateAt = DateTime.UtcNow;
        UpdateAt = DateTime.UtcNow;
    }
    
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int ProductId { get; set; }
    
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    
    [Required]
    public DateTime CreateAt { get; set; }
    
    [Required]
    public DateTime UpdateAt { get; set; }
    
    [Required]
    public string Local { get; set; }
    
    [Required]
    public MovementType Type { get; set; }
    
    [Required]
    public int Quantity { get; set; }
    
    
}