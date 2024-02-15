using System;
using System.Collections.Generic;

namespace Zombi;

public partial class ZombieType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Zombie> Zombies { get; set; } = new List<Zombie>();
}
