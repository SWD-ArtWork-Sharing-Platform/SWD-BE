using System;
using System.Collections.Generic;

namespace Market.Models;

public partial class DBankAccount
{
    public int AccountId { get; set; }

    public string UserId { get; set; } = null!;

    public string AccountNumber { get; set; } = null!;

    public string AccountType { get; set; } = null!;

    public decimal Balance { get; set; }

    public bool Confirmed { get; set; }

    public string? ConfirmCode { get; set; }
}
