//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace CRMManager.Controllers
//{
//    public class AppObjectController : Controller
//    {
//        public AppObjectController()
//        {

//        }
//        public IActionResult Index()
//        {
//            AppObjectViewModel data = new AppObjectViewModel();
//            data.PageTitle = "Mô đun ứng dụng";
//            return View(data);
//        }

//        public IActionResult Detail()
//        {
//            AppObjectViewModel data = new AppObjectViewModel();
//            data.GetDetailData();
//            data.PageTitle = "Mô đun ứng dụng";
//            return View("Index", data);
//        }
//    }
//}
