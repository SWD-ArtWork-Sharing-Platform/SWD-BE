using System;
using System.Collections.Generic;

namespace Management.Models;

public partial class FConfiguration
{
    public string ConfigurationId { get; set; } = null!;

    public decimal? CommisionFee { get; set; }

    public DateTime? AppliedDate { get; set; }

    public string? Status { get; set; }

    public string? Id { get; set; }
}
