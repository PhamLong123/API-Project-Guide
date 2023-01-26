using AutoMapper;
using CRMManager.Helper.Logic.RetailOrgChart.Branch;
using CRMManager.Models;
using CRMSharedLib.DataType.Logic;
using Microsoft.AspNetCore.Mvc;
using MSAServices.Authentication;
using System;
using System.Threading.Tasks;
using static CRMManager.Helper.Logic.RetailOrgChart.OrgchartHelper.EnumHelper;

namespace CRMManager.Controllers
{
    public class BranchController : Controller
    {
        IMapper _mapper;
        CMSBranchManagement _cMSBranchManagement;
        public BranchController(IMapper mapper, CMSBranchManagement cMSBranchManagement)
        {
            _mapper = mapper;
            _cMSBranchManagement = cMSBranchManagement;
        }
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
            BranchesViewModel viewModel = await _cMSBranchManagement.GetBranches(string.Empty, demoUser.CompanyID);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BranchesViewModel model)
        {
            CMSUser demoUser = new CMSUser()
            {
                UserID = "admin",
                BranchID = 1,
                CompanyID = 1,
                LoginDate = System.DateTime.Now,
                SessionID = System.Guid.NewGuid()
            };
            BranchesViewModel viewModel = await _cMSBranchManagement.GetBranches(model.KeySearch, demoUser.CompanyID);
            return View(viewModel);
        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult AddNew()
        {
            CMSUser demoUser = new CMSUser()
            {
                UserID = "admin",
                BranchID = 1,
                CompanyID = 1,
                LoginDate = System.DateTime.Now,
                SessionID = System.Guid.NewGuid()
            };
            BranchViewModel viewModel = _cMSBranchManagement.GetDataToCreate(demoUser.CompanyID);
            viewModel.DataState = EDataState.Create;
            viewModel.PageTitle = "THÊM CHI NHÁNH";
            return View("Detail", viewModel);
        }

        public async Task<IActionResult> Edit(string Number)
        {
            CMSUser demoUser = new CMSUser()
            {
                UserID = "admin",
                BranchID = 1,
                CompanyID = 1,
                LoginDate = System.DateTime.Now,
                SessionID = System.Guid.NewGuid()
            };
            BranchViewModel viewModel = await _cMSBranchManagement.GetDataByNumber(Number, demoUser.CompanyID);
            viewModel.DataState = EDataState.Edit;
            viewModel.PageTitle = "SỬA THÔNG TIN CHI NHÁNH";
            return View("Detail", viewModel);
        }

        public async Task<IActionResult> SaveData(BranchViewModel model)
        {
            CMSUser demoUser = new CMSUser()
            {
                UserID = "admin",
                BranchID = 1,
                CompanyID = 1,
                LoginDate = System.DateTime.Now,
                SessionID = System.Guid.NewGuid()
            };
            if (!ModelState.IsValid)
            {
                model.ErrorMasage = ErrorMasage.UpdateFail.ToString();
                model.IsChanged = false;
                return View("Detail", model);
            }
            if (model.DataState == EDataState.Create)
            {
                var result = await _cMSBranchManagement.InsertBranch(model);
            }
            if (model.DataState == EDataState.Edit)
            {
                var result = await _cMSBranchManagement.UpdateBranch(model);
            }
            model = await _cMSBranchManagement.GetDataByNumber(model.Number, demoUser.CompanyID);
            model.ErrorMasage = ErrorMasage.UpdateSuccess.ToString();
            model.DataState = EDataState.Edit;
            model.PageTitle = "SỬA THÔNG TIN CHI NHÁNH";
            return View("Detail", model);
        }
    }
}
