using System;
using System.Collections.Generic;

namespace Flower.Models;

public partial class Adress
{
    public int Id { get; set; }

    public int? IdStreat { get; set; }

    public string? NumberHouse { get; set; }

    public int? NumberOpartamens { get; set; }

    public virtual Streat? IdStreatNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
