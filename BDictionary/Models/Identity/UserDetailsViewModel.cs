using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDictionary.UI.Models.Identity
{
    public class UserDetailsViewModel
    {
        public ApplicationUser User { get; set; }
        public LockoutViewModel Lockout { get; set; }
    }
}