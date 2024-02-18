using System;
using System.Collections.Generic;

namespace Management.Models;

public partial class DInteraction
{
    public string InteractionId { get; set; } = null!;

    public string? Id { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? Like { get; set; }

    public string? Comments { get; set; }

    public string? PostId { get; set; }

    public virtual FPost? Post { get; set; }
}
