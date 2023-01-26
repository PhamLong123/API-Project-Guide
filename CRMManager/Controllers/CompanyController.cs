using AutoMapper;
using CRMLogic.Base.BusinessObject;
using CRMLogic.Base.Common;
using CRMLogic.Base.CRMManager;
using CRMManager.Helper.Logic.RetailOrgChart.Company;
using CRMManager.Models;
using Microsoft.AspNetCore.Mvc;
using MSAServices.Authentication;
using MSAServices.RetailService;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CRMManager.Helper.Logic.RetailOrgChart.OrgchartHelper.EnumHelper;

namespace CRMManager.Controllers
{
    public class CompanyController : Controller
    { 
        IMapper _mapper;
        CMSCompanyManagerment _cMSCompanyManagerment;
        public CompanyController(IMapper mapper, CMSCompanyManagerment cMSCompanyManagerment)
        {
            _mapper = mapper;
            _cMSCompanyManagerment = cMSCompanyManagerment;
        }

        [Breadcrumb("Công ty")]
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
            CompanyViewModel companyView = await _cMSCompanyManagerment.GetCompany(demoUser.CompanyID);
            companyView.PageTitle = "THÔNG TIN CÔNG TY";
            return View(companyView);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CompanyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ErrorMasage = ErrorMasage.UpdateFail.ToString();
                model.IsChanged = false;
                return View("Index", model);
            }
            var result = await _cMSCompanyManagerment.UpdateCompany(model);
            if(result == false)
            {
                model.ErrorMasage = ErrorMasage.UpdateFail.ToString();
                model.IsChanged = false;
            }
            model.ErrorMasage = ErrorMasage.UpdateSuccess.ToString();
            return View("Index", model);
        }
        [HttpPost]
        public IActionResult Delete()
        {
            return RedirectToAction("Index");
        }
    }
}
