using BDictionary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDictionary.Business
{
    public interface IWordService
    {
        Word GetWord(int id);
        IList<Word> GetAll(string sortOrder, string searchString, string typeSelected);
        void AddOrUpdate(Word word);
        bool Delete(int id);
    }
}
