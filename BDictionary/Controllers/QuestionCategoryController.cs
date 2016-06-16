using AutoMapper;
using BDictionary.Business;
using BDictionary.Domain;
using BDictionary.UI.Models.Categories;
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
            QuestionCategoriesViewModelContainer model = new QuestionCategoriesViewModelContainer();
            if(id.HasValue)
            {
                QuestionCategory category = _categoryService.GetQuestionCategory(id.Value);
                model = MapCategoryToCategoryViewModel(category);
            }

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
            if(String.IsNullOrEmpty(categoryModel.QuestionCategoryViewModel.Parent))
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
            foreach(QuestionAnswerViewModel model in categoryModel.QuestionAnswerViewModel)
            {
                questionAnswers.Add(_mapper.Map<QuestionAnswer>(model));
            }

            category.QuestionAnswers = questionAnswers;
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
        public ActionResult AddQuestionAnswer(QuestionCategoriesViewModelContainer model)
        {
            if(!String.IsNullOrEmpty(model.QuestionAnswerHelper.AnswerToAdd))
            {
                QuestionAnswerViewModel answerModel = new QuestionAnswerViewModel()
                {
                    CategoryID = model.QuestionCategoryViewModel.Id,
                    Value = model.QuestionAnswerHelper.AnswerToAdd,
                    IsPrimary = false
                };

                model.QuestionAnswerViewModel.Add(answerModel);
            }
            model.QuestionAnswerHelper.AnswerToAdd = "";
            model.QuestionAnswerHelper.AnswerToRemove = "";
            ModelState.Clear();
            return View("Index", model);
        }


        [Authorize]
        public ActionResult RemoveQuestionAnswer(QuestionCategoriesViewModelContainer model)
        {
            if (!String.IsNullOrEmpty(model.QuestionAnswerHelper.AnswerToRemove))
            {
                QuestionAnswerViewModel existingAnswer = model.QuestionAnswerViewModel.Where(qa => qa.Value == model.QuestionAnswerHelper.AnswerToRemove.Trim()).FirstOrDefault();
                if (existingAnswer != null)
                    model.QuestionAnswerViewModel.Remove(existingAnswer);
            }
            model.QuestionAnswerHelper.AnswerToAdd = "";
            model.QuestionAnswerHelper.AnswerToRemove = "";
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
            if (category != null)
            {
                model.QuestionCategoryViewModel = _mapper.Map<QuestionCategoryViewModel>(category);
                foreach (QuestionAnswer answer in _categoryService.GetAnswers(category.Id).ToList())
                {
                    QuestionAnswerViewModel answerModel = _mapper.Map<QuestionAnswerViewModel>(answer);
                    answerModel.IsPrimary = answer.Questions.Any();
                    model.QuestionAnswerViewModel.Add(answerModel);
                }
            }

            return model;
        }

        #endregion


    }
}