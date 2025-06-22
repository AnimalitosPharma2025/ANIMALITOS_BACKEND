using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class Rol
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int StatusId { get; set; }

    public virtual Status Status { get; set; } = null!;
}
