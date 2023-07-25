using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SupplyChain.ViewModels;
[Keyless]
public class InventoryManagementChart
{
    public InventoryManagementChart()
    {
    }

    public int Day { get; set; }
    
    public int Receiving { get; set; }
    
    public int Shipping { get; set; }
}