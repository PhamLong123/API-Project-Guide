using AutoMapper;
using CRMLogic.Base.Common;
using CRMLogic.Mall.Thiso.Application;

using CRMManager.Helper.Logic.Authentication;
using CRMManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRMManager.Controllers
{
    public class LoginController : Controller
    {
        CRMAccountManager _authenticationService;
        IMapper _mapper;

        LoginViewModel _viewModel;
        BaseCRMManagerApp _crmManagerApp;


        public LoginController(CRMManagerApplication crmManagerApp, IMapper mapper,
            CRMAccountManager authenticationService)
        {

            _mapper = mapper;
            _crmManagerApp = crmManagerApp;
            _authenticationService = authenticationService;

        }
        public IActionResult Index()
        {
            LoginViewModel _viewModel = new LoginViewModel();
            _viewModel.AppID = _crmManagerApp.AppID;
            _viewModel.BranchID = _crmManagerApp.BranchID;
            _viewModel.PageTitle = "Đăng nhập";
            return View(_viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Pwd))
            {
                ModelState.AddModelError("DisplayError", "Vui lòng nhập thông tin tài khoản");
                return View(model);
            }
            else
            {
                int branchID = model.BranchID;
                int appID = _crmManagerApp.AppID;
                model.AppID = appID;
                model.BranchID = branchID;

                LoginUser loginUser = await _authenticationService.Login(appID, model.UserName, model.Pwd);

                if (loginUser != null)
                {
                    HttpContext.Session.SetString("UserID", model.UserName);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(model);
                }
            }
        }
    }
}
