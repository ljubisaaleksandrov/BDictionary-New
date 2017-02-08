using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDictionary.UI.Models.Categories
{
    public class QuestionCategoryTreeViewModel
    {
        public int? SelectedCategoryId { get; set; }
        public string SelectedCategoryName { get; set; }
        public bool CategoryError { get; set; }
    }
}