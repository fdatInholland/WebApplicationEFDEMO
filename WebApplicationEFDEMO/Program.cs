using DAL;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;
using System.Text.Json.Serialization;

namespace WebApplicationEFDEMO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddScoped<ICustomerService, CustomerService>();


            builder.Services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

            var app = builder.Build();

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
