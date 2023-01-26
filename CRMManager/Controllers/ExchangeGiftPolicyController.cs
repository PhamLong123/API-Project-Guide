//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using SmartBreadcrumbs.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class ExchangeGiftPolicyController : Controller
//    {
//        public ExchangeGiftPolicyController()
//        {

//        }

//        [Breadcrumb("Chính sách đổi quà")]
//        public IActionResult Index()
//        {
//            ExchangeGiftPoliciesViewModel model = new ExchangeGiftPoliciesViewModel();
//            return View(model);
//        }

//        [Breadcrumb("Sửa thông tin chính sách đổi quà")]
//        public IActionResult Edit(int ID)
//        {
//            ExchangeGiftPolicyViewModel model = new ExchangeGiftPoliciesViewModel().ExchangeGiftPolicies.Where(x => x.ID == ID).FirstOrDefault();
//            return View(model);
//        }

//        [Breadcrumb("Thêm chính sách đổi quà")]
//        public IActionResult AddNew()
//        {
//            ExchangeGiftPolicyViewModel model = new ExchangeGiftPolicyViewModel();
//            return View(model);
//        }
//    }
//}
