using System;
using System.Collections.Generic;

namespace Market.Models;

public partial class FArtwork
{
    public string ArtworkId { get; set; } = null!;

    public string? ArtworkName { get; set; }

    public decimal? Price { get; set; }

    public decimal? Discount { get; set; }

    public string? Status { get; set; }

    public string Id { get; set; } = null!;

    public string? CategoryId { get; set; }

    public string? ImageUrl { get; set; }
    public string? ImageLocalPath { get; set; }

    public virtual DCategory? Category { get; set; }

    public virtual ICollection<DOrderDetail> DOrderDetails { get; set; } = new List<DOrderDetail>();
}
