//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace CRMManager.Controllers
//{
//    public class NonMemberRegisterController : Controller
//    {
//        public IActionResult Index()
//        {
//            NonMemberRegisterViewModel data = new NonMemberRegisterViewModel();
//            data.PageTitle = "Đăng ký khách hàng nhóm ngoài thành viên";
//            return View(data);
//        }

//        [HttpPost]
//        public IActionResult Register(NonMemberRegisterViewModel model)
//        {
//            return RedirectToAction("Index");
//        }
//    }
//}
