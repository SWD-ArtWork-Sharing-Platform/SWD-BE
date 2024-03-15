using System;
using System.Collections.Generic;

namespace Management.Models;

public partial class DPackageOfCreator
{
    public string PackageId { get; set; } = null!;

    public string Id { get; set; } = null!;

    public DateTime? ExpiredDate { get; set; }

    public DateTime? GraceDate { get; set; }

    public decimal? Price { get; set; }

    public string? Status { get; set; }
    public int Remain { get; set; }

    public virtual FPackage Package { get; set; } = null!;
}
