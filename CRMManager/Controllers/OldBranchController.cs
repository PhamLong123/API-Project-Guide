using AutoMapper;
using CRMLogic.Base.BusinessObject;
using CRMLogic.Base.Common;
using CRMLogic.Base.CRMManager;
using CRMManager.Helper.Logic.RetailOrgChart.Branch;
using CRMManager.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CRMManager.Controllers
{
    public class OldBranchController : Controller
    {
        IMapper _mapper;
        CMSBranchManagement _cMSBranchManagement;
        public OldBranchController(IMapper mapper, CMSBranchManagement cMSBranchManagement)
        {
            _mapper = mapper;
            _cMSBranchManagement = cMSBranchManagement;
        }

        [Breadcrumb("Trung tâm thương mại")]
        public async Task<IActionResult> Index()
        {
            BranchesViewModel viewModel = new BranchesViewModel();
            CMSBranchData initData = await _cMSBranchManagement.GetInitBranch(1);
            viewModel.BaseBranches = initData.BaseBranches;
            return View(viewModel);
        }

        [Breadcrumb("Thêm trung tâm thương mại")]
        public IActionResult AddNew()
        {
            BranchesViewModel model = new BranchesViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddNew(BranchesViewModel model)
        {
            return RedirectToAction("Index");
        }

        [Breadcrumb("Sửa thông tin trung tâm thương mại")]
        public async Task<IActionResult> Edit(int ID)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BranchesViewModel viewModel)
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete()
        {
            return RedirectToAction("Index");
        }
    }
}
