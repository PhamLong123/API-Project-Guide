using AutoMapper;
using CRMManager.Helper;
using CRMManager.Helper.Logic.RetailOrgChart.MallUtil;
using CRMManager.Models;
using Microsoft.AspNetCore.Mvc;
using MSAServices.Authentication;
using CRMSharedLib.DataType.Logic;
using MSAServices.ContentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CRMManager.Helper.Logic.RetailOrgChart.OrgchartHelper.EnumHelper;

namespace CRMManager.Controllers
{
    public class MallUtilController : Controller
    {
        IMapper _mapper;
        CMSMallUtilManagerment _cMSMallUtilManagerment;
        public MallUtilController(IMapper mapper, CMSMallUtilManagerment cMSMallUtilManagerment)
        {
            _mapper = mapper;
            _cMSMallUtilManagerment = cMSMallUtilManagerment;
        }
        public async Task<IActionResult> Index()
        {
            DataConverter dataConverter = new DataConverter();

            CMSUser demoUser = new CMSUser()
            {
                UserID = "admin",
                BranchID = 1,
                CompanyID = 1,
                LoginDate = System.DateTime.Now,
                SessionID = System.Guid.NewGuid()
            };
            MallUtilsViewModel model = await _cMSMallUtilManagerment.GetMallUtils(String.Empty, demoUser.CompanyID);
            model.PageTitle = "DANH SÁCH TIỆN ÍCH";
            return View(model);
        }

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
            MallUtilViewModel viewModel = await _cMSMallUtilManagerment.GetDataToCreate(demoUser.CompanyID, demoUser.BranchID);
            viewModel.PageTitle = "THÊM TIỆN ÍCH";
            viewModel.DataState = EDataState.Create;
            return View("Detail", viewModel);
        }

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
            MallUtilViewModel viewModel = await _cMSMallUtilManagerment.GetDataByNumber(Number, demoUser.CompanyID);
            if (!String.IsNullOrEmpty(Massage))
            {
                viewModel.ErrorMasage = Massage;
                viewModel.IsChanged = true;
            }
            viewModel.PageTitle = "SỬA THÔNG TIN TIỆN ÍCH";
            viewModel.DataState = EDataState.Edit;
            return View("Detail", viewModel);
        }

        public IActionResult Detail()
        {
            return View();
        }

        /// <summary>
        /// Hàm sử lí create hoặc update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveData(MallUtilViewModel model)
        {
            CMSUser demoUser = new CMSUser()
            {
                UserID = "admin",
                BranchID = 1,
                CompanyID = 1,
                LoginDate = System.DateTime.Now,
                SessionID = System.Guid.NewGuid()
            };
            if (model.FloorID == null)
            {
                model.ErrorMasage = FieldRequire.FloorRequire.ToString();
                model.IsChanged = false;
                return View("Detail", model);
            }
            if (String.IsNullOrEmpty(model.Number))
            {
                model.ErrorMasage = FieldRequire.NumberRequire.ToString();
                model.IsChanged = false;
                return View("Detail", model);
            }
            if (String.IsNullOrEmpty(model.Number))
            {
                model.ErrorMasage = FieldRequire.NameRequire.ToString();
                model.IsChanged = false;
                return View("Detail", model);
            }
            //if (!ModelState.IsValid)
            //{
            //    model.ErrorMasage = ErrorMasage.UpdateFail.ToString();
            //    model.IsChanged = false;
            //    return View("Detail", model);
            //}
            if (model.DataState == EDataState.Create)
            {
                var result = await _cMSMallUtilManagerment.InsertMallUtil(model);
            }
            if (model.DataState == EDataState.Edit)
            {
                var result = await _cMSMallUtilManagerment.UpdateMallUtil(model);
            }
            model = await _cMSMallUtilManagerment.GetDataByNumber(model.Number, demoUser.CompanyID);
            model.ErrorMasage = ErrorMasage.UpdateSuccess.ToString();
            return await Edit(model.ErrorMasage, model.Number);
        }
    }
}
