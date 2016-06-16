using System;
using System.Collections.Generic;
using BDictionary.Domain;
using Autofac;
using System.Linq;
using System.Data.Entity;

namespace BDictionary.Business
{
    class QuestionCategoryService : IQuestionCategoryService
    {
        #region Fields
        private readonly IComponentContext _componentContext;
        #endregion

        #region Constructors
        public QuestionCategoryService(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }
        #endregion

        #region Interface Methods
        public void AddOrUpdate(QuestionCategory category)
        {
            using (BDictionaryEntities db = new BDictionaryEntities())
            {
                QuestionCategory existingCategory = db.QuestionCategories.FirstOrDefault(qc => qc.Id == category.Id);
                if (existingCategory != null)
                {
                    existingCategory.Name = category.Name;
                    existingCategory.ParentID = category.ParentID != 0 ? category.ParentID : null;
                    existingCategory.AnswerType = category.AnswerType;

                    List<QuestionAnswer> existingAnswers = db.QuestionAnswers.Where(qa => qa.CategoryID == category.Id).ToList();
                    foreach (QuestionAnswer existingAnswer in existingAnswers)
                    {
                        if (!category.QuestionAnswers.Any(qa => qa.Id == existingAnswer.Id))
                            db.QuestionAnswers.Remove(existingAnswer);
                    }

                    foreach (QuestionAnswer modelAnswer in category.QuestionAnswers)
                    {
                        QuestionAnswer existingAnswer = db.QuestionAnswers.Where(qa => qa.Id == modelAnswer.Id).FirstOrDefault();
                        if (existingAnswer == null)
                        {
                            QuestionAnswer newAnswer = new QuestionAnswer()
                            {
                                CategoryID = category.Id,
                                Value = ParseAnswer(modelAnswer.Value, category.AnswerType)
                            };

                            db.QuestionAnswers.Add(newAnswer);
                        }
                        else
                        {
                            existingAnswer.Value = ParseAnswer(modelAnswer.Value, category.AnswerType);
                        }

                        db.SaveChanges();
                    }
                }
                else
                {
                    QuestionCategory newCategory = new QuestionCategory()
                    {
                        Name = category.Name,
                        ParentID = category.ParentID != 0 ? category.ParentID : null,
                        AnswerType = category.AnswerType
                    };

                    db.SaveChanges();

                    foreach (QuestionAnswer modelAnswer in category.QuestionAnswers)
                    {
                        modelAnswer.CategoryID = newCategory.Id;
                        db.QuestionAnswers.Add(modelAnswer);
                    }

                    db.QuestionCategories.Add(newCategory);
                }

                db.SaveChanges();
            }
        }
        public object BuildNode(QuestionCategory category, bool isOpened = false, bool isSelected = false, dynamic[] children = null)
        {
            if (children == null)
            {
                return new
                {
                    id = category.Id,                       // required
                    parent = category.ParentID != null ? category.ParentID.ToString() : "#", // required
                    text = $"{category.Name} ({category.Id})",
                    state = new { opened = isOpened, selected = isSelected },
                    children = HasChild(category.Id),       // node text
                    isLeaf = HasChild(category.Id)
                };
            }
            else
            {
                return new
                {
                    id = category.Id,               // required
                    parent = category.ParentID != null ? category.ParentID.ToString() : "#", // required
                    text = $"{category.Name} ({category.Id})",
                    state = new { opened = isOpened, selected = isSelected },
                    children = children,            // node text
                    isLeaf = HasChild(category.Id)
                };
            }
        }

        public dynamic[] BuildTree(List<QuestionCategory> categories)
        {
            dynamic[] content = new dynamic[categories.Count()];
            for (int i = 0; i < categories.Count(); i++)
                content[i] = BuildNode(categories[i]);

            return content;
        }

