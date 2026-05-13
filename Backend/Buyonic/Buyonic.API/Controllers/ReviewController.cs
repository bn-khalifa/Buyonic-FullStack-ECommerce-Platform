using Buyonic.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Buyonic.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewManager _reviewManager;
        private readonly ICustomerManager _customerManager;

        public ReviewController(IReviewManager reviewManager, ICustomerManager customerManager)
        {
            _reviewManager = reviewManager;
            _customerManager = customerManager;
        }

        [HttpGet("product/{productId:int}")]
        public async Task<IActionResult> GetProductReviews([FromRoute] int productId)
        {
            var reviews = await _reviewManager.GetProductReviews(productId);
            return Ok(reviews);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> SubmitReview([FromBody] ProductReviewDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Get customer from token
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null) return Unauthorized();

            var customer = await _customerManager.GetCustomerByEmailAsync(email);
            if (customer == null) return Unauthorized();

            // Override customerId from token — never trust client-sent IDs
            dto.CustomerId = customer.Id;

            var result = await _reviewManager.SubmitReviewAsync(dto);
            if (!result)
                return Forbid(); // customer has no delivered order for this product

            return Ok("Review submitted successfully.");
        }
    }
}