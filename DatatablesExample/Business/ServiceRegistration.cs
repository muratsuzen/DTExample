using Business.Abstracts;
using Business.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public static class ServiceRegistration
    {
        public static void BusinessServiceRegistration(this IServiceCollection services)
        {             
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<IProductService,ProductService>();

        }
    }
}
