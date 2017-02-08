using AutoMapper;
using BDictionary.Business;
using BDictionary.Domain;
using BDictionary.UI.Models.Categories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDictionary.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuestionCategoryController : Controller
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IQuestionCategoryService _categoryService;
        #endregion

        #region Constructors
        public QuestionCategoryController(IMapper mapper, IQuestionCategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }
        #endregion

        #region Action Methods
        public ActionResult Index(int? id)
        {
            QuestionCategory category = new QuestionCategory();
            QuestionCategoriesViewModelContainer model = new QuestionCategoriesViewModelContainer();
            if(id.HasValue)
            {
                category = _categoryService.GetQuestionCategory(id.Value);
            }

            model = MapCategoryToCategoryViewModel(category);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(QuestionCategoriesViewModelContainer model)
        {
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddOrUpdate(QuestionCategoriesViewModelContainer categoryModel)
        {
            int parentCategory = 0;
            if(!String.IsNullOrEmpty(categoryModel.QuestionCategoryViewModel.Parent))
            {
                parentCategory = _categoryService.GetQuestionCategory(categoryModel.QuestionCategoryViewModel.Parent).Id;
            }

            QuestionCategory category = new QuestionCategory()
            {
                Id = categoryModel.QuestionCategoryViewModel.Id,
                Name = categoryModel.QuestionCategoryViewModel.Name,
                ParentID = parentCategory,
                AnswerType = categoryModel.QuestionCategoryViewModel.QuestionAnswerType
            };

            List<QuestionAnswer> questionAnswers = new List<QuestionAnswer>();
            foreach (QuestionAnswerViewModel model in categoryModel.QuestionAnswerViewModel)
            {
                questionAnswers.Add(_mapper.Map<QuestionAnswer>(model));
            }

            List<Question> questions = new List<Question>();
            foreach (QuestionViewModel model in categoryModel.QuestionViewModel)
            {
                Question question = _mapper.Map<Question>(model);
                question.QuestionAnswer = new QuestionAnswer() { Value = model.Answer };
                questions.Add(question);
            }

            category.QuestionAnswers = questionAnswers;
            category.Questions = questions;

            _categoryService.AddOrUpdate(category);
            return RedirectToAction("Index", categoryModel);
        }

        [HttpGet]
        [Authorize]
        public JsonResult GetCategoryChildren(int catId, int? selCatId)
        {
            if (selCatId != null && catId == -1)
            {
                int selectedCategoryId = selCatId ?? default(int);
                var parents = _categoryService.GetAllParents(selectedCategoryId);
                var rootCategories = _categoryService.GetChildren(null).ToList();
                return Json(_categoryService.BuildTreeForSelectedItem(selectedCategoryId, parents, rootCategories), JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<QuestionCategory> children = catId != -1 ? _categoryService.GetChildren(catId) : _categoryService.GetChildren(null);
                var result = _categoryService.BuildTree(children);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        public JsonResult GetLeafCategories(string input)
        {
            IEnumerable<QuestionCategory> leafCategories = _categoryService.Search(input);
            dynamic[] content = new dynamic[leafCategories.Count()];
            int counter = 0;
            foreach (QuestionCategory category in leafCategories)
            {
                content[counter] = new { categoryName = $"{category.Name} ({category.Id})", id = category.Id, text = GetCategoryDisplayName(category) };
                counter++;
            }

            return Json(content, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult SelectCategory(QuestionCategoriesViewModelContainer model)
        {
            if(model.SelectedCategoryId != 0)
            {
                model.SelectedCategoryName = _categoryService.GetQuestionCategory(model.SelectedCategoryId).Name;
            }

            var answersList = model.QuestionAnswerViewModel.Select(qa => qa.Value).ToList();
            model.QuestionHelperViewModel.Answers = answersList;
            string defaultAnswer = "Select answer";
            List<string> answers = new List<string>() { defaultAnswer };
            answers.AddRange(answersList);
            model.QuestionHelperViewModel.Answers = answers;

            ModelState.Clear();
            return View("Index", model);
        }
        [Authorize]
        public ActionResult EditCategory(QuestionCategoriesViewModelContainer model)
        {
            if (model.SelectedCategoryId != 0)
            {
                return RedirectToAction("Index", new { Id = model.SelectedCategoryId });
            }
            
            var answersList = model.QuestionAnswerViewModel.Select(qa => qa.Value).ToList();
            model.QuestionHelperViewModel.Answers = answersList;
            string defaultAnswer = "Select answer";
            List<string> answers = new List<string>() { defaultAnswer };
            answers.AddRange(answersList);
            model.QuestionHelperViewModel.Answers = answers;

            ModelState.Clear();
            return View("Index", model);
        }
        [Authorize]
        public ActionResult RemoveCategory(QuestionCategoriesViewModelContainer model)
        {
            if (model.SelectedCategoryId != 0)
            {
                _categoryService.RemoveCategory(model.SelectedCategoryId);
            }

            var answersList = model.QuestionAnswerViewModel.Select(qa => qa.Value).ToList();
            model.QuestionHelperViewModel.Answers = answersList;
            string defaultAnswer = "Select answer";
            List<string> answers = new List<string>() { defaultAnswer };
            answers.AddRange(answersList);
            model.QuestionHelperViewModel.Answers = answers;

            ModelState.Clear();
            return View("Index", model);
        }

        [Authorize]
        public ActionResult AddQuestionAnswer(QuestionCategoriesViewModelContainer model)
        {
            if(!String.IsNullOrEmpty(model.QuestionAnswerHelperViewModel.AnswerToAdd))
            {
                QuestionAnswerViewModel answerModel = new QuestionAnswerViewModel()
                {
                    CategoryID = model.QuestionCategoryViewModel.Id,
                    Value = model.QuestionAnswerHelperViewModel.AnswerToAdd,
                    IsPrimary = false
                };

                model.QuestionAnswerViewModel.Add(answerModel);
            }
            model.QuestionAnswerHelperViewModel.AnswerToAdd = "";
            model.QuestionAnswerHelperViewModel.AnswerToRemove = "";

            var answersList = model.QuestionAnswerViewModel.Select(qa => qa.Value).ToList();
            model.QuestionHelperViewModel.Answers = answersList;

            string defaultAnswer = "Select answer";
            List<string> answers = new List<string>() { defaultAnswer };
            answers.AddRange(answersList);
            model.QuestionHelperViewModel.Answers = answers;

            ModelState.Clear();
            return View("Index", model);
        }
        [Authorize]
        public ActionResult RemoveQuestionAnswer(QuestionCategoriesViewModelContainer model)
        {
            if (!String.IsNullOrEmpty(model.QuestionAnswerHelperViewModel.AnswerToRemove))
            {
                QuestionAnswerViewModel existingAnswer = model.QuestionAnswerViewModel.Where(qa => qa.Value == model.QuestionAnswerHelperViewModel.AnswerToRemove.Trim()).FirstOrDefault();
                if (existingAnswer != null)
                    model.QuestionAnswerViewModel.Remove(existingAnswer);
            }
            model.QuestionAnswerHelperViewModel.AnswerToAdd = "";
            model.QuestionAnswerHelperViewModel.AnswerToRemove = "";
            var answersList = model.QuestionAnswerViewModel.Select(qa => qa.Value).ToList();
            model.QuestionHelperViewModel.Answers = answersList;

            string defaultAnswer = "Select answer";
            List<string> answers = new List<string>() { defaultAnswer };
            answers.AddRange(answersList);
            model.QuestionHelperViewModel.Answers = answers;

            ModelState.Clear();
            return View("Index", model);
        }

        [Authorize]
        public ActionResult AddQuestion(QuestionCategoriesViewModelContainer model)
        {
            if (!String.IsNullOrEmpty(model.QuestionHelperViewModel.QuestionToAdd))
            {
                QuestionViewModel Model = new QuestionViewModel()
                {
                    CategoryID = model.QuestionCategoryViewModel.Id,
                    Value = model.QuestionHelperViewModel.QuestionToAdd,
                    Answer = model.QuestionHelperViewModel.SelectedAnswer,
                    IsShiz = model.QuestionHelperViewModel.IsShiz,
                    Creator = User.Identity.GetUserId()
                };

                model.QuestionViewModel.Add(Model);
                model.QuestionAnswerViewModel.Where(qa => qa.Value == model.QuestionHelperViewModel.SelectedAnswer).FirstOrDefault().IsPrimary = true;
            }
            model.QuestionHelperViewModel.QuestionToAdd = "";
            model.QuestionHelperViewModel.QuestionToRemove = "";

            var answersList = model.QuestionAnswerViewModel.Select(qa => qa.Value).ToList();
            string defaultAnswer = "Select answer";
            List<string> answers = new List<string>() { defaultAnswer };
            answers.AddRange(answersList);
            model.QuestionHelperViewModel.SelectedAnswer = defaultAnswer;
            model.QuestionHelperViewModel.Answers = answers;

            ModelState.Clear();
            return View("Index", model);
        }
        [Authorize]
        public ActionResult RemoveQuestion(QuestionCategoriesViewModelContainer model)
        {
            if (!String.IsNullOrEmpty(model.QuestionHelperViewModel.QuestionToRemove))
            {
                QuestionViewModel existing = model.QuestionViewModel.Where(qa => qa.Value == model.QuestionHelperViewModel.QuestionToRemove.Trim()).FirstOrDefault();
                if (existing != null)
                {
                    var answer = model.QuestionAnswerViewModel.Where(qa => qa.Value == existing.Answer).FirstOrDefault();
                    answer.IsPrimary = model.QuestionViewModel.Any(q => q.Answer == answer.Value);

                    model.QuestionViewModel.Remove(existing);
                }
            }
            model.QuestionHelperViewModel.QuestionToAdd = "";
            model.QuestionHelperViewModel.QuestionToRemove = "";

            var answersList = model.QuestionAnswerViewModel.Select(qa => qa.Value).ToList();
            string defaultAnswer = "Select answer";
            List<string> answers = new List<string>() { defaultAnswer };
            answers.AddRange(answersList);
            model.QuestionHelperViewModel.SelectedAnswer = defaultAnswer;
            model.QuestionHelperViewModel.Answers = answers;

            ModelState.Clear();
            return View("Index", model);
        }

        #endregion

        #region HelperMethods

        [NonAction]
        private string GetCategoryDisplayName(QuestionCategory category)
        {
            string result = $"{category.Name} ({category.Id})";
            category = _categoryService.GetParent(category.Id);
            while (category != null)
            {
                result = category.Name.Substring(0, category.Name.Length > 30 ? 30 : category.Name.Length) + " > " + result;
                category = _categoryService.GetParent(category.Id);
            }
            return result;
        }

        private QuestionCategoriesViewModelContainer MapCategoryToCategoryViewModel(QuestionCategory category)
        {
            QuestionCategoriesViewModelContainer model = new QuestionCategoriesViewModelContainer();

            model.QuestionCategoryViewModel = _mapper.Map<QuestionCategoryViewModel>(category);


            if(category.Id != 0)
            {
                foreach (QuestionAnswer answer in _categoryService.GetAnswers(category.Id).ToList())
                {
                    QuestionAnswerViewModel answerModel = _mapper.Map<QuestionAnswerViewModel>(answer);
                    answerModel.IsPrimary = _categoryService.IsPrimaryAnswer(answer.Id);
                    model.QuestionAnswerViewModel.Add(answerModel);
                }
                foreach (Question question in _categoryService.GetQuestions(category.Id).ToList())
                {
                    QuestionViewModel questionModel = _mapper.Map<QuestionViewModel>(question);
                    questionModel.Answer = question.QuestionAnswer.Value;
                    model.QuestionViewModel.Add(questionModel);
                }
            }

            string defaultAnswer = "Select answer";
            List<string> answers = new List<string>() { defaultAnswer };
            answers.AddRange(model.QuestionAnswerViewModel.Select(qa => qa.Value).ToList());
            model.QuestionHelperViewModel.Answers = answers;
            model.QuestionHelperViewModel.SelectedAnswer = defaultAnswer;

            model.SelectedCategoryId = 0;
            model.SelectedCategoryName = "";

            return model;
        }

        #endregion
    }
}