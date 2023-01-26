using AutoMapper;
using CRMLogic.Base.BusinessObject;
using CRMLogic.Base.Common;
using CRMLogic.Base.CRMManager;
using CRMManager.Helper.Logic.RetailOrgChart.Floor;
using CRMManager.Models;
using CRMSharedLib.DataType.Logic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSAServices.Authentication;
using MSAServices.ContentService;
using MSAServices.RetailService;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static CRMManager.Helper.Logic.RetailOrgChart.OrgchartHelper.EnumHelper;

namespace CRMManager.Controllers
{
    public class FloorController : Controller
    {
        CMSFloorManagerment _cMSFloorManagerment;
        IMapper _mapper;
        public FloorController(IMapper mapper, CMSFloorManagerment cMSFloorManagerment)
        {
            _mapper = mapper;
            _cMSFloorManagerment = cMSFloorManagerment;
        }

        [Breadcrumb("Tầng")]
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
            FloorsViewModel viewModel = await _cMSFloorManagerment.GetFloors(String.Empty);
            viewModel.PageTitle = "DANH SÁCH TẦNG";
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(FloorsViewModel viewModel)
        {
            CMSUser demoUser = new CMSUser()
            {
                UserID = "admin",
                BranchID = 1,
                CompanyID = 1,
                LoginDate = System.DateTime.Now,
                SessionID = System.Guid.NewGuid()
            };
            viewModel = await _cMSFloorManagerment.GetFloors(viewModel.KeySearch);
            return View(viewModel);
        }

        [Breadcrumb("Chi tiết tầng")]
        public IActionResult Detail()
        {
            return View();
        }

        [Breadcrumb("Thêm tầng")]
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
            FloorViewModel model = _cMSFloorManagerment.GetDataToCreate(demoUser.BranchID);
            model.DataState = EDataState.Create;
            model.PageTitle = "THÊM CHI NHÁNH";
            return View("Detail", model);
        }

        [Breadcrumb("Sửa tầng")]
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
            FloorViewModel model = await _cMSFloorManagerment.GetDataByNumber(Number);
            if (!String.IsNullOrEmpty(Massage))
            {
                model.ErrorMasage = Massage;
                model.IsChanged = true;
            }
            model.DataState = EDataState.Edit;
            model.PageTitle = "SỬA THÔNG TIN TẦNG";
            return View("Detail", model);
        }

        [Breadcrumb("Chi tiết tầng")]
        public async Task<IActionResult> SaveData(FloorViewModel model)
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
                var result = await _cMSFloorManagerment.InsertFloor(model);
            }
            if (model.DataState == EDataState.Edit)
            {
                var result = await _cMSFloorManagerment.UpdateFloor(model);
            }
            model = await _cMSFloorManagerment.GetDataByNumber(model.Number);
            model.ErrorMasage = ErrorMasage.UpdateSuccess.ToString();
            return await Edit(model.ErrorMasage, model.Number);
        }

        [HttpPost]
        public IActionResult Delete()
        {
            return RedirectToAction("Index");
        }
        
    }
}
