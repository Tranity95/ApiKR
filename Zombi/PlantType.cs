using System;
using System.Collections.Generic;

namespace Zombi;

public partial class PlantType
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Plant> Plants { get; set; } = new List<Plant>();
}
