using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.DTOS;
using Talabat.Apis.Errors;
using Talabat.Core.Services;

namespace Talabat.Apis.Controllers
{
    [Authorize]
    public class PaymentsController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(
            IPaymentService paymentService
            )
        {
            _paymentService = paymentService;
        }

        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreatOrUpdatePayment(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (basket is null) return BadRequest(new ApiResponse(400));
            
            return Ok(basket);
        }
    }
}
