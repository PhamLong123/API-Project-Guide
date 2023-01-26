//using AutoMapper;
//using CRMLogic.Base.Common;
//using CRMLogic.Base.CRMManager;
//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using SmartBreadcrumbs.Attributes;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class JobScheduleController : Controller
//    {
//        BaseTaskScheduleOperation _baseTaskScheduleOperation;
//        BaseCRMManagerApp _crmManagerApp;
//        JobSchedulesViewModel _viewModel;
//        IMapper _mapper;
//        public JobScheduleController(BaseTaskScheduleOperation baseTaskScheduleOperation,
//            IMapper mapper, BaseCRMManagerApp crmManagerApp,
//            JobSchedulesViewModel data)
//        {
//            _baseTaskScheduleOperation = baseTaskScheduleOperation;
//            _mapper = mapper;
//            _crmManagerApp = crmManagerApp;
//            _viewModel = data;
//        }

//        [Breadcrumb("Lập lịch tự động")]
//        public async Task<IActionResult> Index()
//        {
//            await _viewModel.LoadData();
//            _viewModel.PageTitle = "Lập lịch tự động";
//            return View(_viewModel);
//        }

//        [Breadcrumb("Chi tiết")]
//        public IActionResult Details(int id)
//        {
//            JobScheduleViewModel _detailViewModel = new JobScheduleViewModel();
//            _detailViewModel.PageTitle = "Chi tiết lịch tự động";
//            return View(_detailViewModel);
//        }

//        [Breadcrumb("Chỉnh sửa")]
//        public IActionResult Edit(int id)
//        {
//            JobScheduleViewModel _detailViewModel = new JobScheduleViewModel();
//            _detailViewModel.PageTitle = "Chi tiết lịch tự động";
//            return View(_detailViewModel);
//        }

//        public IActionResult Create()
//        {
//            JobScheduleViewModel _detailViewModel = new JobScheduleViewModel();
//            _detailViewModel.PageTitle = "Lập lịch tự động";
//            return View(_detailViewModel);
//        }
//    }
//}
