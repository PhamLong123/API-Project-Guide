//using AutoMapper;
//using CRMLogic.Base.BusinessObject;
//using CRMLogic.Base.CRMManager;
//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using SmartBreadcrumbs.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class PromotionCodeController : Controller
//    {
//        BaseENoteOperation _baseENoteOperation;
//        Utilities _utilities;
//        IMapper _mapper;
//        ErrorMessage errorMessage = new ErrorMessage();
//        bool isSuccess = false;
//        public PromotionCodeController(BaseENoteOperation baseENoteOperation, IMapper mapper)
//        {
//            _baseENoteOperation = baseENoteOperation;
//            _mapper = mapper;
//            _utilities = new Utilities();
//        }
//        [Breadcrumb("Quản lý mã giảm giá")]
//        public IActionResult Index()
//        {
//            PromotionCodesViewModel model = new PromotionCodesViewModel();
//            return View(model);
//        }
//        [Breadcrumb("Chỉnh sửa thông tin mã giảm giá")]
//        public IActionResult Edit(int ID)
//        {
//            PromotionCodeViewModel model = new PromotionCodesViewModel().PromotionCodes.Where(x => x.ID == ID).FirstOrDefault();
//            return View(model);
//        }
//        [Breadcrumb("Thêm mã giảm giá")]
//        public IActionResult AddNew()
//        {
//            PromotionCodeViewModel model = new PromotionCodeViewModel();
//            return View(model);
//        }

//        [Breadcrumb("Định nghĩa mã giảm giá")]
//        public IActionResult Detail(ENoteViewModel model)
//        {
//            //if(model.EnotesDetailList == null)
//            //{
//            //    model.EnotesDetailList = new List<ENoteDetailViewModel>();
//            //}    
//            return View(model);
//        }

//        [HttpPost]
//        public async Task<IActionResult> ImportFromExcel(ENoteViewModel eNote)
//        {
//            if(eNote.FileUpload == null)
//            {
//                TempData["Icon"] = errorMessage.ErrorIcon;
//                TempData["Error"] = errorMessage.ImportFile;
//                return View("Detail", eNote);
//            }
//            else if(eNote.FileUpload.FormFile.Length > 0)
//            {
//                eNote = await _utilities.ChangeDataExcelToList(eNote);
//            }
//            if(eNote.EnotesDetailList.Count() == 0)
//            {
//                TempData["Icon"] = errorMessage.ErrorIcon;
//                TempData["Error"] = errorMessage.ExcelFileEmpty;
//                return View("Detail", eNote);
//            }
//            else
//            {
//                TempData["Icon"] = errorMessage.SuccessIcon;
//                TempData["Error"] = errorMessage.GetDataFromExcelSuccess;
//            }
//            return View("Detail", eNote);
//        }

//        [HttpPost]
//        public async Task<IActionResult> AddNew(ENoteViewModel eNote)
//        {
//            eNote.ID = Guid.NewGuid();
//            foreach(var item in eNote.EnotesDetailList)
//            {
//                item.ID = Guid.NewGuid();
//                item.DocID = eNote.ID;
//            };
//            BaseENotes baseENotes = _mapper.Map<BaseENotes>(eNote);
//            baseENotes = await _baseENoteOperation.CreateENote(baseENotes);
//            isSuccess = baseENotes != null;
//            if (!isSuccess)
//            {
//                TempData["Icon"] = errorMessage.ErrorIcon;
//                TempData["Error"] = errorMessage.InsertFail;
//                return View("Detail", eNote);
//            }
//            else
//            {
//                TempData["Icon"] = errorMessage.SuccessIcon;
//                TempData["Error"] = errorMessage.InsertDone;
//            }
//            return View("Detail", eNote);
//        }
//        [Breadcrumb("Gửi mã khuyến mãi")]
//        public IActionResult SendCode()
//        {
//            return View();
//        }
//    }
//}
