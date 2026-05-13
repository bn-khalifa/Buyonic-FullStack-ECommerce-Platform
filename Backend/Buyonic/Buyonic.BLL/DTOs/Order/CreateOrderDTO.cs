namespace Buyonic.BLL
{
    public class CreateOrderDTO
    {
        public int CustomerId { get; set; }
        public int PaymentMethodId { get; set; }
        public string ShippingAddress { get; set; }

        public List<CreateOrderItemDTO> OrderItems { get; set; }
    }
}
