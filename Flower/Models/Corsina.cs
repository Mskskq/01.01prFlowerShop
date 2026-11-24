using System;
using System.Collections.Generic;

namespace Flower.Models;

public partial class Corsina
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? FlowersId { get; set; }

    public int? Count { get; set; }

    public virtual Flower? Flowers { get; set; }

    public virtual User? User { get; set; }
}
