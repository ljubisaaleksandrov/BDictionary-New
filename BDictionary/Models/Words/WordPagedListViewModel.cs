using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDictionary.BDictionary.UI.Models.Word
{
    public class WordPagedListViewModel : PagedList<WordListEntryViewModel>
    {
        public IEnumerable<WordListEntryViewModel> ListEntries { get; set; }
        public string ItemController { get; set; }

        public WordPagedListViewModel(IQueryable<WordListEntryViewModel> superset, int pageNumber, int pageSize)
        : base(superset, pageNumber, pageSize)
        {
            ListEntries = superset.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }

        public WordPagedListViewModel(IEnumerable<WordListEntryViewModel> superset, int pageNumber, int pageSize)
        : this(superset.AsQueryable<WordListEntryViewModel>(), pageNumber, pageSize)
        { }
    }
}