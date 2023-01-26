//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using SmartBreadcrumbs.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class RankUpPolicyController : Controller
//    {
//        public RankUpPolicyController()
//        {

//        }

//        [Breadcrumb("Chính sách lên hạng")]
//        public IActionResult Index()
//        {
//            RankUpPoliciesViewModel model = new RankUpPoliciesViewModel();
//            return View(model);
//        }

//        [Breadcrumb("Sửa thông tin chính sách lên hạng")]
//        public IActionResult Edit(int ID)
//        {
//            RankUpPolicyViewModel model = new RankUpPoliciesViewModel().RankUpPolicies.Where(x => x.ID == ID).FirstOrDefault();
//            return View(model);
//        }

//        [Breadcrumb("Thêm chính sách lên hạng")]
//        public IActionResult AddNew()
//        {
//            RankUpPolicyViewModel model = new RankUpPolicyViewModel();
//            return View(model);
//        }
//    }
//}
