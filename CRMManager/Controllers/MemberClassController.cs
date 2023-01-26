using CRMManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using AutoMapper;
using CRMLogic.Base.BusinessObject;
using CRMLogic.Base.CRMManager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MSAServices.LoyaltyService;
using MSAServices.RetailService;
using MSAServices.ContentService;
using SmartBreadcrumbs.Attributes;
using CRMSharedLib.DataType.Logic;
using static CRMManager.Helper.Logic.RetailOrgChart.OrgchartHelper.EnumHelper;
using CRMManager.Helper.Logic.Loyalty.MemberClass;
using MSAServices.Authentication;
using CRMManager.Helper.Logic.Authentication;

namespace CRMManager.Controllers
{
    public class MemberClassController : Controller
    {
        CMSMemberClassManagement _cMSMemberClassManagement;
        public MemberClassController(CMSMemberClassManagement cMSMemberClassManagement)
        {
            _cMSMemberClassManagement = cMSMemberClassManagement;
        }
        #region MemberClass
        [Breadcrumb("Định nghĩa hạng thẻ")]
        public async Task<IActionResult> Index()
        {
            // Lấy user ở session
            // Lấy được danh sách quyền, role
            LoginUser user = null; // getSession
            if(!user.Roles.Contains("MemberClass"))
            {
                // trả về view không có quyền
                return View("NotPermission");
            }

            //CMSUser demoUser = new CMSUser()
            //{
            //    UserID = "admin",
            //    BranchID = 1,
            //    CompanyID = 1,
            //    LoginDate = System.DateTime.Now,
            //    SessionID = System.Guid.NewGuid()
            //};
            MemberClassesViewModel model = await _cMSMemberClassManagement.GetMemberClasses(String.Empty, demoUser.CompanyID);
            model.BaseMemberClasses = model.BaseMemberClasses.Where(x => x.IsMember == true).ToList();
            //model.BaseMemberClasses = new List<BaseMemberClass>();
            //MemberClassesViewModel model = new MemberClassesViewModel();
            model.BaseMemberClass = new BaseMemberClass();
            model.BaseCompanies = new List<BaseCompany>();
            model.BaseCLPs = new List<BaseCLP>();
            model.PageTitle = "ĐỊNH NGHĨA HẠNG THẺ";
            model.DataState = EDataState.Idle;
            return View(model);
        }

        [Breadcrumb("Định nghĩa hạng thẻ")]
        [HttpPost]
        public async Task<IActionResult> Index(MemberClassesViewModel model)
        {
            return View(model);
        }

        public IActionResult Detail()
        {
            return View();
        }


        //[Breadcrumb("Chính sách tăng hạng")]
        //public async Task<IActionResult> RankingPolicy(int ID)
        //{
        //    CMSUser demoUser = new CMSUser()
        //    {
        //        UserID = "admin",
        //        BranchID = 1,
        //        CompanyID = 1,
        //        LoginDate = System.DateTime.Now,
        //        SessionID = System.Guid.NewGuid()
        //    };
        //    MemRangeClassViewModel model = await _cMSMemberClassManagement.GetDataByClassID(ID, demoUser.CompanyID);
        //    //model.PageTitle = "CHÍNH SÁCH TĂNG HẠNG";
        //    return View(model);
        //}

        [Breadcrumb("Thêm hạng thẻ")]
        public async Task<IActionResult> AddNew()
        {
            CMSUser demoUser = new CMSUser()
            {
                UserID = "admin",
                BranchID = 1,
                CompanyID = 1,
                LoginDate = System.DateTime.Now,
                SessionID = System.Guid.NewGuid()
            };
            MemberClassViewModel model = await _cMSMemberClassManagement.GetDataToCreate(demoUser.CompanyID);
            model.PageTitle = "THÊM HẠNG THẺ";
            model.DataState = EDataState.Create;
            return View("Detail", model);
        }


        [Breadcrumb("Sửa hạng thẻ")]
        public async Task<IActionResult> Edit(string Massage, string Number)
        {
            CMSUser demoUser = new CMSUser()
            {
                UserID = "admin",
                BranchID = 1,
                CompanyID = 1,
                LoginDate = System.DateTime.Now,
                SessionID = System.Guid.NewGuid()
            };
            MemberClassViewModel model = await _cMSMemberClassManagement.GetDataByNumber(Number, demoUser.CompanyID);
            model.BaseMemRange = await _cMSMemberClassManagement.GetDataByClassID(model.ID, demoUser.CompanyID);
            if (!String.IsNullOrEmpty(Massage))
            {
                model.ErrorMasage = Massage;
                model.IsChanged = true;
            }
            model.PageTitle = "SỬA THÔNG TIN HẠNG THẺ: " + model.ClassName.ToUpper();
            if(model.BaseMemRange == null)
            {
                model.BaseMemRange = new BaseMemRangeClass();
            }
            model.DataState = EDataState.Edit;
            return View("Detail", model);
        }

        [Breadcrumb("Định nghĩa hạng thẻ")]
        public async Task<IActionResult> SaveData(MemberClassViewModel model)
        {
            CMSUser demoUser = new CMSUser()
            {
                UserID = "admin",
                BranchID = 1,
                CompanyID = 1,
                LoginDate = System.DateTime.Now,
                SessionID = System.Guid.NewGuid()
            };
            if (String.IsNullOrEmpty(model.ClassNo))
            {
                model.ErrorMasage = FieldRequire.NumberRequire.ToString();
                model.IsChanged = false;
                return View("Detail", model);
            }
            if (String.IsNullOrEmpty(model.ClassName))
            {
                model.ErrorMasage = FieldRequire.NameRequire.ToString();
                model.IsChanged = false;
                return View("Detail", model);
            }
            if (model.DataState == EDataState.Edit)
            {
                var result = _cMSMemberClassManagement.UpdateData(model);
                var rangeresult = _cMSMemberClassManagement.UpdateMemRangeData(model.BaseMemRange);
            }
            if(model.DataState == EDataState.Create)
            {
                model.CLPID = 1;
                model.CompanyID = demoUser.CompanyID;
                model.BaseMemRange.CLPID = 1;
                model.BaseMemRange.DurationID = 5;
                model.BaseMemRange.MemberClassID = model.ID;
                model.BaseMemRange.ClassNo = model.ClassNo;
                model.BaseMemRange.CreatedOn = DateTime.Now;
                model.BaseMemRange.CreatedBy = demoUser.UserID;
                var result = _cMSMemberClassManagement.InsertData(model);
                MemberClassViewModel newCreateModel = await _cMSMemberClassManagement.GetDataByNumber(model.ClassNo, demoUser.CompanyID);
                model.BaseMemRange.MemberClassID = newCreateModel.ID;
                var resultRangeClass = _cMSMemberClassManagement.InsertMemRangeData(model.BaseMemRange);
            }
            model = await _cMSMemberClassManagement.GetDataByNumber(model.ClassNo, demoUser.CompanyID);
            model.BaseMemRange = await _cMSMemberClassManagement.GetDataByClassID(model.ID, demoUser.CompanyID);
            model.ErrorMasage = ErrorMasage.UpdateSuccess.ToString();
            return await Edit(model.ErrorMasage, model.ClassNo);
        }
        #endregion

    }
}
