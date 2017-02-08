using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDictionary.UI.Models.Categories
{
    public interface IQuestionCategoryViewModelContainer
    {
        QuestionCategoryViewModel QuestionCategoryViewModel { get; set; }
    }
    public class QuestionCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; }
        public int QuestionAnswerType { get; set; }
    }
}