//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using SmartBreadcrumbs.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class ConsumerPolicyController : Controller
//    {
//        public ConsumerPolicyController()
//        {

//        }

//        [Breadcrumb("Chính sách tiêu dùng")]
//        public IActionResult Index()
//        {
//            ConsumerPoliciesViewModel model = new ConsumerPoliciesViewModel();
//            return View(model);
//        }

//        [Breadcrumb("Sửa thông tin chính sách tiêu dùng")]
//        public IActionResult Edit(int ID)
//        {
//            ConsumerPolicyViewModel model = new ConsumerPoliciesViewModel().ConsumerPolicies.Where(x => x.ID == ID).FirstOrDefault();
//            return View(model);
//        }

//        [Breadcrumb("Thêm chính sách tiêu dùng")]
//        public IActionResult AddNew()
//        {
//            ConsumerPolicyViewModel model = new ConsumerPolicyViewModel();
//            return View(model);
//        }
//    }
//}
