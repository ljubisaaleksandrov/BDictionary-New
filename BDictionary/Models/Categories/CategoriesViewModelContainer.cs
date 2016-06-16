using BDictionary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDictionary.UI.Models.Categories
{
    public class QuestionCategoriesViewModelContainer : IQuestionCategoryViewModelContainer,
                                                        IQuestionAnswerViewModelContainer
    {
        public QuestionCategoriesViewModelContainer()
        {
            QuestionCategoryTreeViewModel = new QuestionCategoryTreeViewModel();
            QuestionCategoryViewModel = new QuestionCategoryViewModel();
            QuestionAnswerViewModel = new List<QuestionAnswerViewModel>();
            QuestionAnswerHelper = new QuestionAnswerHelper();
        }

        public QuestionCategoryTreeViewModel QuestionCategoryTreeViewModel { get; set; }
        public QuestionCategoryViewModel QuestionCategoryViewModel { get; set; }
        public List<QuestionAnswerViewModel> QuestionAnswerViewModel { get; set; }
        public QuestionAnswerHelper QuestionAnswerHelper { get; set; }
    }
}