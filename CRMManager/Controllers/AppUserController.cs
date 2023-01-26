using AutoMapper;
using CRMLogic.Base.BaseBO;
using CRMLogic.Base.Common;
using CRMLogic.Base.CRMManager;
using CRMManager.Helper.Logic.Authentication;
using CRMManager.Models;
using CRMSharedLib.DataType.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSAServices.Authentication;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CRMManager.Helper.Logic.RetailOrgChart.OrgchartHelper.EnumHelper;

namespace CRMManager.Controllers
{
    public class AppUserController : Controller
    {
        //AppUsersViewModel _viewModel;
        AppUserManagement _userManagement;

        public AppUserController(AppUserManagement userManagement)
        {
            _userManagement = userManagement;
        }
        

        [Breadcrumb("Quản trị người dùng")]
        public async Task<IActionResult> Index(int selectedAppID = -1, int selectedRoleID = -1)
        {
            UserData initData=await  _userManagement.GetInitData(selectedAppID);
            AppUsersViewModel viewModel = initData.UsersViewModel;


            List<BaseAppList> AppList = initData.AppList;
            viewModel.AppList = initData.AppList;

            viewModel.PageTitle = "Quản trị người dùng";


            viewModel.SelectListApp = new SelectList(AppList, "ID", "Name");
                
                AppList.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.Name.ToString(),
                    Value = a.ID.ToString(),
                    Selected = false
                };
            });
            

            return View("Index", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetUserByAppID(int selectedAppID)
        {
            return await Index(selectedAppID, -1);

        }

        public async Task<IActionResult> AddRoleToList(AppUserViewModel viewModel, int appID, int SelectedRoleID)
        {
            List<BaseAppRole> Roles = await _userManagement.GetAppRoles(appID);
            BaseAppRole baseAppRole = Roles.Where(x => x.ID == SelectedRoleID).FirstOrDefault();
            if (viewModel.Roles == null)
            {
                viewModel.Roles = new List<BaseAppRole>();
            }
            if (viewModel.Roles != null)
            {
                BaseAppRole RoleOfUser = viewModel.Roles.Where(x => x.ID == baseAppRole.ID).FirstOrDefault();
                if (RoleOfUser != null)
                {
                    viewModel.AppRoles = new SelectList(Roles, "ID", "Name");
                    viewModel.PageTitle = "CHỈNH SỬA THÔNG TIN NGƯỜI DÙNG";
                    viewModel.DataState = EDataState.Edit;
                    viewModel.ErrorMassage = baseAppRole.Name + " Đã tồn tại";
                    viewModel.IsChanged = false;
                    return View("Details", viewModel);
                }
            }
            viewModel.Roles.Add(baseAppRole);
            viewModel.AppRoles = new SelectList(Roles, "ID", "Name");
            viewModel.PageTitle = "CHỈNH SỬA THÔNG TIN NGƯỜI DÙNG";
            viewModel.DataState = EDataState.Edit;
            return View("Details", viewModel);
        }

        public async Task<IActionResult> DeleteRoleFromEdit(AppUserViewModel viewModel, int appID, int roleID)
        {
            List<BaseAppRole> Roles = await _userManagement.GetAppRoles(appID);
            //BaseAppRole baseAppRole = Roles.Where(x => x.ID == roleID).FirstOrDefault();
            if (viewModel.Roles != null)
            {
                //BaseAppRole RoleOfUser = viewModel.Roles.Where(x => x.ID == baseAppRole.ID).FirstOrDefault();
                BaseAppRole RoleOfUser = viewModel.Roles.Where(x => x.ID == roleID).FirstOrDefault();
                if (RoleOfUser != null)
                {
                    viewModel.Roles.Remove(RoleOfUser);
                }
            }
            if (viewModel.Roles == null)
            {
                viewModel.Roles = new List<BaseAppRole>();
            }
            viewModel.AppRoles = new SelectList(Roles, "ID", "Name");
            viewModel.PageTitle = "CHỈNH SỬA THÔNG TIN NGƯỜI DÙNG";
            viewModel.DataState = EDataState.Edit;
            //viewModel.IsChanged = false;
            return View("Create", viewModel);
        }


        [Breadcrumb("Chi tiết")]
        public async Task<IActionResult> Details(string Massage, int ID, int SelectedRoleID = -1)
        {
            
            AppUserViewModel viewModel = await _userManagement.GetUser(ID);
            if (!String.IsNullOrEmpty(Massage))
            {
                viewModel.ErrorMassage = Massage;
                viewModel.IsChanged = true;
            }
            List<BaseAppRole> AppRoleList = await _userManagement.GetAppRoles(viewModel.AppID);

            if(SelectedRoleID != -1)
            {
                BaseAppRole baseAppRole = AppRoleList.Where(x => x.ID == SelectedRoleID).FirstOrDefault();
                BaseAppRole RoleOfUser = viewModel.Roles.Where(x => x.ID == baseAppRole.ID).FirstOrDefault();
                if (RoleOfUser != null)
                {
                    viewModel.SelectedRoleIDs = viewModel.Roles.Select(x => x.ID).ToList();
                    viewModel.AppRoles = new SelectList(AppRoleList, "ID", "Name");
                    viewModel.PageTitle = "CHỈNH SỬA THÔNG TIN NGƯỜI DÙNG";
                    viewModel.DataState = EDataState.Edit;
                    viewModel.ErrorMassage = baseAppRole.Name + " Đã tồn tại";
                    viewModel.IsChanged = false;
                    return View("Details", viewModel);
                }
                viewModel.Roles.Add(baseAppRole);
            }

            viewModel.SelectedRoleIDs = viewModel.Roles.Select(x=>x.ID).ToList();
            viewModel.AppRoles = new SelectList(AppRoleList, "ID", "Name");
            viewModel.PageTitle = "CHỈNH SỬA THÔNG TIN NGƯỜI DÙNG";
            viewModel.DataState = EDataState.Edit;
            return View("Details", viewModel);
        }

        [Breadcrumb("Chỉnh sửa")]
        public async Task<IActionResult> Edit(int ID)
        {
            AppUserViewModel viewModel = await _userManagement.GetUser(ID);
            

            return View(viewModel);

            
        }

        public async Task<IActionResult> AddRoleToCreate(AppUserViewModel viewModel, int appID, int SelectedRoleID)
        {
            List<BaseAppRole> Roles = await _userManagement.GetAppRoles(appID);
            BaseAppRole baseAppRole = Roles.Where(x => x.ID == SelectedRoleID).FirstOrDefault();
            if(viewModel.Roles == null)
            {
                viewModel.Roles = new List<BaseAppRole>();
            }    
            if(viewModel.Roles != null)
            {
                BaseAppRole RoleOfUser = viewModel.Roles.Where(x => x.ID == baseAppRole.ID).FirstOrDefault();
                if (RoleOfUser != null)
                {
                    viewModel.AppRoles = new SelectList(Roles, "ID", "Name");
                    viewModel.PageTitle = "THÊM NGƯỜI DÙNG";
                    viewModel.DataState = EDataState.Create;
                    viewModel.ErrorMassage = baseAppRole.Name + " Đã tồn tại";
                    viewModel.IsChanged = false;
                    return View("Create", viewModel);
                }
            }
            viewModel.Roles.Add(baseAppRole);
            viewModel.AppRoles = new SelectList(Roles, "ID", "Name");
            viewModel.PageTitle = "THÊM NGƯỜI DÙNG";
            return View("Create", viewModel);
        }

        public async Task<ActionResult> DeleteRoleFromCreate(AppUserViewModel viewModel, int appID, int roleID)
        {
            List<BaseAppRole> Roles = await _userManagement.GetAppRoles(appID);
            //BaseAppRole baseAppRole = Roles.Where(x => x.ID == roleID).FirstOrDefault();
            if (viewModel.Roles != null)
            {
                //BaseAppRole RoleOfUser = viewModel.Roles.Where(x => x.ID == baseAppRole.ID).FirstOrDefault();
                BaseAppRole RoleOfUser = viewModel.Roles.Where(x => x.ID == roleID).FirstOrDefault();
                if (RoleOfUser != null)
                {
                    viewModel.Roles.Remove(RoleOfUser);
                }
            }
            if (viewModel.Roles == null)
            {
                viewModel.Roles = new List<BaseAppRole>();
            }
            viewModel.AppRoles = new SelectList(Roles, "ID", "Name");
            viewModel.PageTitle = "THÊM NGƯỜI DÙNG";
            viewModel.DataState = EDataState.Create;
            //viewModel.IsChanged = false;
            return View("Create", viewModel);
        }

        [Breadcrumb("Thêm mới")]
        public async Task<IActionResult> Create(int appID, int SelectedRoleID = -1)
        {  
            List<BaseAppRole> Roles = await _userManagement.GetAppRoles(appID);

            AppUserViewModel viewModel = new AppUserViewModel()
            {
                DataState = EDataState.Create,
                SelectedAppID = appID,
                AppID = appID,
                AppRoleID = null
            };
            viewModel.Roles = new List<BaseAppRole>();
            if (SelectedRoleID != -1)
            {
                BaseAppRole baseAppRole = Roles.Where(x => x.ID == SelectedRoleID).FirstOrDefault();
                BaseAppRole RoleOfUser = viewModel.Roles.Where(x => x.ID == baseAppRole.ID).FirstOrDefault();
                if (RoleOfUser != null)
                {
                    viewModel.AppRoles = new SelectList(Roles, "ID", "Name");
                    viewModel.PageTitle = "THÊM NGƯỜI DÙNG";
                    viewModel.DataState = EDataState.Create;
                    viewModel.ErrorMassage = baseAppRole.Name + " Đã tồn tại";
                    viewModel.IsChanged = false;
                    return View("Create", viewModel);
                }
                viewModel.Roles.Add(baseAppRole);
            }
            viewModel.AppRoles = new SelectList(Roles, "ID", "Name");
            viewModel.PageTitle = "THÊM NGƯỜI DÙNG";
            return View("Create", viewModel);
        }
        [Breadcrumb("Thêm mới")]
        [HttpPost]
        public async Task<IActionResult> Create(AppUserViewModel _detailViewModel)
        {
            //AppUserViewModel _detailViewModel = new AppUserViewModel()
            //{
            //    DataState = EDataState.Create,
            //    Name = "Demo User",
            //    Number = "DemoUser",
            //    Pwd = "123456",
            //    SelectedAppID = 1,


            //};



            if (await _userManagement.SaveUser(_detailViewModel))
            {
                return View("Index");
            }
            else
            {
                return View(_detailViewModel);
            }
        }
        /// <summary>
        /// Luu data khi Edit/Create
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> SaveData(AppUserViewModel viewModel)
        {
            if(viewModel.DataState == EDataState.Create && viewModel.Pwd == null)
            {
                viewModel.ErrorMassage = "Hãy nhập mật khẩu";
                viewModel.IsChanged = false;
                return View("Create", viewModel);
            }
            if (string.IsNullOrEmpty(viewModel.CreatedBy))
            {
                viewModel.CreatedBy = "demo";
                viewModel.CreatedOn = DateTime.Now;
            }
            viewModel.ModifiedBy = "demo";
            viewModel.ModifiedOn = DateTime.Now;
            //Mật khẩu mặc định
            viewModel.AppRoleID = 1;
            bool isSaved = await _userManagement.SaveUser(viewModel);
            if (!isSaved)
            {
                viewModel.ErrorMassage = ErrorMasage.UpdateSuccess.ToString();
                return View("Index");
            }
            else
            {
                return View(viewModel);
            }
            //UserData initData = await _userManagement.GetInitData(viewModel.AppID);
            //AppUsersViewModel model = initData.UsersViewModel;
            //BaseAppUser CurrentUser = model.AppUserList.Where(x => x.Number == viewModel.Number).FirstOrDefault();
            
        }

        public async Task<IActionResult> ChangePass(AppUserViewModel viewModel)
        {
            if(viewModel.NewPass != viewModel.ComfirmNewPass)
            {
                viewModel.ErrorMassage = "Xác nhận mật khẩu không đúng";
                viewModel.IsChanged = false;
                return View("Details", viewModel);
            }
            var result = _userManagement.ChangePwd(viewModel.ID, viewModel.Pwd, viewModel.NewPass);
            viewModel.ErrorMassage = "Cập nhật mật khẩu thành công";
            return await Details(viewModel.ErrorMassage, viewModel.ID);
        }





        //public async Task<JsonResult> GetAppUserList(string appName, int selectedAppID, int selectedRoleID)
        //{
        //    _viewModel.SelectedAppID = selectedAppID;
        //    //_viewModel.SelectedRoleID = selectedRoleID;
        //    // await _viewModel.LoadData();
        //    TempData["SelectedAppName"] = appName;
        //    return Json(_viewModel.AppUserList);
        //}
    }
}
