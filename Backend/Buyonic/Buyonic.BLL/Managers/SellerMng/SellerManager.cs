using Buyonic.DAL;
namespace Buyonic.BLL
{
    public class SellerManager : ISellerManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public SellerManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SellerDTO>> GetSellersAsync()
        {
            var sellers = await _unitOfWork.SellerRepository.GetAllSellersAsync();

            return sellers.Select(SellerDTOsMappers.SellerDtoMapper);
        }

        public async Task<IEnumerable<SellerWithProductsDTO>> GetSellersWithProductsAsync()
        {
            var sellers = await _unitOfWork.SellerRepository.GetAllSellersWithProductsAsync();

            return sellers.Select(SellerDTOsMappers.SellerWithProdDtoMapper);
        }

        public async Task<SellerDTO?> GetSellerByIdAsync(int id)
        {
            var seller = await _unitOfWork.SellerRepository.GetSellerByIdAsync(id);

            if (seller == null)
                return null;

            return SellerDTOsMappers.SellerDtoMapper(seller);
        }

        public async Task<SellerDTO?> GetSellerByEmailAsync(string email)
        {
            var seller = await _unitOfWork.SellerRepository.GetSellerByEmailAsync(email);

            if (seller == null)
                return null;

            return SellerDTOsMappers.SellerDtoMapper(seller);
        }

        public async Task<SellerWithProductsDTO?> GetSellerByIdWithProductsAsync(int id)
        {
            var seller = await _unitOfWork.SellerRepository.GetSellerWithProductsByIdAsync(id);

            if (seller == null)
                return null;

            return SellerDTOsMappers.SellerWithProdDtoMapper(seller);
        }

        public async Task<SellerWithProductsDTO?> GetSellerByEmailWithProductsAsync(string email)
        {
            var seller = await _unitOfWork.SellerRepository.GetSellerWithProductsByEmailAsync(email);

            if (seller == null)
                return null;

            return SellerDTOsMappers.SellerWithProdDtoMapper(seller);
        }

        public async Task<SellerDTO> AddSellerAsync(CreateSellerDTO dto)
        {
            var seller = new Seller
            {
                StoreName = dto.StoreName,
                UserId = dto.UserId
            };

            _unitOfWork.SellerRepository.Add(seller);

            await _unitOfWork.SaveAsync();

            var createdSeller = await _unitOfWork.SellerRepository.GetSellerByIdAsync(seller.Id);

            return SellerDTOsMappers.SellerDtoMapper(createdSeller!);
        }

        public async Task<bool> UpdateSellerAsync(int id, UpdateSellerDTO dto)
        {
            var seller = await _unitOfWork.SellerRepository.GetByIdAsync(id);

            if (seller == null)
                return false;

            if (!string.IsNullOrWhiteSpace(dto.StoreName))
                seller.StoreName = dto.StoreName;

            if (dto.Rating.HasValue)
                seller.Rating = dto.Rating;

            _unitOfWork.SellerRepository.Update(seller);

            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteSellerAsync(int id)
        {
            var seller = await _unitOfWork.SellerRepository.GetByIdAsync(id);

            if (seller == null)
                return false;

            _unitOfWork.SellerRepository.Delete(seller);

            await _unitOfWork.SaveAsync();

            return true;
        }
    }
}