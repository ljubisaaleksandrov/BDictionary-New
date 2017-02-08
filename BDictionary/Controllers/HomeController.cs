using BDictionary.Business;
using BDictionary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDictionary.UI.Controllers
{
    public class HomeController : Controller
    {
        #region Fields
        private readonly IRegionService _regionService;
        #endregion

        #region Constructors
        public HomeController(IRegionService regionService)
        {
            _regionService = regionService;
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Info()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult RegionDetails(int id)
        {
            Region region = _regionService.GetRegion(id);

            return Json( new { regionName = region.Name, description = region.Description }, JsonRequestBehavior.AllowGet);
        }
    }
}