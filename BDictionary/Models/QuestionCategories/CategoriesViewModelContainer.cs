using BDictionary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDictionary.UI.Models.Categories
{
    public class QuestionCategoriesViewModelContainer : IQuestionCategoryViewModelContainer,
                                                        IQuestionAnswerViewModelContainer,
                                                        IQuestionViewModelContainer
    {
        public QuestionCategoriesViewModelContainer()
        {
            QuestionCategoryTreeViewModel = new QuestionCategoryTreeViewModel();
            QuestionCategoryViewModel = new QuestionCategoryViewModel();
            QuestionAnswerViewModel = new List<QuestionAnswerViewModel>();
            QuestionAnswerHelperViewModel = new QuestionAnswerHelperViewModel();
            QuestionViewModel = new List<QuestionViewModel>();
            QuestionHelperViewModel = new QuestionHelperViewModel();
        }

        public QuestionCategoryTreeViewModel QuestionCategoryTreeViewModel { get; set; }
        public QuestionCategoryViewModel QuestionCategoryViewModel { get; set; }

        public List<QuestionAnswerViewModel> QuestionAnswerViewModel { get; set; }
        public QuestionAnswerHelperViewModel QuestionAnswerHelperViewModel { get; set; }

        public List<QuestionViewModel> QuestionViewModel { get; set; }
        public QuestionHelperViewModel QuestionHelperViewModel { get; set; }

        public string SelectedCategoryName { get; set; }
        public int SelectedCategoryId { get; set; }
    }
}