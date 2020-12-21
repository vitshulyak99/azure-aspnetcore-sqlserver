using System;
using Microsoft.AspNetCore.Identity;

namespace FApp.DAL
{
    public class User : IdentityUser
    {

        public DateTime CreatedDateTime { get; set; }
        public DateTime LastLoginDateTime { get; set; }
        public bool IsBlocked { get; set; } = false;

    }
}