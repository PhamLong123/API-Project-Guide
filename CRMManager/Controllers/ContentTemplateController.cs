//using AutoMapper;
//using CRMLogic.Base.BaseBO;
//using CRMLogic.Base.Common;
//using CRMLogic.Base.CRMManager;
//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class ContentTemplateController : Controller
//    {
//        BaseTemplateOperation _baseTemplateOperation;
//        BaseTemplateGroupOperation _baseTemplateGroupOperation;
//        BaseCRMManagerApp _crmManagerApp;
//        ContentTemplatesViewModel _viewModel;
//        IMapper _mapper;
//        public ContentTemplateController(BaseTemplateOperation baseTemplateOperation,
//            BaseTemplateGroupOperation baseTemplateGroupOperation,
//            IMapper mapper)
//        {
//            _baseTemplateOperation = baseTemplateOperation;
//            _baseTemplateGroupOperation = baseTemplateGroupOperation;
//            _mapper = mapper;
//            _viewModel = new ContentTemplatesViewModel(_baseTemplateOperation, _baseTemplateGroupOperation, _mapper, _crmManagerApp);
//        }
//        public async Task<IActionResult> Index(int selectedGroupID = -1)
//        {
//            _viewModel.SelectedGroupID = selectedGroupID;
//            await _viewModel.LoadData();
//            _viewModel.PageTitle = "Quản lý nội dung mẫu thư";
//            return View(_viewModel);
//        }

//        public async Task<IActionResult> Details(int templateID)
//        {
//            BaseTemplate baseTemplate = await _baseTemplateOperation.GetTemplateByID(templateID);
//            ContentTemplateViewModel _detailViewModel = _mapper.Map<ContentTemplateViewModel>(baseTemplate);

//            _detailViewModel.PageTitle = "Chi tiết nội dung mẫu thư";

//            return View(_detailViewModel);
//        }

//        public async Task<IActionResult> Edit(int templateID)
//        {
//            BaseTemplate baseTemplate = await _baseTemplateOperation.GetTemplateByID(templateID);
//            ContentTemplateViewModel _detailViewModel = _mapper.Map<ContentTemplateViewModel>(baseTemplate);

//            _detailViewModel.PageTitle = "Chỉnh sửa nội dung mẫu thư";

//            return View(_detailViewModel);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Edit(ContentTemplateViewModel model)
//        {
//            return RedirectToAction("Index");
//        }

//        public async Task<IActionResult> Create()
//        {
//            ContentTemplateViewModel _detailViewModel = new ContentTemplateViewModel();
//            _detailViewModel.PageTitle = "Thêm nội dung mẫu thư";
//            return View(_detailViewModel);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(ContentTemplateViewModel model)
//        {
//            return RedirectToAction("Index");
//        }
//    }
//}
