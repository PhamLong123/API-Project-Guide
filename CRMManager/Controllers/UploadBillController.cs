//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace CRMManager.Controllers
//{
//    public class UploadBillController : Controller
//    {
//        public UploadBillController()
//        {

//        }
//        public IActionResult Index()
//        {
//            ViewBag.Message = TempData["Mobile"];
//            UploadBillViewModel data = new UploadBillViewModel();
//            data.PageTitle = "Tải hóa đơn khách hàng";
//            return View(data);
//        }

//        public IActionResult SearchBillByMobile(string mobile)
//        {
//            TempData["Mobile"] = mobile;
//            UploadBillViewModel data = new UploadBillViewModel();
//            data = data.GetData();
//            return View("Index", data);
//        }

//        [HttpPost]
//        public IActionResult SaveUploadBill(UploadBillViewModel model)
//        {
//            return RedirectToAction("Index");
//        }
//    }
//}
