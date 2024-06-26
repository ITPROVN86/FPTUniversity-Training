namespace UniversityMVC.Models
{
    public class ProductRatingPrediction
    {
        public float Score { get; set; }
        public uint userId { get; set; }
        public uint productId { get; set; }
    }
}
