namespace Buyonic.BLL
{
    public class ReviewEligibilityDTO
    {
        public bool CanReview { get; set; }
        public bool HasReviewed { get; set; }
        public string? Message { get; set; }
    }
}
