//using AutoMapper;
//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace CRMManager.Controllers
//{
//    public class NonMemberManagementController : Controller
//    {
//        //BaseCustomerOperation _baseCustomerOperation;
//        IMapper _mapper;
//        NonMembersViewModel _viewModel;
//        public NonMemberManagementController(/*BaseCustomerOperation baseCustomerOperation,*/ IMapper mapper)
//        {
//            //_baseCustomerOperation = baseCustomerOperation;
//            _mapper = mapper;
//            _viewModel = new NonMembersViewModel();
//        }
//        public IActionResult Index()
//        {
//            _viewModel.PageTitle = "Quản trị khách hàng nhóm không là thành viên";
//            return View(_viewModel);
//        }

//        public IActionResult Details(string memID)
//        {
//            NonMemberViewModel _detailViewModel = new NonMemberViewModel();
//            _detailViewModel.PageTitle = "Chi tiết thành viên";
//            return View(_detailViewModel);
//        }

//        [HttpPost]
//        public IActionResult Search(NonMemberViewModel model)
//        {
//            return RedirectToAction("Index");
//        }

//        [HttpPost]
//        public IActionResult Edit(NonMemberViewModel model)
//        {
//            return RedirectToAction("Index");
//        }
//    }
//}
