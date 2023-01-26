//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using SmartBreadcrumbs.Attributes;

//namespace CRMManager.Controllers
//{
//    public class DataMessageController : Controller
//    {
//        public DataMessageController()
//        {

//        }

//        [Breadcrumb("Dịch dữ liệu")]
//        public IActionResult Index()
//        {
//            DataMessagesViewModel _viewModel = new DataMessagesViewModel();
//            _viewModel = _viewModel.GetDefaultData();
//            _viewModel.PageTitle = "Dịch dữ liệu";
//            return View(_viewModel);
//        }

//        [Breadcrumb("Thêm mới")]
//        public IActionResult Create()
//        {
//            DataMessageViewModel _viewModel = new DataMessageViewModel();
//            _viewModel.PageTitle = "Dịch dữ liệu";
//            return View(_viewModel);
//        }

//        [HttpPost]
//        public IActionResult Create(DataMessageViewModel model)
//        {
//            DataMessageViewModel _viewModel = new DataMessageViewModel();
//            _viewModel.PageTitle = "Dịch dữ liệu";
//            return RedirectToAction("Index");
//        }

//        [Breadcrumb("Chi tiết")]
//        public IActionResult Details(int ID)
//        {
//            DataMessageViewModel _detailViewModel = new DataMessageViewModel();
//            _detailViewModel.PageTitle = "Dịch dữ liệu";
//            return View(_detailViewModel);
//        }

//        [Breadcrumb("Chỉnh sửa")]
//        public IActionResult Edit(int ID)
//        {
//            DataMessageViewModel _detailViewModel = new DataMessageViewModel();
//            _detailViewModel.PageTitle = "Dịch dữ liệu";
//            return View(_detailViewModel);
//        }

//        [HttpPost]
//        public IActionResult Edit(DataMessageViewModel model)
//        {
//            DataMessageViewModel _detailViewModel = new DataMessageViewModel();
//            _detailViewModel.PageTitle = "Dịch dữ liệu";
//            return RedirectToAction("Index");
//        }
//    }
//}
