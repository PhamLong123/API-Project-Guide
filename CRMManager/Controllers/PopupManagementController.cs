using CRMManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRMManager.Controllers
{
    public class PopupManagementController : Controller
    {
        public IActionResult Index()
        {
            PopupContentViewModel data = new PopupContentViewModel();
            data = data.GetDefaultData();
            data.PageTitle = "Quản lý hộp thoại popup";
            return View(data);
        }
        public IActionResult Detail()
        {
            PopupContentViewModel data = new PopupContentViewModel();
            data = data.GetData();
            return View("Index", data);
        }

        public IActionResult AddNew()
        {
            PopupContentViewModel data = new PopupContentViewModel();
            data = data.GetDefaultData();
            return View("Index", data);
        }

        [HttpPost]
        public IActionResult SavePopup(PopupContentViewModel model)
        {
            PopupContentViewModel data = new PopupContentViewModel();
            data = model;

            return RedirectToAction("Index");
            //return View("PromotionNewsManagement", data);
        }
    }
}
