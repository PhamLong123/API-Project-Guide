using AutoMapper;
using CRMLogic.Base.Common;
using CRMLogic.Base.CRMManager;
using CRMManager.Helper.Logic.RetailOrgChart.BussinessCategory;
using CRMManager.Models;
using CRMSharedLib.DataType.Logic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSAServices.Authentication;
using MSAServices.ContentService;
using SmartBreadcrumbs.Attributes;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static CRMManager.Helper.Logic.RetailOrgChart.OrgchartHelper.EnumHelper;

namespace CRMManager.Controllers
{
    public class BusinessCategoryController : Controller
    {
        IMapper _mapper;
        CMSBussinessCategoryManagerment _cMSBussinessCategoryManagerment;
        public BusinessCategoryController(
            IMapper mapper, CMSBussinessCategoryManagerment cMSBussinessCategoryManagerment)
        {
            _mapper = mapper;
            _cMSBussinessCategoryManagerment = cMSBussinessCategoryManagerment;
        }

        [Breadcrumb("Ngành hàng")]
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
            BusinessCategoriesViewModel model = await _cMSBussinessCategoryManagerment.GetBusinessCategories(String.Empty);
            model.PageTitle = "DANH SÁCH NGÀNH HÀNG";
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BusinessCategoriesViewModel model)
        {
            CMSUser demoUser = new CMSUser()
            {
                UserID = "admin",
                BranchID = 1,
                CompanyID = 1,
                LoginDate = System.DateTime.Now,
                SessionID = System.Guid.NewGuid()
            };
            model = await _cMSBussinessCategoryManagerment.GetBusinessCategories(model.KeySearch);
            return View(model);
        }

        public IActionResult Details()
        {
            return View();
        }

        [Breadcrumb("Sửa thông tin ngành hàng")]
        public async Task<IActionResult> Edit(string Massage, string Number)
        {
            BusinessCategoryViewModel model = await _cMSBussinessCategoryManagerment.GetDataByNumber(Number);
            if (!String.IsNullOrEmpty(Massage))
            {
                model.ErrorMasage = Massage;
                model.IsChanged = true;
            }
            model.PageTitle = "SỬA THÔNG TIN NGÀNH HÀNG";
            model.DataState = EDataState.Edit;
            return View("Details", model);
        }

        [Breadcrumb("Thêm ngành hàng")]
        public IActionResult Create()
        {
            BusinessCategoryViewModel model = _cMSBussinessCategoryManagerment.GetDataToCreate();
            model.PageTitle = "THÊM NGÀNH HÀNG";
            model.DataState = EDataState.Create;
            return View("Details", model);
        }

        public async Task<IActionResult> SaveData(BusinessCategoryViewModel model)
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
                return View("Details", model);
            }
            if (model.DataState == EDataState.Create)
            {
                var result = await _cMSBussinessCategoryManagerment.InsertBusinessCategory(model);
            }
            if(model.DataState == EDataState.Edit)
            {
                var result = await _cMSBussinessCategoryManagerment.UpdateBusinessCategory(model);
            }
            model = await _cMSBussinessCategoryManagerment.GetDataByNumber(model.Number);
            model.ErrorMasage = ErrorMasage.UpdateSuccess.ToString();
            return await Edit(model.ErrorMasage, model.Number);
        }

        public IActionResult Delete(BusinessCategoryViewModel model)
        {
            
            return RedirectToAction("Index");
        }

    }
}
