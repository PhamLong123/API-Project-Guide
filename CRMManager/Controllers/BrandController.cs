using AutoMapper;
using CRMManager.Helper.Logic.RetailOrgChart.Brand;
using CRMManager.Models;
using CRMSharedLib.DataType.Logic;
using Microsoft.AspNetCore.Mvc;
using MSAServices.Authentication;
using SmartBreadcrumbs.Attributes;
using System;
using System.Threading.Tasks;
using static CRMManager.Helper.Logic.RetailOrgChart.OrgchartHelper.EnumHelper;
using CRMSharedLib.DataType.Logic;

namespace CRMManager.Controllers
{
    public class BrandController : Controller
    {
        IMapper _mapper;
        CMSBrandManagerment _cMSBrandManagerment;
        public BrandController(IMapper mapper, CMSBrandManagerment cMSBrandManagerment)
        {
            _mapper = mapper;
            _cMSBrandManagerment = cMSBrandManagerment;

        }
        [Breadcrumb("Doanh nghiệp")]
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
            BrandsViewModel viewModel = await _cMSBrandManagerment.GetBrands(String.Empty, demoUser.CompanyID);
            viewModel.PageTitle = "DANH SÁCH DOANH NGHIỆP";
            return View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Index(BrandsViewModel viewModel)
        {
            CMSUser demoUser = new CMSUser()
            {
                UserID = "admin",
                BranchID = 1,
                CompanyID = 1,
                LoginDate = System.DateTime.Now,
                SessionID = System.Guid.NewGuid()
            };
            viewModel = await _cMSBrandManagerment.GetBrands(viewModel.KeySearch, demoUser.CompanyID);
            viewModel.PageTitle = "DANH SÁCH DOANH NGHIỆP";
            return View(viewModel);

        }

        public IActionResult Detail()
        {
            return View();
        }

        [Breadcrumb("Thêm doanh nghiệp")]
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
            BrandViewModel model = _cMSBrandManagerment.GetDataToCreate(demoUser.CompanyID);
            model.DataState = EDataState.Create;
            model.PageTitle = "THÊM DOANH NGHIỆP";
            return View("Detail", model);
        }

        [Breadcrumb("Sửa thông tin doanh nghiệp")]
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
            BrandViewModel model = await _cMSBrandManagerment.GetDataByNumber(Number, demoUser.CompanyID);
            if (!String.IsNullOrEmpty(Massage))
            {
                model.ErrorMasage = Massage;
                model.IsChanged = true;
            }
            model.DataState = EDataState.Edit;
            model.PageTitle = "CHỈNH SỬA THÔNG TIN DOANH NGHIỆP";
            return View("Detail", model);
        }

        [Breadcrumb("Chi tiết thông tin doanh nghiệp")]
        public async Task<IActionResult> SaveData(BrandViewModel model)
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
                var result = await _cMSBrandManagerment.InsertBrand(model);
            }
            if (model.DataState == EDataState.Edit)
            {
                var result = await _cMSBrandManagerment.UpdateBrand(model);
            }
            model = await _cMSBrandManagerment.GetDataByNumber(model.Number, demoUser.CompanyID);
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
