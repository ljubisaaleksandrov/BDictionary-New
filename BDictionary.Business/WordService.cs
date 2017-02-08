using BDictionary.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDictionary.Business
{
    class WordService : IWordService
    {
        public void AddOrUpdate(Word word)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Word> GetAll(string sortOrder, string searchString, string typeSelected)
        {
            using (BDictionaryEntities db = new BDictionaryEntities())
            {
                IQueryable<Word> words = db.Words.Include(x => x.WordType);

                if (!String.IsNullOrEmpty(searchString))
                {
                    words = words.Where(x => x.Value.Contains(searchString) || x.Description.Contains(searchString) || x.Example.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "value_desc":
                        words = words.OrderByDescending(x => x.Value);
                        break;
                    case "description":
                        words = words.OrderBy(x => x.Description);
                        break;
                    case "description_desc":
                        words = words.OrderByDescending(x => x.Description);
                        break;
                    case "type":
                        words = words.OrderBy(x => x.WordType.Value);
                        break;
                    case "type_desc":
                        words = words.OrderByDescending(x => x.WordType.Value);
                        break;
                    default:
                        words = words.OrderBy(x => x.Value);
                        break;
                }

                return words.ToList();
            }
        }

        public Word GetWord(int id)
        {
            throw new NotImplementedException();
        }
    }
}
