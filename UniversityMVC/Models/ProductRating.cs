using Microsoft.ML.Data;

namespace UniversityMVC.Models
{
    public class ProductRating
    {
        [LoadColumn(0)]
        public uint userId { get; set; }

        [LoadColumn(1)]
        public uint productId { get; set; }

        [LoadColumn(2)]
        public float Label { get; set; }
    }
}
