using System;
using System.Collections.Generic;

namespace Market.Models;

public partial class FWishlist
{
    public string WishlistId { get; set; } = null!;

    public string Id { get; set; } = null!;

    public decimal? Total { get; set; }

    public string? Note { get; set; }
}
