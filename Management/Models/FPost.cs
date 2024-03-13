using System;
using System.Collections.Generic;

namespace Management.Models;

public partial class FPost
{
    public string PostId { get; set; } = null!;

    public string? ArtworkId { get; set; }

    public string? Tittle { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? Status { get; set; }

    public virtual FArtwork? Artwork { get; set; }

}
