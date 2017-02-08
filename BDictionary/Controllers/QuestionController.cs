using AutoMapper;
using BDictionary.BDictionary.UI.Models.Questions;
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
    public class QuestionController : Controller
    {
        #region Fields
        private readonly IQuestionService _questionService;
        private readonly IQuestionCategoryService _questionCategoryService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public QuestionController(IQuestionService questionService, IQuestionCategoryService questionCategoryService, IMapper mapper)
        {
            _questionService = questionService;
            _questionCategoryService = questionCategoryService;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        [Authorize]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize, string venueSelected)
        {
            if (searchString != null)
                searchString = searchString.Trim();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ValueSortParm = String.IsNullOrEmpty(sortOrder) ? "question_value_desc" : "";
            ViewBag.AnswerSortParm = sortOrder == "answer" ? "answer_desc" : "answer";
            ViewBag.CategorySortParm = sortOrder == "category" ? "category_desc" : "category";
            ViewBag.ShizSortParm = sortOrder == "shiz" ? "shiz_desc" : "shiz";
            ViewBag.CurrentItemsPerPage = pageSize = pageSize == null ? 10 : (int)pageSize;
            int pageNumber = (page ?? 1);

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.SortOrder = sortOrder;
            List<QuestionListEntryViewModel> items = new List<QuestionListEntryViewModel>();
            foreach (Question question in _questionService.GetAll(sortOrder, searchString, venueSelected))
            {
                QuestionListEntryViewModel item = _mapper.Map<QuestionListEntryViewModel>(question);
                items.Add(item);
            }

            QuestionListViewModel model = new QuestionListViewModel() { PagedListModel = new QuestionPagedListViewModel(items, pageNumber, (int)pageSize) };
            model.PagedListModel.ItemController = "Question";

            //List<string> venues = new List<string>() { "Select Venue" };
            //venues.AddRange(_venueService.GetAllVenues().Select(v => v.VenueName));
            //model.PagedListModel.Venues = venues.Count > 1 ? venues : null;

            return View(model.PagedListModel);
        }

        [Authorize]
        public ActionResult Delete(int id, string returnUrl)
        {
            if (id != 0)
            {
                _questionService.Delete(id);
            }

            return Redirect(returnUrl);
        }

        [Authorize]
        public ActionResult Edit(int id, string returnUrl)
        {
            Question question = _questionService.GetQuestion(id);
            QuestionViewModel model = _mapper.Map<QuestionViewModel>(question);

            ViewBag.Answers = _questionCategoryService.GetAnswers(model.CategoryID).Select(a => a.Value).ToList();
            ViewBag.ReturnUrl = returnUrl;

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(QuestionViewModel model)
        {
            Question question = _mapper.Map<Question>(model);
            _questionService.AddOrUpdate(question);

            return View("Index");
        }

        #endregion
    }
}