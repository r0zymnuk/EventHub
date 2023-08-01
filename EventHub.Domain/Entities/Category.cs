using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Domain.Entities;
public class Category
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ImageUrl { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Category? ParentCategory { get; set; }

    public IEnumerable<Category> SubCategories = new Collection<Category>();
}
