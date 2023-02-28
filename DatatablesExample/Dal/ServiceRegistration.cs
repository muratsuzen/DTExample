using Dal.Abstracts;
using Dal.Concrete;
using Dal.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dal
{
    public static class ServiceRegistration
    {
        public static void DataAccessServiceRegistration(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("memorydb"));

            services.AddTransient<IProductRepository, ProductRepository>();
        }
    }
}
