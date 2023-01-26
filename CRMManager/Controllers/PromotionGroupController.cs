//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using SmartBreadcrumbs.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class PromotionGroupController : Controller
//    {
//        public PromotionGroupController()
//        {

//        }
//        [Breadcrumb("Quản lý chương trình khuyến mãi")]
//        public IActionResult Index()
//        {
//            PromotionGroupsViewModel model = new PromotionGroupsViewModel();
//            return View(model);
//        }
//        [Breadcrumb("Sửa chương trình khuyến mãi")]
//        public IActionResult Edit(int ID)
//        {
//            PromotionGroupViewModel model = new PromotionGroupsViewModel().PromotionGroups.Where(x => x.ID == ID).FirstOrDefault();
//            PromotionGroupDetailsViewModel promotionGroupDetails = new PromotionGroupDetailsViewModel();
//            ViewBag.PGD = promotionGroupDetails.PromotionGroupDetails;
//            return View(model);
//        }
//        [Breadcrumb("Thêm chương trình khuyến mãi")]
//        public IActionResult AddNew()
//        {
//            PromotionGroupViewModel model = new PromotionGroupViewModel();
//            return View(model);
//        }
//    }
//}
