//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class MemRangeClassController : Controller
//    {
//        public MemRangeClassController()
//        {

//        }
//        public IActionResult Index(MemRangeClassViewModel memRangeClassViewModel)
//        {
//            ViewBag.PageTitle = "PHÂN NHÓM HẠNG THẺ";
//            List<MemRangeClassViewModel> memRangeClassViewModels = new List<MemRangeClassViewModel>();
//            ViewBag.MemRanges = memRangeClassViewModels;
//            return View(memRangeClassViewModel);
//        }

//        public IActionResult Test()
//        {
//            //MemRangeClassViewModel memRangeClassViewModel = new MemRangeClassViewModel();
//            //List<MemRangeClassViewModel> memRangeClassViewModels = new List<MemRangeClassViewModel>();
//            //ViewBag.MemRanges = memRangeClassViewModels;
//            return View();
//        }

//        public IActionResult Edit(int ID)
//        {
//            MemRangeClassViewModel memRangeClassViewModel = new MemRangeClassViewModel();
//            return RedirectToAction("Index", memRangeClassViewModel);
//        }

//        [HttpPost]
//        public IActionResult Edit(MemRangeClassViewModel memRangeClassViewModel)
//        {   
//            return RedirectToAction("Index", memRangeClassViewModel);
//        }

//        [HttpPost]
//        public IActionResult AddNew(MemRangeClassViewModel memRangeClassViewModel)
//        {
//            return RedirectToAction("Index", memRangeClassViewModel);
//        }
//    }
//}
