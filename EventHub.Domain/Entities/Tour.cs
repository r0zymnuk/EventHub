using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Domain.Entities;
public class Tour : EventBase
{
    public ICollection<Event> Events { get; set; } = new List<Event>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
