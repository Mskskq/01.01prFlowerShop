using System;
using System.Collections.Generic;

namespace Flower.Models;

public partial class FlowersInOrder
{
    public int Id { get; set; }

    public int? OrdeId { get; set; }

    public int? FlowersId { get; set; }

    public int? Count { get; set; }

    public virtual Flower? Flowers { get; set; }

    public virtual Order? Orde { get; set; }
}
