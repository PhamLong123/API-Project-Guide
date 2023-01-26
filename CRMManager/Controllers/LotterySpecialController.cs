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
//    public class LotterySpecialController : Controller
//    {
//        private readonly IWebHostEnvironment _webhost;
//        public LotterySpecialController(IWebHostEnvironment webHost)
//        {
//            _webhost = webHost;
//        }
//        public IActionResult Index()
//        {
//            return View();
//        }
//        #region Program
//        public IActionResult Program()
//        {
//            ViewBag.PageTitle = "CHƯƠNG TRÌNH QUAY SÔ ĐẶC BIỆT";
//            LotterySpecialProgramsViewModel model = new LotterySpecialProgramsViewModel();
//            return View(model);
//        }
//        public IActionResult AddNewProgram(LotterySpecialProgramViewModel model)
//        {
//            ViewBag.PageTitle = "THÊM CHƯƠNG TRÌNH";
//            return View(model);
//        }
//        [HttpPost]
//        public IActionResult AddNewProgram()
//        {
//            return RedirectToAction("Program");
//        }

//        public IActionResult EditProgram(int ID)
//        {
//            ViewBag.PageTitle = "SỬA THÔNG TIN CHƯƠNG TRÌNH";
//            var model = new LotterySpecialProgramsViewModel().LotterySpecialPrograms.Where(x => x.ID == ID).FirstOrDefault();
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
//        #endregion

//        #region Gift
//        public IActionResult Gift()
//        {
//            ViewBag.PageTitle = "QUÀ TRÚNG THƯỞNG";
//            LotterySpecialGiftsViewModel model = new LotterySpecialGiftsViewModel();
//            return View(model);
//        }
//        public IActionResult AddNewGift(LotterySpecialGiftViewModel model)
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
//            var model = new LotterySpecialGiftsViewModel().LotterySpecialGifts.Where(x => x.ID == ID).FirstOrDefault();
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
//            var saveimg = Path.Combine(_webhost.WebRootPath, "Dat'sImages", "LotterySpecial", "Gift", imgfile.FileName);
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

//        #region Result
//        public IActionResult Result()
//        {
//            ViewBag.PageTitle = "KẾT QUẢ QUAY SỐ";
//            LotterySpecialResultsViewModel model = new LotterySpecialResultsViewModel();
//            return View(model);
//        }
//        public IActionResult Detail(int ID)
//        {
//            ViewBag.PageTitle = "CHI TIẾT";
//            var model = new LotterySpecialResultsViewModel().LotterySpecialResults.Where(x => x.ID == ID).FirstOrDefault();
//            return View(model);
//        }
//        #endregion
//    }
//}
