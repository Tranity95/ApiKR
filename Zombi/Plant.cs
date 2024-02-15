using System;
using System.Collections.Generic;

namespace Zombi;

public partial class Plant
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Damage { get; set; }

    public int PlantTypeId { get; set; }

    public virtual PlantType PlantType { get; set; } = null!;
}
