//using AutoMapper;
//using CRMLogic.Base.CRMManager;
//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using SmartBreadcrumbs.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class RedemptionGroupController : Controller
//    {
//        BaseRedemptionGroupOperation _baseRedemptionGroupOperation;
//        RedemptionGroupsViewModel _viewmodel;
//        IMapper _mapper;
//        public RedemptionGroupController(BaseRedemptionGroupOperation baseRedemptionGroupOperation,
//            IMapper mapper)
//        {
//            _baseRedemptionGroupOperation = baseRedemptionGroupOperation;
//            _mapper = mapper;
//            _viewmodel = new RedemptionGroupsViewModel(baseRedemptionGroupOperation, mapper);
//        }

//        [Breadcrumb("Nhóm quà")]
//        public async Task<IActionResult> Index()
//        {
//            await _viewmodel.GetData();
//            return View(_viewmodel);
//        }

//        [Breadcrumb("Thêm nhóm quà")]
//        public IActionResult AddNew()
//        {
//            RedemptionGroupsViewModel model = new RedemptionGroupsViewModel();
//            return View(model);
//        }
//        [HttpPost]
//        public async Task<IActionResult> AddNew(RedemptionGroupsViewModel model)
//        {
//            await _viewmodel.InsertData(model.RedemptionGroup);
//            return RedirectToAction("Index");
//        }

//        [Breadcrumb("Sửa nhóm quà")]
//        public async Task<IActionResult> Edit(int ID)
//        {
//            await _viewmodel.GetDataByID(ID);
//            return View(_viewmodel);
//        }
//        [HttpPost]
//        public async Task<IActionResult> Edit(RedemptionGroupsViewModel model)
//        {
//            await _viewmodel.UpdateData(model.RedemptionGroup);
//            return View(_viewmodel);
//        }

//        [HttpPost]
//        public IActionResult Delete()
//        {
//            return RedirectToAction("Index");
//        }
//    }
//}
