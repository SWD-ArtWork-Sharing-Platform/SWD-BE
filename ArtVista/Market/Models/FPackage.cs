﻿using System;
using System.Collections.Generic;

namespace Market.Models;

public partial class FPackage
{
    public string PackageId { get; set; } = null!;

    public string? PackageName { get; set; }

    public int? MaximumArtworks { get; set; }

    public decimal? Price { get; set; }

    public decimal? Discount { get; set; }

    public string? PackageTime { get; set; }    

    public virtual ICollection<DPackageOfCreator> DPackageOfCreators { get; set; } = new List<DPackageOfCreator>();
}
