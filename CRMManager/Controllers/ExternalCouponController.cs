//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using SmartBreadcrumbs.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class ExternalCouponController : Controller
//    {
//        public ExternalCouponController()
//        {

//        }

//        [Breadcrumb("Quản lý mã khuyến mãi bên ngoài")]
//        public IActionResult Index()
//        {
//            ExternalCouponsViewModel model = new ExternalCouponsViewModel();
//            return View(model);
//        }

//        [Breadcrumb("Sửa thông tin mã khuyến mãi bên ngoài")]
//        public IActionResult Edit(int ID)
//        {
//            ExternalCouponViewModel discountViewModel = new ExternalCouponsViewModel().OutsideDiscounts.Where(x => x.ID == ID).FirstOrDefault();
//            return View(discountViewModel);
//        }

//        [Breadcrumb("Thêm mã khuyến mãi bên ngoài")]
//        public IActionResult AddNew()
//        {
//            ExternalCouponViewModel discountViewModel = new ExternalCouponViewModel();
//            return View(discountViewModel);
//        }

//        [Breadcrumb("Nhập mã giảm giá")]
//        public IActionResult ImportFromExcel()
//        {
//            return View();
//        }
//    }
//}
