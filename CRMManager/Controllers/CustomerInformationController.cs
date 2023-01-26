//using AutoMapper;
//using CRMLogic.Base.Common;
//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using MSAServices.RetailService;

//namespace CRMManager.Controllers
//{
//    public class CustomerInformationController : Controller
//    {
//        //BaseCustomerOperation _baseCustomerOperation;
//        RetailOrgChartService _retailOrgChart;
//        IMapper _mapper;
//        CustomersInformationViewModel _viewModel;
//        BaseCRMManagerApp _crmManagerApp;

//        public CustomerInformationController(/*BaseCustomerOperation baseCustomerOperation,*/ RetailOrgChartService retailOrgChart, IMapper mapper,
//              BaseCRMManagerApp crmApp)
//        {
//            //_baseCustomerOperation = baseCustomerOperation;
//            _retailOrgChart = retailOrgChart;
//            _mapper = mapper;
//            _crmManagerApp = crmApp;
//            //_viewModel = new CustomersInformationViewModel(RetailOrgChartService _retailOrgChart, _mapper, _crmManagerApp);
//        }

//        public IActionResult Index()
//        {
//            _viewModel.PageTitle = "Kiểm tra thông tin khách hàng";
//            return View(_viewModel);
//        }

//        public IActionResult Details(string memID)
//        {
//            CustomerInformationViewModel _detailViewModel = new CustomerInformationViewModel();
//            _detailViewModel.PageTitle = "Chi tiết thông tin khách hàng";
//            return View(_detailViewModel);
//        }

//        [HttpPost]
//        public IActionResult Search(CustomerInformationViewModel model)
//        {
//            return RedirectToAction("Index");
//        }

//        [HttpPost]
//        public IActionResult Edit(CustomerInformationViewModel model)
//        {
//            return RedirectToAction("Index");
//        }
//    }
//}
