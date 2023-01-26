//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using SmartBreadcrumbs.Attributes;

//namespace CRMManager.Controllers
//{
//    public class AppContentController : Controller
//    {
//        public AppContentController()
//        {

//        }

//        [Breadcrumb("Cấu hình nội dung ứng dụng")]
//        public IActionResult Index()
//        {
//            AppContentViewModel data = new AppContentViewModel();
//            data = data.GetDefaultData();
//            data.PageTitle = "Cấu hình nội dung ứng dụng";
//            return View(data);
//        }
//    }
//}
