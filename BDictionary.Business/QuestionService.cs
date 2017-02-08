using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDictionary.Domain;
using System.Data.Entity;

namespace BDictionary.Business
{
    class QuestionService : IQuestionService
    {
        public void AddOrUpdate(Question question)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            using (BDictionaryEntities db = new BDictionaryEntities())
            {
                Question question = db.Questions.FirstOrDefault(q => q.Id == id);

                if (question != null)
                {
                    db.Questions.Remove(question);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public IList<Question> GetAll(string sortOrder, string searchString, string categorySelected)
        {
            using (BDictionaryEntities db = new BDictionaryEntities())
            {
                IQueryable<Question> questions = db.Questions.Include(x => x.QuestionAnswer).Include(x => x.QuestionCategory);

                if (!String.IsNullOrEmpty(searchString))
                {
                    questions = questions.Where(x => x.Value.Contains(searchString) || x.QuestionAnswer.Value.Contains(searchString) || x.QuestionCategory.Name.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "question_value_desc":
                        questions = questions.OrderByDescending(x => x.Value);
                        break;
                    case "answer":
                        questions = questions.OrderBy(x => x.QuestionAnswer.Value);
                        break;
                    case "answer_desc":
                        questions = questions.OrderByDescending(x => x.QuestionAnswer.Value);
                        break;
                    case "category":
                        questions = questions.OrderBy(x => x.QuestionCategory.Name);
                        break;
                    case "category_desc":
                        questions = questions.OrderByDescending(x => x.QuestionCategory.Name);
                        break;
                    case "shiz":
                        questions = questions.OrderBy(x => x.IsShiz);
                        break;
                    case "shiz_desc":
                        questions = questions.OrderByDescending(x => x.IsShiz);
                        break;
                    default:
                        questions = questions.OrderBy(x => x.Value);
                        break;
                }

                return questions.ToList();
            }
        }

        public IList<Question> GetAllByQuestionCategory(int categoryId)
        {
            using (BDictionaryEntities db = new BDictionaryEntities())
            {
                return db.Questions.Include(x => x.QuestionAnswer).Include(x => x.QuestionCategory).Where(q => q.CategoryID == categoryId).ToList();
            }
        }

        public Question GetQuestion(int id)
        {
            using (BDictionaryEntities db = new BDictionaryEntities())
            {
                return db.Questions.Include(x => x.QuestionAnswer).Include(x => x.QuestionCategory).FirstOrDefault(q => q.Id == id);
            }
        }
    }
}
