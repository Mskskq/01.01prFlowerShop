using System;
using System.Collections.Generic;

namespace Flower.Models;

public partial class Streat
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IdCity { get; set; }

    public virtual ICollection<Adress> Adresses { get; set; } = new List<Adress>();

    public virtual City? IdCityNavigation { get; set; }
}
