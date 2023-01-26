//using CRMManager.Models;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using SmartBreadcrumbs.Attributes;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class LotteryController : Controller
//    {
//        private readonly IWebHostEnvironment _webhost;
//        public LotteryController(IWebHostEnvironment webHost)
//        {
//            _webhost = webHost;
//        }
//        #region Program
//        [Breadcrumb("Chương trình quay số")]
//        public IActionResult Index()
//        {
//            LotteryProgramsViewModel model = new LotteryProgramsViewModel();
//            return View(model);
//        }
        
//        public IActionResult Program()
//        {
//            LotteryProgramsViewModel model = new LotteryProgramsViewModel();
//            return View(model);
//        }
//        [Breadcrumb("Thêm chương trình quay số")]
//        public IActionResult AddNewProgram(LotteryProgramViewModel model)
//        {
//            return View(model);
//        }
//        [HttpPost]
//        public IActionResult AddNewProgram()
//        {
//            return RedirectToAction("Program");
//        }
//        [Breadcrumb("Sửa thông tin chương trình quay số")]
//        public IActionResult EditProgram(int ID)
//        {
//            var model = new LotteryProgramsViewModel().LotteryPrograms.Where(x => x.ID == ID).FirstOrDefault();
//            return View(model);
//        }
//        [HttpPost]
//        public IActionResult EditProgram()
//        {
//            return RedirectToAction("Program");
//        }

//        [HttpPost]
//        public IActionResult DeleteProgram()
//        {
//            return RedirectToAction("Program");
//        }
//        [HttpPost]
//        public IActionResult UploadiconProgram(IFormFile imgfile)
//        {
//            var saveimg = Path.Combine(_webhost.WebRootPath, "Dat'sImages", "Lottery", "Program", imgfile.FileName);
//            string imgext = Path.GetExtension(imgfile.FileName);
//            if (imgext == ".jpg" || imgext == ".png")
//            {
//                using (var uploadimg = new FileStream(saveimg, FileMode.Create))
//                {
//                    imgfile.CopyTo(uploadimg);
//                    ViewData["Message"] = "Ảnh" + imgfile.FileName + "đã được lưu";
//                }
//            }

//            return RedirectToAction("Program");
//        }
//        [HttpPost]
//        public IActionResult UploadimageProgram(IFormFile imgfile)
//        {
//            var saveimg = Path.Combine(_webhost.WebRootPath, "Dat'sImages", "Lottery", "Program", imgfile.FileName);
//            string imgext = Path.GetExtension(imgfile.FileName);
//            if (imgext == ".jpg" || imgext == ".png")
//            {
//                using (var uploadimg = new FileStream(saveimg, FileMode.Create))
//                {
//                    imgfile.CopyTo(uploadimg);
//                    ViewData["Message"] = "Ảnh" + imgfile.FileName + "đã được lưu";
//                }
//            }

//            return RedirectToAction("Program");
//        }
//        #endregion

//        #region Gift 
//        public IActionResult Gift()
//        {
//            ViewBag.PageTitle = "QUÀ QUAY SỐ";
//            LotteryGiftsViewModel model = new LotteryGiftsViewModel();
//            return View(model);
//        }
//        public IActionResult AddNewGift(LotteryGiftViewModel model)
//        {
//            ViewBag.PageTitle = "THÊM QUÀ";
//            return View(model);
//        }
//        [HttpPost]
//        public IActionResult AddNewGift()
//        {
//            return RedirectToAction("Gift");
//        }

//        public IActionResult EditGift(int ID)
//        {
//            ViewBag.PageTitle = "SỬA THÔNG TIN QUÀ";
//            var model = new LotteryGiftsViewModel().LotteryGifts.Where(x => x.ID == ID).FirstOrDefault();
//            return View(model);
//        }
//        [HttpPost]
//        public IActionResult EditGift()
//        {
//            return RedirectToAction("Gift");
//        }

//        [HttpPost]
//        public IActionResult DeleteGift()
//        {
//            return RedirectToAction("Gift");
//        }
//        [HttpPost]
//        public IActionResult UploadiconGift(IFormFile imgfile)
//        {
//            var saveimg = Path.Combine(_webhost.WebRootPath, "Dat'sImages", "Lottery", "Gift", imgfile.FileName);
//            string imgext = Path.GetExtension(imgfile.FileName);
//            if (imgext == ".jpg" || imgext == ".png")
//            {
//                using (var uploadimg = new FileStream(saveimg, FileMode.Create))
//                {
//                    imgfile.CopyTo(uploadimg);
//                    ViewData["Message"] = "Ảnh" + imgfile.FileName + "đã được lưu";
//                }
//            }

//            return RedirectToAction("Gift");
//        }
//        #endregion

//        #region PromotionGift
//        public IActionResult PromotionGift()
//        {
//            ViewBag.PageTitle = "QUÀ TIÊU DÙNG";
//            PromotionGiftsViewModel model = new PromotionGiftsViewModel();
//            return View(model);
//        }
//        public IActionResult AddNewPromotionGift(PromotionGiftViewModel model)
//        {
//            ViewBag.PageTitle = "THÊM QUÀ TIÊU DÙNG";
//            return View(model);
//        }
//        [HttpPost]
//        public IActionResult AddNewPromotionGift()
//        {
//            return RedirectToAction("PromotionGift");
//        }

//        public IActionResult EditPromotionGift(int ID)
//        {
//            ViewBag.PageTitle = "SỬA THÔNG TIN QUÀ TIÊU DÙNG";
//            var model = new PromotionGiftsViewModel().PromotionGifts.Where(x => x.ID == ID).FirstOrDefault();
//            return View(model);
//        }
//        [HttpPost]
//        public IActionResult EditPromotionGift()
//        {
//            return RedirectToAction("PromotionGift");
//        }

//        [HttpPost]
//        public IActionResult DeletePromotionGift()
//        {
//            return RedirectToAction("PromotionGift");
//        }
//        [HttpPost]
//        public IActionResult UploadiconPromotionGift(IFormFile imgfile)
//        {
//            var saveimg = Path.Combine(_webhost.WebRootPath, "Dat'sImages", "Lottery", "PromotionGift", imgfile.FileName);
//            string imgext = Path.GetExtension(imgfile.FileName);
//            if (imgext == ".jpg" || imgext == ".png")
//            {
//                using (var uploadimg = new FileStream(saveimg, FileMode.Create))
//                {
//                    imgfile.CopyTo(uploadimg);
//                    ViewData["Message"] = "Ảnh" + imgfile.FileName + "đã được lưu";
//                }
//            }

//            return RedirectToAction("PromotionGift");
//        }
//        #endregion
//    }
//}
