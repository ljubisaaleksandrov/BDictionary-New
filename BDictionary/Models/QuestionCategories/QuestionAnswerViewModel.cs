﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDictionary.UI.Models.Categories
{
    public interface IQuestionAnswerViewModelContainer
    {
        List<QuestionAnswerViewModel> QuestionAnswerViewModel { get; set; }
        QuestionAnswerHelperViewModel QuestionAnswerHelperViewModel { get; set; }
    }
    public class QuestionAnswerViewModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int CategoryID { get; set; }
        public bool IsPrimary { get; set; }
    }

    public class QuestionAnswerHelperViewModel
    {
        public string AnswerToAdd { get; set; }
        public string AnswerToRemove { get; set; }
    }
}