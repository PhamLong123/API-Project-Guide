//using AutoMapper;
//using CRMDAL.DTO;
//using CRMLogic.Base.BaseBO;
//using CRMLogic.Base.CRMManager;
//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class AppListController : Controller
//    {
//        BaseAppListOperation _baseAppListOperation;
//        IMapper _mapper;

//        string pageTitle = "Danh sách ứng dụng";
//        public AppListController(BaseAppListOperation baseAppListOperation,

//            IMapper mapper)
//        {
//            _baseAppListOperation = baseAppListOperation;
//            _mapper = mapper;
//        }

//        public async Task<IActionResult> Index()
//        {
//            AppListModel data = new AppListModel();

//            //List<BaseAppList> baseAppList = await _baseAppListOperation.GetAppList();
//            //List<AppListVM> appListViewModel = _mapper.Map<List<AppListVM>>(baseAppList);

//            data.PageTitle = pageTitle;

//            //TempData["AppList"] = data.AppLists;
//            return View(data);
//        }

//        public IActionResult Detail(int ID)
//        {
//            AppListModel data = new AppListModel();
//            data = data.GetData();
//            data.PageTitle = pageTitle;
//            return View("Index", data);
//        }

//        public IActionResult Create(AppListModel model)
//        {
//            AppListModel data = new AppListModel();
//            data = data.GetDefaultData();
//            return View("Index", data);
//        }

//        public IActionResult Edit()
//        {
//            AppListModel data = new AppListModel();
//            data = data.GetDefaultData();
//            return View("Index", data);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Save(AppListModel model)
//        {
//            //ViewBag.PageTitle = "DANH SÁCH ỨNG DỤNG";
//            //AppListModel data = new AppListModel();

//            //AppListUI appList = _mapper.Map<AppListUI>(model);

//            //BaseAppList baseAppList = await _baseAppListOperation.CreateAppList(appList);
//            //AppListVM appListViewModel = _mapper.Map<AppListVM>(baseAppList);
//            //List<BaseAppList> baseDataList = await _baseAppListOperation.GetAppList();
//            //List<AppListVM> dataList = _mapper.Map<List<AppListVM>>(baseDataList);

//            //data.AppLists = dataList;
//            //data.AppLists = appListViewModel;
//            return View("Index", data);
//        }
//    }
//}
