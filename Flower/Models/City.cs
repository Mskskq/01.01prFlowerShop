using System;
using System.Collections.Generic;

namespace Flower.Models;

public partial class City
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IdRegion { get; set; }

    public virtual Region? IdRegionNavigation { get; set; }

    public virtual ICollection<Streat> Streats { get; set; } = new List<Streat>();
}
