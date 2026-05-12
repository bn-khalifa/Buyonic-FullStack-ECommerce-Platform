using Buyonic.DAL;
using Buyonic.BLL.DTOs.Sellerdto;
using Buyonic.BLL.Mappers;
namespace Buyonic.BLL.Managers.SellerMng
{
    public class SellerManager : ISellerManager
    {
        private readonly IUnitOfWork _uniteOfWork;
        public SellerManager(IUnitOfWork unitOfWork) => _uniteOfWork = unitOfWork;

        public async Task<IEnumerable<SellerDTO>> GetSellersAsync()
        {
            var sellers = await _uniteOfWork.SellerRepository.GetAllSellersAsync();
            return sellers.Select(SellerDTOsMappers.SellerDtoMapper);
        }
        public async Task<IEnumerable<SellerWithProductsDTO>> GetSellersWithProductsAsync()
        {
            var sellers = await _uniteOfWork.SellerRepository.GetAllSellersWithProductsAsync();
            return sellers.Select(SellerDTOsMappers.SellerWithProdDtoMapper);
        }

        public async Task<SellerDTO?> GetSellerByIdAsync(int id)
        {
            var seller = await _uniteOfWork.SellerRepository.GetSellerByIdAsync(id);
            if (seller == null) return null;
            return SellerDTOsMappers.SellerDtoMapper(seller);
        }

        public async Task<SellerDTO?> GetSellerByEmailAsync(string email)
        {
            var seller = await _uniteOfWork.SellerRepository.GetSellerByEmailAsync(email);
            if (seller == null) return null;
            return SellerDTOsMappers.SellerDtoMapper(seller);
        }

        public async Task<SellerWithProductsDTO?> GetSellerByIdWithProductsAsync(int id)
        {
            var sellers = await _uniteOfWork.SellerRepository.GetAllSellersWithProductsAsync();
            var seller = sellers.FirstOrDefault(s => s.Id == id);
            if (seller == null) return null;
            return SellerDTOsMappers.SellerWithProdDtoMapper(seller);
        }

        public async Task<SellerWithProductsDTO?> GetSellerByEmailWithProductsAsync(string email)
        {
            var sellers = await _uniteOfWork.SellerRepository.GetAllSellersWithProductsAsync();
            var seller = sellers.FirstOrDefault(s => s.User.Email == email);
            if (seller == null) return null;

            return SellerDTOsMappers.SellerWithProdDtoMapper(seller);
        }


        public async Task AddSellerAsync(Seller seller)
        {
            _uniteOfWork.SellerRepository.Add(seller);
            await _uniteOfWork.SaveAsync();
        }

        public async Task UpdateSellerAsync(Seller seller)
        {
            _uniteOfWork.SellerRepository.Update(seller);
            await _uniteOfWork.SaveAsync();
        }

        public async Task DeleteSellerAsync(int id)
        {
            var seller = await _uniteOfWork.SellerRepository.GetByIdAsync(id);
            if (seller == null) return;
            _uniteOfWork.SellerRepository.Delete(seller);
            await _uniteOfWork.SaveAsync();
        }
        
    }
}