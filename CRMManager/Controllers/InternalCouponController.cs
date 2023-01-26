//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using SmartBreadcrumbs.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class InternalCouponController : Controller
//    {
//        public InternalCouponController()
//        {

//        }

//        [Breadcrumb("Quản lý mã khuyến mãi nội bộ")]
//        public IActionResult Index()
//        {
//            InternalCouponsViewModel model = new InternalCouponsViewModel();
//            return View(model);
//        }

//        [Breadcrumb("Sửa thông tin mã khuyến mãi nội bộ")]
//        public IActionResult Edit(int ID)
//        {
//            InternalCouponViewModel discountViewModel = new InternalCouponsViewModel().InternalDiscounts.Where(x => x.ID == ID).FirstOrDefault();
//            return View(discountViewModel);
//        }

//        [Breadcrumb("Thêm mã khuyến mãi nội bộ")]
//        public IActionResult AddNew()
//        {
//            InternalCouponViewModel discountViewModel = new InternalCouponViewModel();
//            return View(discountViewModel);
//        }
//    }
//}
