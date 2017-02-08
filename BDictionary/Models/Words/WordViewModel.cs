using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDictionary.BDictionary.UI.Models.Word
{
    public class WordeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Type { get; set; }
    }
}