using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class PaypalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Success()
        {
            // Paypal gọi hàm này khi thanh toán thành công, ghi nhận vào CSDL trạng thái đã thanh toán bằng paypal ở đây
            return View();
        }

        public IActionResult Cancel()
        {
            // Paypal gọi hàm này khi thanh toán bị hủy, ghi nhận vào CSDL trạng thái chưa thanh toán ở đây
            return View();
        }
    }
}
