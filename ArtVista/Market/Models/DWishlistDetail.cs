using System;
using System.Collections.Generic;

namespace Market.Models;

public partial class DWishlistDetail
{
    public string WishlistDetailId { get; set; } = null!;

    public string? ArtworkId { get; set; }

    public string? WishlistId { get; set; }
}
