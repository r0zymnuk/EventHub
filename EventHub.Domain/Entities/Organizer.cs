using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Domain.Entities;
public class Organizer : User
{
    public ICollection<Event> Events { get; set; } = new List<Event>();
}
