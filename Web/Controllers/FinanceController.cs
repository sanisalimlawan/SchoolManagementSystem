using Flutterwave.Net;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class FinanceController : Controller
    {
        private readonly FlutterwaveApi _flutterwaveApi;
        public FinanceController(FlutterwaveApi api)
        {
            _flutterwaveApi = api;
        }
        public async Task<IActionResult> Index()
        {
            var reuest = new             {
                tx_ref = "tx-123456789",
                amount = 100,
                currency = "USD",
                payment_type = "card",
                redirect_url = "https://yourwebsite.com/redirect"
            };

            //var value = await _flutterwaveApi.Payments.Initialize(reuest);
            //Banks
            return View();
        }
    }
}
