using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDictionary.BDictionary.UI.Models.Questions
{
    public class QuestionPagedListViewModel : PagedList<QuestionListEntryViewModel>
    {
        public IEnumerable<QuestionListEntryViewModel> ListEntries { get; set; }
        public string ItemController { get; set; }

        public QuestionPagedListViewModel(IQueryable<QuestionListEntryViewModel> superset, int pageNumber, int pageSize)
        : base(superset, pageNumber, pageSize)
        {
            ListEntries = superset.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }

        public QuestionPagedListViewModel(IEnumerable<QuestionListEntryViewModel> superset, int pageNumber, int pageSize)
        : this(superset.AsQueryable<QuestionListEntryViewModel>(), pageNumber, pageSize)
        { }
    }
}