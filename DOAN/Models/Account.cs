using System;
using System.Collections.Generic;

namespace DOAN.Models
{
    public partial class Account
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? Role { get; set; }
    }
}
