using System;
using System.Collections.Generic;

namespace Management.Models;

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

    public virtual ICollection<FPost> FPosts { get; set; } = new List<FPost>();

    public virtual ICollection<FReport> FReports { get; set; } = new List<FReport>();
}
