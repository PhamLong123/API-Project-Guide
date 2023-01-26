using CRMManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSAServices.Authentication;
using SmartBreadcrumbs.Attributes;

namespace CRMManager.Controllers
{
    public class MyAccountController : Controller
    {
        MSAAuthenticationService _authenticationService;
        public MyAccountController(MSAAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [Breadcrumb("Tài khoản của tôi")]
        public IActionResult Index(int ID)
        {
            AccountViewModel data = new AccountViewModel();
            data = data.GetData();
            data.PageTitle = "Tài khoản của tôi";
            return View(data);
        }

        [HttpPost]
        public IActionResult Index(AccountViewModel model)
        {
            AccountViewModel data = new AccountViewModel();
            data = model;
            CMSUser demoUser = new CMSUser()
            {
                UserID = "admin",
                BranchID = 1,
                LoginDate = System.DateTime.Now,
                SessionID = System.Guid.NewGuid()


            };
            //_authenticationService.SetLogin(demoUser);

            HttpContext.Session.SetString("UserID", demoUser.UserID.ToString());

            return RedirectToAction("MyAccount", 1);
        }
    }
}
