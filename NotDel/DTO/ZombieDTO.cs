using System;
using System.Collections.Generic;

namespace Zombi;

public partial class ZombieDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Health { get; set; }

    public int ZombieTypeId { get; set; }
    public string ZombieType { get; set; }

}
