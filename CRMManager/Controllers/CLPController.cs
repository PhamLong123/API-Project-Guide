//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class CLPController : Controller
//    {
//        public IActionResult Index()
//        {
//            ViewBag.PageTitle = "CHƯƠNG TRÌNH KHÁCH HÀNG THÂN THIẾT";
//            CLPsViewModel model = new CLPsViewModel();
//            return View(model);
//        }
//        public IActionResult AddNew(CLPViewModel model)
//        {
//            ViewBag.PageTitle = "THÊM CHƯƠNG TRÌNH KHÁCH HÀNG THÂN THIẾT";
//            return View(model);
//        }
//        [HttpPost]
//        public IActionResult AddNew()
//        {
//            return RedirectToAction("Index");
//        }
//        public IActionResult Edit(int ID)
//        {
//            ViewBag.PageTitle = "SỬA THÔNG TIN CHƯƠNG TRÌNH KHÁCH HÀNG THÂN THIẾT";
//            var model = new CLPsViewModel().CLPs.Where(x => x.ID == ID).FirstOrDefault();
//            return View(model);
//        }
//        [HttpPost]
//        public IActionResult Edit()
//        {
//            return RedirectToAction("Index");
//        }
//        [HttpPost]
//        public IActionResult Delete()
//        {
//            return RedirectToAction("Index");
//        }
//    }
//}
