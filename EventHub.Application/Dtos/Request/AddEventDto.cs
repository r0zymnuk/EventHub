using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Application.Dtos.Request;
public class AddEventDto
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public string ImageUrl { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ICollection<Guid> Categories { get; set; } = new List<Guid>();
    public Location Location { get; set; } = new Location("Ukraine", "Kyiv");
    public Guid Organizer { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
