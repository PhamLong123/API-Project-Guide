using AutoMapper;
using CRMLogic.Base.BusinessObject;
using CRMManager.Helper.Logic.Loyalty.MemberClass;
using CRMManager.Models;
using CRMSharedLib.DataType.Logic;
using Microsoft.AspNetCore.Mvc;
using MSAServices.Authentication;
using MSAServices.LoyaltyService;
using MSAServices.RetailService;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMManager.Controllers
{
    public class NonMemberClassController : Controller
    {
        CMSMemberClassManagement _cMSMemberClassManagement;
        public NonMemberClassController(CMSMemberClassManagement cMSMemberClassManagement)
        {
            _cMSMemberClassManagement = cMSMemberClassManagement;
        }
        [Breadcrumb("Định nghĩa nhóm không thành viên")]
        public async Task<IActionResult> Index()
        {
            CMSUser demoUser = new CMSUser()
            {
                UserID = "admin",
                BranchID = 1,
                CompanyID = 1,
                LoginDate = System.DateTime.Now,
                SessionID = System.Guid.NewGuid()
            };
            MemberClassesViewModel model = await _cMSMemberClassManagement.GetMemberClasses(String.Empty, demoUser.CompanyID);
            model.BaseMemberClasses = model.BaseMemberClasses.Where(x => x.IsMember == false).ToList();
            //model.BaseMemberClasses = new List<BaseMemberClass>();
            //MemberClassesViewModel model = new MemberClassesViewModel();
            model.BaseMemberClass = new BaseMemberClass();
            model.BaseCompanies = new List<BaseCompany>();
            model.BaseCLPs = new List<BaseCLP>();
            model.PageTitle = "ĐỊNH NGHĨA NHÓM KHÔNG THÀNH VIÊN";
            model.DataState = EDataState.Idle;
            return View(model);
        }

        public IActionResult Detail()
        {
            return View();
        }

        [Breadcrumb("Sửa thông tin khách hàng không thành viên")]
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
            model.PageTitle = "SỬA THÔNG TIN KHÁCH HÀNG KHÔNG THÀNH VIÊN: " + model.ClassName.ToUpper();
            if (model.BaseMemRange == null)
            {
                model.BaseMemRange = new BaseMemRangeClass();
            }
            model.DataState = EDataState.Edit;
            return View("Detail", model);
        }

        [Breadcrumb("Thêm khách hàng không thành viên")]
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
            model.PageTitle = "THÊM KHÁCH HÀNG KHÔNG THÀNH VIÊN";
            model.DataState = EDataState.Create;
            return View("Detail", model);
        }
    }
}
