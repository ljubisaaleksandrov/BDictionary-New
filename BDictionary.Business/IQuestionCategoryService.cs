using BDictionary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDictionary.Business
{
    public interface IQuestionCategoryService
    {
        QuestionCategory GetQuestionCategory(int categoryId);
        QuestionCategory GetQuestionCategory(string categoryName);
        IEnumerable<QuestionCategory> Search(string input);
        void AddOrUpdate(QuestionCategory category);
        void RemoveCategory(int categoryId);

        IEnumerable<QuestionAnswer> GetAnswers(int categoryId);
        bool IsPrimaryAnswer(int answerId);
        IEnumerable<Question> GetQuestions(int categoryId);
        
        dynamic[] BuildTree(List<QuestionCategory> categories);
        dynamic[] BuildTreeForSelectedItem(int selectedCategoryId, List<int> parents, List<QuestionCategory> categories);
        object BuildNode(QuestionCategory category, bool isOpened = false, bool isSelected = false, dynamic[] children = null);
        List<QuestionCategory> GetChildren(int? parentId);
        QuestionCategory GetParent(int categoryId);
        List<int> GetAllParents(int categoryId);
        bool HasChild(int categoryId);
    }
}
