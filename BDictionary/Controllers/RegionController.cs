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
    public class RegionController : Controller
    {
        #region Fields
        private readonly IRegionService _regionService;
        #endregion

        #region Constructors
        public RegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }
        #endregion

        #region ActionMethods
        public ActionResult Index()
        {
            List<Region> regions = _regionService.GetAll("", "").ToList();
            return View(regions);
        }

        public ActionResult Edit(int id)
        {
            Region region = _regionService.GetRegion(id);
            return View(region);
        }

        [HttpPost]
        public ActionResult SaveRegion(Region region)
        {
            _regionService.AddOrUpdate(region);

            return RedirectToAction("Index");
        }
        #endregion
    }
}