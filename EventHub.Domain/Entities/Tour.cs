using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Domain.Entities;
public class Tour : EventBase
{
    public new bool IsTour { get; init; } = true;
    public ICollection<Event> Events { get; set; } = new List<Event>();
}
