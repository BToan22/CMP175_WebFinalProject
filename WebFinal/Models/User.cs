using System;
using System.Collections.Generic;

namespace WebFinal.Models;

public partial class User
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public string City { get; set; } = null!;
}
