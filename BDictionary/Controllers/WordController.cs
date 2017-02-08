using AutoMapper;
using BDictionary.BDictionary.UI.Models.Word;
using BDictionary.Business;
using BDictionary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDictionary.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WordController : Controller
    {
        #region Fields
        private readonly IWordService _wordService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public WordController(IWordService wordService, IMapper mapper)
        {
            _wordService = wordService;
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
            ViewBag.ValueSortParm = String.IsNullOrEmpty(sortOrder) ? "value_desc" : "";
            ViewBag.DescriptionSortParm = sortOrder == "description" ? "description_desc" : "description";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
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
            List<WordListEntryViewModel> items = new List<WordListEntryViewModel>();
            foreach (Word question in _wordService.GetAll(sortOrder, searchString, venueSelected))
            {
                WordListEntryViewModel item = _mapper.Map<WordListEntryViewModel>(question);
                items.Add(item);
            }

            WordListViewModel model = new WordListViewModel() { PagedListModel = new WordPagedListViewModel(items, pageNumber, (int)pageSize) };
            model.PagedListModel.ItemController = "Word";

            //List<string> venues = new List<string>() { "Select Venue" };
            //venues.AddRange(_venueService.GetAllVenues().Select(v => v.VenueName));
            //model.PagedListModel.Venues = venues.Count > 1 ? venues : null;

            return View(model.PagedListModel);
        }
        #endregion
    }
}