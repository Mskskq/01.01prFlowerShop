using System;
using System.Collections.Generic;

namespace Flower.Models;

public partial class Flower
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Img { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Corsina> Corsinas { get; set; } = new List<Corsina>();

    public virtual ICollection<FlowersInOrder> FlowersInOrders { get; set; } = new List<FlowersInOrder>();
}
