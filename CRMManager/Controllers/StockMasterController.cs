//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using SmartBreadcrumbs.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class StockMasterController : Controller
//    {
//        public StockMasterController()
//        {

//        }
//        [Breadcrumb("Kho quà")]
//        public IActionResult Index()
//        {
//            StockMastersViewModel model = new StockMastersViewModel();
//            return View(model);
//        }

//        [Breadcrumb("Sửa thông tin  quà")]
//        public IActionResult Edit(int ID)
//        {
//            StockMasterViewModel model = new StockMastersViewModel().StockMasters.Where(x => x.ID == ID).FirstOrDefault();
//            return View(model);
//        }

//        [Breadcrumb("Thêm quà")]
//        public IActionResult AddNew()
//        {
//            StockMasterViewModel model = new StockMasterViewModel();
//            return View(model);
//        }
//    }
//}
