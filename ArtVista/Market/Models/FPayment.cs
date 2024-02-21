using System;
using System.Collections.Generic;

namespace Market.Models;

public partial class FPayment
{
    public string? PaymentId { get; set; }

    public string? PaymentName { get; set; }

    public decimal? Total { get; set; }

    public string? PaymentStatus { get; set; }

    public string? Note { get; set; }

    public string? OrderId { get; set; }

    public virtual FOrder? Order { get; set; }
}
