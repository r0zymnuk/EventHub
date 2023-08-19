namespace EventHub.Domain.ValueObjects;
public class Promo
{
    public string Code { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
}
