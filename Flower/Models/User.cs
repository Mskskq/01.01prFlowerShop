using System;
using System.Collections.Generic;

namespace Flower.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Lastname { get; set; }

    public string? Surname { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Telephone { get; set; }

    public int? Address { get; set; }

    public virtual Adress? AddressNavigation { get; set; }

    public virtual ICollection<Corsina> Corsinas { get; set; } = new List<Corsina>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
