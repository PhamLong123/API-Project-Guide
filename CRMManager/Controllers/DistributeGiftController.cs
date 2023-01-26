//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using SmartBreadcrumbs.Attributes;

//namespace CRMManager.Controllers
//{
//    public class DistributeGiftController : Controller
//    {
//        [Breadcrumb("Phát/sử dụng quà")]
//        public IActionResult Index()
//        {
//            ViewBag.Message = TempData["RedeemCode"];
//            DistributeRedemptionGiftViewModel data = new DistributeRedemptionGiftViewModel();
//            data.PageTitle = "Phát/sử dụng quà";
//            return View(data);
//        }

//        [HttpPost]
//        public IActionResult DistributeGift(string redeemCode)
//        {
//            TempData["RedeemCode"] = redeemCode;
//            DistributeRedemptionGiftViewModel data = new DistributeRedemptionGiftViewModel();
//            data = data.GetData();
//            return View("Index", data);
//        }
//    }
//}
