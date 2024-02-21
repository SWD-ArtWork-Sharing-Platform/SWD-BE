using System;
using System.Collections.Generic;

namespace Market.Models;

public partial class DCategory
{
    public string CategoryId { get; set; } = null!;

    public string? CategoryName { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public string? Type { get; set; }

    public int? Quantity { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<FArtwork> FArtworks { get; set; } = new List<FArtwork>();
}
