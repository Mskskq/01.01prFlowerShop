using System;
using System.Collections.Generic;

namespace Flower.Models;

public partial class Region
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
