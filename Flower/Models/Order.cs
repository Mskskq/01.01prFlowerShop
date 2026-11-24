using System;
using System.Collections.Generic;

namespace Flower.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public DateTime? DeliveryTime { get; set; }

    public int? IdAdress { get; set; }

    public decimal? Cost { get; set; }

    public virtual ICollection<FlowersInOrder> FlowersInOrders { get; set; } = new List<FlowersInOrder>();

    public virtual Adress? IdAdressNavigation { get; set; }

    public virtual User? User { get; set; }
}
