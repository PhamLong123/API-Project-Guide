using AutoMapper;
using CRMManager.Helper.Logic.Authentication;
using CRMManager.Models;
using CRMSharedLib.DataType.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSAServices.Authentication;
using SmartBreadcrumbs.Attributes;
using System.Threading.Tasks;

namespace CRMManager.Controllers
{
    /// <summary>
    /// view Name=AppRole\Index
    /// view Model=AppRoleModel
    /// 
    /// </summary>
    public class AppRoleController : Controller
    {


        AppRoleManagement _appRoleManagement;
        IMapper _mapper;
        public AppRoleController(AppRoleManagement appRoleManagement, IMapper mapper)
        {
            _appRoleManagement = appRoleManagement;
            _mapper = mapper;
        }

        [Breadcrumb("Quản trị vai trò người dùng")]
        public async Task<IActionResult> Index(int selectedAppID = -1)
        {
            AppRolesViewModel appRolesViewModel = new AppRolesViewModel();
            appRolesViewModel.SelectedAppID = selectedAppID;
            //await _viewModel.LoadData();
            appRolesViewModel.PageTitle = "Quản trị vai trò người dùng";


            InitAppRoleData initAppRoleData = await _appRoleManagement.GetInitData();
            appRolesViewModel.AppRoles = initAppRoleData.BaseAppRoles;
            ViewBag.AppList = new SelectList(initAppRoleData.BaseAppLists, "ID", "Name");
            return View(appRolesViewModel);
        }

        //public async Task<JsonResult> GetAppRoleList(int selectedAppID = -1)
        //{
        //    _viewModel.SelectedAppID = selectedAppID;
        //    await _viewModel.LoadData();
        //    return Json(_viewModel.AppRoleList);
        //}

        [Breadcrumb("Chỉnh sửa")]
        public async Task<IActionResult> Edit(int roleID, int selectedAppID)
        {
            //int appID = 1;
            BaseAppRole baseAppRole = await _appRoleManagement.GetRoleDetailsByID(roleID, selectedAppID);
            if (baseAppRole != null)
            {
                AppRoleViewModel appRoleViewModel = _mapper.Map<AppRoleViewModel>(baseAppRole);
                appRoleViewModel.PageTitle = "Chỉnh sửa";
                appRoleViewModel.SelectedAppName = "";
                appRoleViewModel.DataState = EDataState.Edit;
                return View(appRoleViewModel);
            }
            else
            {
                return View("Index");
            }

        }

        //[Breadcrumb("Chỉnh sửa")]
        //public async Task<IActionResult> Edit(int roleID)
        //{
        //    // BaseAppRole baseAppRole = await _baseAppRoleOperation.GetRoleByID(roleID);
        //    //AppRoleViewModel _detailViewModel = _mapper.Map<AppRoleViewModel>(baseAppRole);
        //    //_detailViewModel.SetBusinessOperation(_baseAppRoleOperation);
        //    //await _detailViewModel.LoadData();

        //    _detailViewModel.PageTitle = "Chi tiết vai trò người dùng";
        //    var selectedItem = _viewModel.SelectListApp.Find(p => p.Value == baseAppRole.AppID.ToString());
        //    _detailViewModel.SelectedAppName = selectedItem.Text;
        //    return View(_detailViewModel);
        //}

        [Breadcrumb("Thêm mới")]
        public IActionResult Create(int selectedAppID)
        {
            AppRoleViewModel _detailViewModel = new AppRoleViewModel();
            //var selectedItem = model.SelectListApp.Find(p => p.Value == model.SelectedAppID.ToString());
            //_detailViewModel.SelectedAppName = selectedItem.Text;
            _detailViewModel.PageTitle = "Thêm vai trò người dùng";
            _detailViewModel.DataState = EDataState.Create;
            return View("Create", _detailViewModel);
        }

        [Breadcrumb("Thêm mới")]
        [HttpPost]
        public IActionResult Create(AppRolesViewModel model)
        {
            AppRoleViewModel _detailViewModel = new AppRoleViewModel();
            var selectedItem = model.SelectListApp.Find(p => p.Value == model.SelectedAppID.ToString());
            _detailViewModel.SelectedAppName = selectedItem.Text;
            _detailViewModel.PageTitle = "Thêm vai trò người dùng";
            return View("Create", _detailViewModel);
        }
        /// <summary>
        /// Save Data tu View Edit/Create?
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save(AppRoleViewModel model)
        {
            ////hardCode
            //model.DataState = EDataState.Edit;
            if (string.IsNullOrEmpty(model.CreatedBy))
            {
                model.CreatedBy = "msa";
                model.CreatedOn = System.DateTime.Now;

            }
            model.ModifiedBy = "msa";
            model.ModifiedOn = System.DateTime.Now;
            bool isSaved = await _appRoleManagement.Save(model);
            if (!isSaved)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }



        //public async Task<IActionResult> ListRoleByApp(int id)
        //{
        //    int appID = id;
        //    AppRoleModel data = new AppRoleModel();
        //    List<BaseAppList> baseAppLists = await _baseAppListOperation.GetAppList();
        //    List<AppListVM> appLists = _mapper.Map<List<AppListVM>>(baseAppLists);

        //    List<BaseAppRole> baseAppRoles = await _baseAppRoleOperation.GetRolesByApp(appID);
        //    List<AppRoleVM> appRoles = _mapper.Map<List<AppRoleVM>>(baseAppRoles);

        //    data.AppLists = appLists;
        //    data.AppRoleList = appRoles;
        //    data.PageTitle = "Quản lý nhóm người dùng";

        //    return RedirectToAction("Index", data);
        //}
    }
}
