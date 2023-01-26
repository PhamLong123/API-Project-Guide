//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using SmartBreadcrumbs.Attributes;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class AppSettingController : Controller
//    {
//        SettingViewModel _viewModel;
//        public AppSettingController(SettingViewModel data)
//        {
//            _viewModel = data;
//        }

//        [Breadcrumb("Quản trị cấu hình")]
//        public async Task<IActionResult> Index()
//        {
//            _viewModel.PageTitle = "Quản trị cấu hình";
//            await _viewModel.LoadData();
//            return View(_viewModel);
//        }

//        [HttpPost]
//        public IActionResult Index(SettingViewModel model)
//        {
//            _viewModel.PageTitle = "Quản trị cấu hình";
//            return RedirectToAction("Index");
//        }
//    }
//}
