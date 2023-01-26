using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartBreadcrumbs.Attributes;
using System.Net.Mail;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [DefaultBreadcrumb("Dashboard")]
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            return View(model);
        }

        public IActionResult SendMail()
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("your_email_address@gmail.com");
            mail.To.Add("to_address");
            mail.Subject = "Test Mail";
            mail.Body = "This is for testing SMTP mail from GMAIL";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

            return View();
        }
    }
}
