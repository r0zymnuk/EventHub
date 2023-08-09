using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.Domain.ValueObjects;
public class Promo
{
    public string Code { get; set; } = string.Empty;
    public int Quantity { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Discount { get; set; }
}
