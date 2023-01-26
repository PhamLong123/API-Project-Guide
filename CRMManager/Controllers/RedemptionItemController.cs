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
//    public class RedemptionItemController : Controller
//    {
//        BaseRedemptionItemsOperation _baseRedemptionItemsOperation;
//        BaseRedemptionGroupOperation _baseRedemptionGroupOperation;
//        BaseMemberClassOperation _baseMemberClassOperation;
//        RedeemItemMallsViewModel _viewModel;
//        IMapper _mapper;
//        public RedemptionItemController(BaseRedemptionItemsOperation baseRedemptionItemsOperation,
//            BaseRedemptionGroupOperation baseRedemptionGroupOperation,
//            BaseMemberClassOperation baseMemberClassOperation,
//            IMapper mapper)
//        {
//            _baseRedemptionItemsOperation = baseRedemptionItemsOperation;
//            _baseRedemptionGroupOperation = baseRedemptionGroupOperation;
//            _baseMemberClassOperation = baseMemberClassOperation;
//            _mapper = mapper;
//            _viewModel = new RedeemItemMallsViewModel(baseRedemptionItemsOperation, baseRedemptionGroupOperation, baseMemberClassOperation, mapper);
//        }

//        [Breadcrumb("Danh sách quà")]
//        public async Task<IActionResult> Index()
//        {
//            await _viewModel.GetData();
//            return View(_viewModel);
//        }

//        [Breadcrumb("Thêm quà")]
//        public IActionResult AddNew()
//        {
//            RedeemItemMallsViewModel model = new RedeemItemMallsViewModel();
//            return View(model);
//        }
//        [HttpPost]
//        public async Task<IActionResult> AddNew(RedeemItemMallsViewModel model)
//        {
//            await _viewModel.InsertData(model.RedeemItemMall);
//            return RedirectToAction("RedeemItemMall");
//        }

//        [Breadcrumb("Sửa thông tin quà")]
//        public async Task<IActionResult> Edit(int ID)
//        {
//            await _viewModel.GetDataByID(ID);
//            return View(_viewModel);
//        }
//        [HttpPost]
//        public async Task<IActionResult> Edit(RedeemItemMallsViewModel model)
//        {
//            await _viewModel.UpdateData(model.RedeemItemMall);
//            return View(_viewModel);
//        }

//        [HttpPost]
//        public IActionResult Delete()
//        {
//            return RedirectToAction("RedeemItemMall");
//        }
//    }
//}
