using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Domain.ValueObjects;
public class Ticket
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
}
