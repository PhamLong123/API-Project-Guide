using CRMManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using CRMLogic.Base.CRMManager;
using AutoMapper;
using CRMLogic.Base.Common;
using System.Text;
using SmartBreadcrumbs.Attributes;
using MSAServices.RetailService;
using CRMManager.Helper.Logic.RetailOrgChart.Outlet;
using CRMManager.Helper;
using MSAServices.Authentication;
using MSAServices.ContentService;
using static CRMManager.Helper.Logic.RetailOrgChart.OrgchartHelper.EnumHelper;
using CRMSharedLib.DataType.Logic;

namespace CRMManager.Controllers
{
    public class OutletController : Controller
    {
        private readonly IWebHostEnvironment _webhost;
        IMapper _mapper;
        CMSOutletManagement _cMSOutlet;
        public OutletController(IWebHostEnvironment webHost, IMapper mapper, CMSOutletManagement cMSOutlet)
        {
            _webhost = webHost;
            _mapper = mapper;
            _cMSOutlet = cMSOutlet;
        }

        [Breadcrumb("Cửa hàng")]
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
            DataConverter dataConverter = new DataConverter();
            OutletsViewModel model = await _cMSOutlet.GetOutlet(-1, -1, -1, demoUser.BranchID, demoUser.CompanyID, string.Empty);
            model.BrandList = dataConverter.GetNewSelectItemsByNumber(model.BaseBrands.ToArray());
            model.FloorList = dataConverter.GetNewSelectItemsByNumber(model.BaseFloors.ToArray());
            model.BussinessList = dataConverter.GetNewSelectItemsByNumber(model.BaseBusinesses.ToArray());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(OutletsViewModel model)
        {
            CMSUser demoUser = new CMSUser()
            {
                UserID = "admin",
                BranchID = 1,
                CompanyID = 1,
                LoginDate = System.DateTime.Now,
                SessionID = System.Guid.NewGuid()
            };
            DataConverter dataConverter = new DataConverter();
            int SelectFloorID = Int32.Parse(model.SelectedFloorID);
            int SelectBrandID = Int32.Parse(model.SelectedBrandID);
            int SelectBussinessID = Int32.Parse(model.SelectedBussinessID);
            model = await _cMSOutlet.GetOutlet(SelectFloorID, SelectBussinessID, SelectBrandID, demoUser.BranchID, demoUser.CompanyID, model.KeySearch);
            model.BrandList = dataConverter.GetNewSelectItemsByNumber(model.BaseBrands.ToArray());
            model.FloorList = dataConverter.GetNewSelectItemsByNumber(model.BaseFloors.ToArray());
            model.BussinessList = dataConverter.GetNewSelectItemsByNumber(model.BaseBusinesses.ToArray());
            return View(model);
        }

        public IActionResult Detail()
        {
            return View();
        }

        [Breadcrumb("Thêm cửa hàng")]
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
            OutletViewModel viewModel = await _cMSOutlet.GetDataToCreate(demoUser.BranchID, demoUser.CompanyID);
            viewModel.PageTitle = "THÊM CỬA HÀNG";
            viewModel.DataState = EDataState.Create;
            return View("Detail", viewModel);
        }

        [Breadcrumb("Sửa thông tin cửa hàng")]
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
            OutletViewModel viewModel = await _cMSOutlet.GetOutletByNumber(Number, demoUser.BranchID, demoUser.CompanyID);
            if (!String.IsNullOrEmpty(Massage))
            {
                viewModel.ErrorMasage = Massage;
                viewModel.IsChanged = true;
            }
            viewModel.PageTitle = "SỬA THÔNG TIN CỬA HÀNG";
            viewModel.DataState = EDataState.Edit;
            return View("Detail", viewModel);
        }

        /// <summary>
        /// Hàm sử lí create hoặc update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveData(OutletViewModel model)
        {
            if (String.IsNullOrEmpty(model.Number))
            {
                model.ErrorMasage = FieldRequire.NumberRequire.ToString();
                return View("Detail", model);
            }
            if (String.IsNullOrEmpty(model.OutletName))
            {
                model.ErrorMasage = FieldRequire.NameRequire.ToString();
                return View("Detail", model);
            }
            if (String.IsNullOrEmpty(model.Address))
            {
                model.ErrorMasage = FieldRequire.AddressRequire.ToString();
                return View("Detail", model);
            }
            if (model.FloorID <= 0)
            {
                model.ErrorMasage = FieldRequire.FloorRequire.ToString();
                return View("Detail", model);
            }
            if (model.CategoryID <= 0)
            {
                model.ErrorMasage = FieldRequire.BussinessCategoryRequire.ToString();
                return View("Detail", model);
            }
            if (model.BrandID <= 0)
            {
                model.ErrorMasage = FieldRequire.BrandRequire.ToString();
                return View("Detail", model);
            }
            if (model.DataState == EDataState.Create)
            {
                var result = await _cMSOutlet.InsertOutlet(model);
            }
            if(model.DataState == EDataState.Edit)
            {
                var result = await _cMSOutlet.UpdateOutlet(model);
            }
            model.ErrorMasage = ErrorMasage.UpdateSuccess.ToString();
            return await Edit(model.ErrorMasage, model.Number);
        }

        [HttpPost]
        public IActionResult Delete()
        {
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public IActionResult ExportToCSV(OutletsViewModel model)
        //{
        //    var builder = new StringBuilder();
        //    builder.AppendLine("ID, Number, OutletName");
        //    foreach (var item in model.BaseOutlets)
        //    {
        //        builder.AppendLine($"{item.ID}, {item.Number}, {item.OutletName}");
        //    }
        //    return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "Outlet.csv");
        //}

        //public IActionResult ExportToExcel(OutletsViewModel model)
        //{
        //    using(var workbook = new XLWorkbook())
        //    {
        //        var worksheet = workbook.Worksheets.Add("Outlet");
        //        var currentRow = 1;
        //        worksheet.Cell(currentRow, 1).Value = "ID";
        //        worksheet.Cell(currentRow, 2).Value = "Number";
        //        worksheet.Cell(currentRow, 3).Value = "OutletName";
        //        foreach(var item in model.BaseOutlets)
        //        {
        //            currentRow++;
        //            worksheet.Cell(currentRow, 1).Value = item.ID;
        //            worksheet.Cell(currentRow, 2).Value = item.Number;
        //            worksheet.Cell(currentRow, 3).Value = item.OutletName;
        //        }
        //        using (var stream = new MemoryStream()) 
        //        {
        //            workbook.SaveAs(stream);
        //            var content = stream.ToArray();
        //            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Outletex.xlsx");
        //        }
        //    }
        //}
    }
}
