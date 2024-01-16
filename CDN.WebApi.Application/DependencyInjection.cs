using CDN.WebApi.Application.Repository;
using CDN.WebApi.Infra.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDN.WebApi.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration config)
        {
            //to configure dbContext
            service.AddSqlServer<freelancersContext>(config.GetConnectionString("DefaultConnection"));

           // register service in dependency injection
            service.AddTransient<IFreelancerService, FreelancerService>();
            return service;
        }

    }
}
