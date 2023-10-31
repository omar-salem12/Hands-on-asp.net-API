﻿using Contracts;
using EmployeeManagement.Repository;
using EmployeeManagement.Service;
using LoggerService;

namespace EmployeeManagement.Extentions
{
    public static class  ServiceExtentions
    {

        public static void ConfigureLoggerService(this IServiceCollection  service)
        {
            service.AddScoped<ILoggerManager, LoggerManager>();
        }


        public static void ConfigureRepositoryManager(this IServiceCollection service)
        {
            service.AddScoped<RepositoryManager, RepositoryManager>();
        }

        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager,ServiceManager>();
        }

    }
}
