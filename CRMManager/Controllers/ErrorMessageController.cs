//using AutoMapper;
//using CRMLogic.Base.BaseBO;
//using CRMLogic.Base.CRMManager;
//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class ErrorMessageController : Controller
//    {
//        BaseErrorMessageOperation _baseErrorMessageOperation;

//        IMapper _mapper;
//        public ErrorMessageController(BaseErrorMessageOperation baseErrorMessageOperation,

//            IMapper mapper)
//        {
//            _baseErrorMessageOperation = baseErrorMessageOperation;

//            _mapper = mapper;
//        }
//        public async Task<IActionResult> Index()
//        {
//            ErrorMessagesViewModel data = new ErrorMessagesViewModel();

//            List<BaseErrorMessage> baseErrorMessages = await _baseErrorMessageOperation.GetErrorMessages();
//            data.PageTitle = "Thông báo lỗi";
//            data.ErrorMessageList = baseErrorMessages;

//            return View(data);
//        }

//        public IActionResult Create()
//        {
//            ErrorMessageViewModel _detailViewModel = new ErrorMessageViewModel();
//            _detailViewModel.PageTitle = "Chi tiết thông báo";
//            return View(_detailViewModel);
//        }

//        [HttpPost]
//        public IActionResult Create(ErrorMessageViewModel model)
//        {
//            ErrorMessageViewModel _detailViewModel = new ErrorMessageViewModel();
//            _detailViewModel.PageTitle = "Chi tiết thông báo";
//            return View(_detailViewModel);
//        }

//        public IActionResult Details(int id)
//        {
//            ErrorMessageViewModel _detailViewModel = new ErrorMessageViewModel();
//            _detailViewModel.PageTitle = "Chi tiết thông báo";
//            return View(_detailViewModel);
//        }

//        public IActionResult Edit(int id)
//        {
//            ErrorMessageViewModel _detailViewModel = new ErrorMessageViewModel();
//            _detailViewModel.PageTitle = "Chỉnh sửa thông báo";
//            return View(_detailViewModel);
//        }

//        [HttpPost]
//        public IActionResult Edit(ErrorMessageViewModel model)
//        {
//            ErrorMessageViewModel _detailViewModel = new ErrorMessageViewModel();
//            _detailViewModel.PageTitle = "Chỉnh sửa thông báo";
//            return RedirectToAction("Index");
//        }
//    }
//}
