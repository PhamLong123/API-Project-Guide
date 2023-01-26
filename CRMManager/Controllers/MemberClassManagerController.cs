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
//    public class MemberClassManagerController : Controller
//    {
//        BaseMemberClassOperation _baseMemberClassOperation;
//        MemberClassesViewModel _viewmodel;
//        IMapper _mapper;
//        public MemberClassManagerController(BaseMemberClassOperation baseMemberClassOperation,
//            IMapper mapper)
//        {
//            _baseMemberClassOperation = baseMemberClassOperation;
//            _mapper = mapper;
//            _viewmodel = new MemberClassesViewModel(_baseMemberClassOperation, _mapper);
//        }

//        [Breadcrumb("Định nghĩa hạng thẻ nhóm thành viên")]
//        public async Task<IActionResult> Index()
//        {
//            await _viewmodel.GetData();
//            return View(_viewmodel);
//        }

//        [Breadcrumb("Sửa thông tin nhóm thành viên")]
//        public async Task<IActionResult> Edit(int ID)
//        {
//            await _viewmodel.GetDataByID(ID);
//            return View(_viewmodel);
//        }

//        [Breadcrumb("Thêm nhóm thành viên")]
//        public async Task<IActionResult> AddNew(int ID)
//        {   
//            return View(_viewmodel);
//        }
//    }
//}