        public dynamic[] BuildTreeForSelectedItem(int selectedCategoryId, List<int> parents, List<QuestionCategory> categories)
        {
            dynamic[] content = new dynamic[categories.Count()];
            for (int i = 0; i < categories.Count(); i++)
            {
                if (parents.Contains(categories[i].Id))
                {
                    var childrenNodes = BuildTreeForSelectedItem(selectedCategoryId, parents, GetChildren(categories[i].Id).ToList());
                    content[i] = BuildNode(categories[i], true, selectedCategoryId == categories[i].Id, childrenNodes);
                }
                else
                {
                    content[i] = BuildNode(categories[i], false, selectedCategoryId == categories[i].Id);
                }
            }

            return content;
        }

        public List<QuestionCategory> GetChildren(int? parentId)
        {
            List<QuestionCategory> result = new List<QuestionCategory>();
            using (BDictionaryEntities db = new BDictionaryEntities())
            {
                foreach (QuestionCategory c in db.QuestionCategories.Where(c => c.ParentID == parentId))
                {
                    result.Add(c);
                }
            }
            return result;
        }

        public QuestionCategory GetParent(int categoryId)
        {
            using (BDictionaryEntities db = new BDictionaryEntities())
            {
                var parentId = db.QuestionCategories.FirstOrDefault(c => c.Id == categoryId).ParentID;
                return db.QuestionCategories.FirstOrDefault(c => c.Id == parentId);
            }
        }

        public List<int> GetAllParents(int categoryId)
        {
            List<int> parents = new List<int>();
            var currentparent = GetParent(categoryId);
            while (currentparent != null)
            {
                parents.Add(currentparent.Id);
                currentparent = GetParent(currentparent.Id);
            }
            return parents;
        }

        public IEnumerable<QuestionAnswer> GetAnswers(int categoryId)
        {
            using (BDictionaryEntities db = new BDictionaryEntities())
            {
                return db.QuestionCategories.Include(x => x.QuestionAnswers).Include(x => x.Questions).Where(c => c.Id == categoryId).FirstOrDefault().QuestionAnswers;
            }
        }

        public QuestionCategory GetQuestionCategory(int categoryId)
        {
            using (BDictionaryEntities db = new BDictionaryEntities())
            {
                return db.QuestionCategories.Where(c => c.Id == categoryId).FirstOrDefault();
            }
        }

        public QuestionCategory GetQuestionCategory(string categoryName)
        {
            using (BDictionaryEntities db = new BDictionaryEntities())
            {
                return db.QuestionCategories.Where(c => c.Name == categoryName).FirstOrDefault();
            }
        }

        public IEnumerable<Question> GetQuestions(int categoryId)
        {
            using (BDictionaryEntities db = new BDictionaryEntities())
            {
                return db.QuestionCategories.Include(x => x.QuestionAnswers).Where(c => c.Id == categoryId).FirstOrDefault().Questions;
            }
        }

        public bool HasChild(int categoryId)
        {
            using (BDictionaryEntities db = new BDictionaryEntities())
            {
                return db.QuestionCategories.Any(c => c.ParentID == categoryId);
            }
        }

        public IEnumerable<QuestionCategory> Search(string input)
        {
            using (BDictionaryEntities db = new BDictionaryEntities())
            {
                return db.QuestionCategories.Where(c => (c.Name.Contains(input) || c.Id.ToString().StartsWith(input)) && !c.QuestionCategory1.Any()).OrderBy(c => c.Name).Take(20).ToList();
            }
        }
        #endregion

        #region Helper Methods

        private string ParseAnswer(string value, int answerType)
        {
            string result = value.Trim();
            if(!String.IsNullOrEmpty(result))
            {
                if (answerType == (int)Domain.HelperModels.Enums.QuestionAnswerType.Number)
                {
                    int intResult;

                    if (Int32.TryParse(value.Trim(), out intResult))
                        return intResult.ToString();
                    else
                        result = "";
                }
            }
            return result;
        }

        #endregion
    }
}
