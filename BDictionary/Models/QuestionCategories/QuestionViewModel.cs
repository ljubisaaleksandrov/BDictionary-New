using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDictionary.UI.Models.Categories
{
    public interface IQuestionViewModelContainer
    {
        List<QuestionViewModel> QuestionViewModel { get; set; }
        QuestionHelperViewModel QuestionHelperViewModel { get; set; }
    }

    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Answer { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Creator { get; set; }
        public bool IsShiz { get; set; }
    }

    public class QuestionHelperViewModel
    {
        public string QuestionToAdd { get; set; }
        public string QuestionToRemove { get; set; }
        public string SelectedAnswer { get; set; }
        public bool IsShiz { get; set; }
        public List<string> Answers { get; set; }
    }
}