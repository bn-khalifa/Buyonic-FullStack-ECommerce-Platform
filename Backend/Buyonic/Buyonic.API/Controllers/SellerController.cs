using Buyonic.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Buyonic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SellerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/seller
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sellers = await _unitOfWork.SellerRepository
                .GetAllSellersWithProductsAsync();

            return Ok(sellers);
        }

        // GET: api/seller/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var seller = await _unitOfWork.SellerRepository
                .GetSellerByIdAsync(id);

            if (seller == null)
                return NotFound();

            return Ok(seller);
        }

        // GET: api/seller/by-name/store1
        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetByStoreName(string name)
        {
            var seller = await _unitOfWork.SellerRepository
                .GetSellerByStoreNameAsync(name);

            if (seller == null)
                return NotFound();

            return Ok(seller);
        }

        // POST: api/seller
        [HttpPost]
        public async Task<IActionResult> Create(Seller seller)
        {
            _unitOfWork.SellerRepository.Add(seller);

            await _unitOfWork.SaveAsync();

            return Ok(seller);
        }

        // PUT: api/seller/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Seller seller)
        {
            if (id != seller.Id)
                return BadRequest();

            _unitOfWork.SellerRepository.Update(seller);

            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // DELETE: api/seller/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var seller = await _unitOfWork.SellerRepository
                .GetByIdAsync(id);

            if (seller == null)
                return NotFound();

            _unitOfWork.SellerRepository.Delete(seller);

            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}