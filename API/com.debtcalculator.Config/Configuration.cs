using com.debtcalculator.Services.Notification.Email;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace com.debtcalculator.Config
{
    public static class Configuration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            registerMediatr(services);
            registerData(services);
            registerObjects(services);
            registerAppServices(services);
        }

        private static void registerMediatr(IServiceCollection services)
        {
            const string applicationAssemblyName = "com.debtcalculator.Domain";
            var assembly = System.AppDomain.CurrentDomain.Load(applicationAssemblyName);

            FluentValidation.AssemblyScanner
                .FindValidatorsInAssembly(assembly)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddScoped(typeof(MediatR.IPipelineBehavior<,>), typeof(Domain.Mediator.FailFastRequestBehavior<,>));

            services.AddMediatR(assembly);
        }


        private static void registerData(IServiceCollection services)
        {
            services.AddScoped<Data.EF.DebtCalculatorDataContext>();
            services.AddTransient<Domain.Contracts.Infra.IUnitOfWork, Data.EF.UnitOfWorkEF>();

            services.AddTransient<Domain.Contracts.Repositories.IUserReadRepository, Data.EF.Repositories.UserReadRepository>();
            services.AddTransient<Domain.Contracts.Repositories.IUserWriteRepository, Data.EF.Repositories.UserWriteRepository>();

            services.AddTransient<Domain.Contracts.Repositories.IDebtReadRepository, Data.EF.Repositories.DebtReadRepository>();
            services.AddTransient<Domain.Contracts.Repositories.IDebtWriteRepository, Data.EF.Repositories.DebtWriteRepository>();
        }

        private static void registerObjects(IServiceCollection services)
        {
            services.AddScoped<Domain.DTOs.DadosSessaoDTO>();
        }

        private static void registerAppServices(IServiceCollection services)
        {
            services.AddTransient<Domain.Contracts.Infra.Services.ISendEmailService, SendEmailService>();
        }
    }
}