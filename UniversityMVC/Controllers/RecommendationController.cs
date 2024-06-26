using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using UniversityMVC.Models;

namespace UniversityMVC.Controllers
{
    public class RecommendationController : Controller
    {
        private readonly MLContext mlContext;
        private readonly ITransformer model;

        public RecommendationController()
        {
            mlContext = new MLContext();
            model = TrainModel();
        }

        private ITransformer TrainModel()
        {
            var data = mlContext.Data.LoadFromTextFile<ProductRating>("data.csv", separatorChar: ',', hasHeader: true);

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = nameof(ProductRating.userId),
                MatrixRowIndexColumnName = nameof(ProductRating.productId),
                LabelColumnName = nameof(ProductRating.Label),
                NumberOfIterations = 20,//Thiết lập số lượng vòng lặp (iterations) mà thuật toán sẽ thực hiện trong quá trình huấn luyện. NumberOfIterations xác định số lần thuật toán sẽ lặp lại quá trình tối ưu hóa. Số lần lặp lớn hơn có thể cải thiện độ chính xác của mô hình, nhưng cũng làm tăng thời gian huấn luyện.
                ApproximationRank = 100
                //ApproximationRank xác định số lượng yếu tố tiềm ẩn (latent factors) được sử dụng để phân tích ma trận. Số lượng yếu tố tiềm ẩn cao hơn có thể giúp mô hình nắm bắt được nhiều cấu trúc phức tạp hơn trong dữ liệu, nhưng cũng làm tăng độ phức tạp và thời gian tính toán.
            };

            var pipeline = mlContext.Transforms.Conversion.MapValueToKey(nameof(ProductRating.userId))
                .Append(mlContext.Transforms.Conversion.MapValueToKey(nameof(ProductRating.productId)))
                .Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

            var model = pipeline.Fit(data);
            return model;
        }

        public IActionResult Recommend(uint userId)
        {
            var predictionEngine = mlContext.Model.CreatePredictionEngine<ProductRating, ProductRatingPrediction>(model);
            var recommendations = new List<ProductRatingPrediction>();

            for (uint i = 101; i <= 106; i++) // Giả sử bạn có productId từ 101 đến 106
            {
                var prediction = predictionEngine.Predict(new ProductRating { userId = userId, productId = i });
                prediction.userId = userId; // Gán lại userId cho kết quả
                prediction.productId = i;   // Gán lại productId cho kết quả
                recommendations.Add(prediction);
            }

            return View(recommendations.OrderByDescending(r => r.Score).Take(5));
        }

    }
}
