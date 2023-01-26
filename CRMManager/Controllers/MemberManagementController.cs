using AutoMapper;
using CRMLogic.Base.Common;
using CRMManager.Helper.Logic.Customer;
using CRMManager.Models;
using CRMSharedLib.DataType.Logic;
using Helper.WebUtil;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSAServices.Authentication;
using MSAServices.LoyaltyService;
using MSAServices.RetailService;
using SmartBreadcrumbs.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMManager.Controllers
{
    public class MemberManagementController : Controller
    {
        ILocationDataSource _locationDS;
        CustomerManagement _customerManagement;
        public MemberManagementController(CustomerManagement customerManagement, ILocationDataSource locationDS)
        {
            _customerManagement = customerManagement;
            _locationDS = locationDS;
        }

        [Breadcrumb("Quản trị thành viên")]
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
            CustomersViewModel viewModel = new CustomersViewModel();
            viewModel.ProvideList = new SelectList(_locationDS.ProvinceDataSource, "ID", "Name");
            viewModel.DistrictList = new SelectList(_locationDS.DistrictDataSource, "ID", "Name");

            viewModel.PageTitle = "Quản trị thành viên";
            CustomerData customerData = await _customerManagement.GetInitCustomer(demoUser.CompanyID);
            viewModel.BaseCustomers = customerData.BaseCustomers;
            viewModel.MemberClasses = customerData.BaseMemberClasses;
            viewModel.MemClassList = new SelectList(viewModel.MemberClasses, "ID", "ClassName");
            return View(viewModel);
        }

        public IActionResult FilterCustomer(CustomersViewModel viewModel)
        {
            if(!string.IsNullOrEmpty(viewModel.SelectedProvideID))
            {
                viewModel.BaseCustomers = viewModel.BaseCustomers.Where(x => x.CityID == viewModel.SelectedProvideID).ToList();
                if (!string.IsNullOrEmpty(viewModel.SelectedDistrictID))
                {
                    viewModel.BaseCustomers = viewModel.BaseCustomers.Where(x => x.DistrictID == viewModel.SelectedDistrictID).ToList();
                }
                if(viewModel.SelectedMemClassID > 0)
                {
                    viewModel.BaseCustomers = viewModel.BaseCustomers.Where(x => x.MemID == viewModel.SelectedDistrictID).ToList();
                }
            }
            return View();
        }

        [Breadcrumb("Chi tiết")]
        public async Task<IActionResult> Details(string memID)
        {
            CustomerViewModel _detailViewModel = await _customerManagement.GetCustomerByMemID(memID);
            _detailViewModel.PageTitle = "Chi tiết thành viên";
            return View(_detailViewModel);
        }

        [HttpPost]
        public IActionResult Search(CustomerViewModel model)
        {
            return RedirectToAction("Index");
        }

        [Breadcrumb("Chỉnh sửa")]
        [HttpPost]
        public async Task<IActionResult> Edit(CustomerViewModel model)
        {
            model.DataState = EDataState.Edit;
             if(  await _customerManagement.SaveCustomer(model))
                return RedirectToAction("Index");
            else
                return View(model);
        }

    }
}
