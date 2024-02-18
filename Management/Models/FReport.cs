using System;
using System.Collections.Generic;

namespace Management.Models;

public partial class FReport
{
    public string ReportId { get; set; } = null!;

    public DateTime? CreatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? ArtworkId { get; set; }

    public string? Status { get; set; }

    public string? Detail { get; set; }

    public string? Id { get; set; }

    public string? ActionNote { get; set; }

    public virtual FArtwork? Artwork { get; set; }
}
