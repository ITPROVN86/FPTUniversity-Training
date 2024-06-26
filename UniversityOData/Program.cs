
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using UniversityBusiness;

namespace UniversityOData
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Specialization>("Specializations");
            modelBuilder.EntitySet<Student>("Students"); 
            modelBuilder.EntityType<Student>().HasRequired(s=>s.Specialization);

            builder.Services.AddScoped(typeof(UniversityDbContext));

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddControllers().AddOData(
                o=>o.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents("odata",modelBuilder.GetEdmModel())
                );
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseODataBatching();
            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
