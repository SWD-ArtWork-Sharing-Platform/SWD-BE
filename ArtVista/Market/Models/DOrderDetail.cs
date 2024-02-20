using System;
using System.Collections.Generic;

namespace Market.Models;

public partial class DOrderDetail
{
    public string OrderDetailId { get; set; } = null!;

    public string? OrderId { get; set; }

    public string? ArtworkId { get; set; }

    public decimal? Price { get; set; }

    public bool? Dowloaded { get; set; }

    public virtual FArtwork? Artwork { get; set; }

    public virtual FOrder? Order { get; set; }
}
