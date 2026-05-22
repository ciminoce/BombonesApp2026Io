using BombonesApp2026.Datos;
using BombonesApp2026.Datos.Interfaces;
using BombonesApp2026.Datos.Repositorios;
using BombonesApp2026.Entidades;
using BombonesApp2026.Servicios.Intefaces;
using BombonesApp2026.Servicios.Servicios;
using BombonesApp2026.Servicios.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BombonesApp2026.Ioc
{
    public static class DependencyInjectionContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddDbContext<BombonesDbContext>();

            services.AddScoped<IFormaDePagoRepositorio, FormaDePagoRepositorio>();
            services.AddScoped<IFormaDePagoServicio, FormaDePagoServicio>();
            services.AddScoped<IValidator<FormaDePago>, FormaDePagoValidator>();

            services.AddScoped<ITipoBombonRepositorio, TipoBombonRepositorio>();
            services.AddScoped<ITipoBombonServicio, TipoBombonServicio>();
            services.AddScoped<IValidator<TipoBombon>, TipoBombonValidator>();

            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IClienteServicio, ClienteServicio>();
            services.AddScoped<IValidator<Cliente>, ClienteValidator>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
