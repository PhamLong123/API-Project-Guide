//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace CRMManager.Controllers
//{
//    public class UIMessageController : Controller
//    {
//        public UIMessageController()
//        {

//        }
//        public IActionResult Index()
//        {
//            UIMessagesViewModel data = new UIMessagesViewModel();
//            data = data.GetDefaultData();
//            data.PageTitle = "Dịch giao diện";
//            return View(data);
//        }

//        public IActionResult Details(int ID)
//        {
//            UIMessageViewModel _detailViewModel = new UIMessageViewModel();
//            _detailViewModel.PageTitle = "Dịch giao diện";
//            return View(_detailViewModel);
//        }

//        public IActionResult Create()
//        {
//            UIMessageViewModel _detailViewModel = new UIMessageViewModel();
//            _detailViewModel.PageTitle = "Dịch giao diện";
//            return View(_detailViewModel);
//        }

//        [HttpPost]
//        public IActionResult Create(UIMessageViewModel model)
//        {
//            UIMessageViewModel _detailViewModel = new UIMessageViewModel();
//            return RedirectToAction("Index");
//        }

//        public IActionResult Edit(int ID)
//        {
//            UIMessageViewModel _detailViewModel = new UIMessageViewModel();
//            _detailViewModel.PageTitle = "Dịch giao diện";
//            return View(_detailViewModel);
//        }

//        [HttpPost]
//        public IActionResult Edit(UIMessageViewModel model)
//        {
//            UIMessageViewModel _detailViewModel = new UIMessageViewModel();
//            return RedirectToAction("Index");
//        }
//    }
//}
