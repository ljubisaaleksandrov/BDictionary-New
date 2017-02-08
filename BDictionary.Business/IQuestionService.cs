using BDictionary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDictionary.Business
{
    public interface IQuestionService
    {
        Question GetQuestion(int id);
        IList<Question> GetAll(string sortOrder, string searchString, string venueSelected);
        IList<Question> GetAllByQuestionCategory(int categoryId);
        void AddOrUpdate(Question question);
        bool Delete(int id);
    }
}
