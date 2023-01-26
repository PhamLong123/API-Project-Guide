//using CRMManager.Models;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class CouponController : Controller
//    {
//        private readonly IWebHostEnvironment _webhost;
//        public CouponController(IWebHostEnvironment webHost)
//        {
//            _webhost = webHost;
//        }
//        public IActionResult Index()
//        {
//            ViewBag.PageTitle = "COUPON";
//            CouponsViewModel model = new CouponsViewModel();
//            return View(model);
//        }
//        public IActionResult AddNew(CouponViewModel model)
//        {
//            ViewBag.PageTitle = "THÊM COUPON";
//            return View(model);
//        }
//        [HttpPost]
//        public IActionResult AddNew()
//        {
//            return RedirectToAction("Index");
//        }

//        public IActionResult Edit(int ID)
//        {
//            ViewBag.PageTitle = "SỬA THÔNG TIN COUPON";
//            var model = new CouponsViewModel().Coupons.Where(x => x.ID == ID).FirstOrDefault();
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
//        [HttpPost]
//        public IActionResult Uploadicon(IFormFile imgfile)
//        {
//            var saveimg = Path.Combine(_webhost.WebRootPath, "Dat'sImages", "Coupon", imgfile.FileName);
//            string imgext = Path.GetExtension(imgfile.FileName);
//            if (imgext == ".jpg" || imgext == ".png")
//            {
//                using (var uploadimg = new FileStream(saveimg, FileMode.Create))
//                {
//                    imgfile.CopyTo(uploadimg);
//                    ViewData["Message"] = "Ảnh" + imgfile.FileName + "đã được lưu";
//                }
//            }

//            return RedirectToAction("Index");
//        }
//        [HttpPost]
//        public IActionResult Uploadimage(IFormFile imgfile)
//        {
//            var saveimg = Path.Combine(_webhost.WebRootPath, "Dat'sImages", "Coupon", imgfile.FileName);
//            string imgext = Path.GetExtension(imgfile.FileName);
//            if (imgext == ".jpg" || imgext == ".png")
//            {
//                using (var uploadimg = new FileStream(saveimg, FileMode.Create))
//                {
//                    imgfile.CopyTo(uploadimg);
//                    ViewData["Message"] = "Ảnh" + imgfile.FileName + "đã được lưu";
//                }
//            }

//            return RedirectToAction("Index");
//        }
//    }
//}

