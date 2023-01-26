using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMManager.Controllers
{
    public class PolicyController : Controller
    {
        public PolicyController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }

        #region MemberClass
        public IActionResult MemberClass()
        {
            return View();
        }
        #endregion
    }
}
