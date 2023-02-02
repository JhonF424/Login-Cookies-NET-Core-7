using System;
using System.Collections.Generic;

namespace LoginProject.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Pass { get; set; }
}
