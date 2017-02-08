using BDictionary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDictionary.Business
{
    public interface IRegionService
    {
        Region GetRegion(int id);
        IList<Region> GetAll(string sortOrder, string searchString);
        void AddOrUpdate(Region region);
    }
}
