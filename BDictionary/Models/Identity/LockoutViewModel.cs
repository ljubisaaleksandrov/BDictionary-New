using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDictionary.UI.Models.Identity
{
    public class LockoutViewModel
    {
        public LockoutStatus Status { get; set; }
        public DateTime? LockoutEndDate { get; set; }
    }
}