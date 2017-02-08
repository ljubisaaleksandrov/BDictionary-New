using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDictionary.Domain;

namespace BDictionary.Business
{
    public class RegionService : IRegionService
    {
        public IList<Region> GetAll(string sortOrder, string searchString)
        {
            using (BDictionaryEntities db = new BDictionaryEntities())
            {
                return db.Regions.ToList();
            }
        }

        public Region GetRegion(int id)
        {
            using (BDictionaryEntities db = new BDictionaryEntities())
            {
                return db.Regions.FirstOrDefault(r => r.Id == id);
            }
        }

        public void AddOrUpdate(Region region)
        {
            using (BDictionaryEntities db = new BDictionaryEntities())
            {
                Region existingRegion = db.Regions.FirstOrDefault(r => r.Id == region.Id);

                if(existingRegion != null)
                {
                    existingRegion.Description = region.Description;
                    db.SaveChanges();
                }

            }
        }
    }
}
