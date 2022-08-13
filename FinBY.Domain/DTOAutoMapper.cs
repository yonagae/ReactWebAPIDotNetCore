using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain
{
    public  class DTOAutoMapper
    {
        /// <summary>
        /// Configures the AutoMapper to allow the easy convertion of Entities to the DTOs objects.
        /// Must be called by the projects that want to be able convert to DTOs
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAutoMapperServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DTOAutoMapper));
        }
    }
}
