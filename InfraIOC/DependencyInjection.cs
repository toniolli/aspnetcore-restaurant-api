using Application.Interfaces;
using Application.Mapping;
using Application.Services;
using AutoMapper;
using Domain.Interfaces;
using Infra.Context;
using Infra.Identity;
using Infra.Repositores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraIOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("POSTGRES");

            // DATABASE
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            // REPOSITORIES
            services.AddScoped<ImesaRepository, MesaRepository>();
            services.AddScoped<IcomandaRepository, ComandaRepository>();
            services.AddScoped<IpedidoRepository, PedidoRepository>();
            services.AddScoped<IitemPedidoRepository, ItemPedidoRepository>();
            services.AddScoped<IcardapioRepository, CardapioRepository>();
            services.AddScoped<IitemCardapioRepository, ItemCardapioRepository>();
            services.AddScoped<IsetorProducaoRepository, SetorProducaoRepository>();
            services.AddScoped<IperfilRepository, PerfilRepository>();
            services.AddScoped<IpermissaoRepository, PermissaoRepository>();
            services.AddScoped<IperfilPermissaoRepository, PerfilPermissaoRepository>();
            services.AddScoped<IusuarioPerfilRepository, UsuarioPerfilRepository>();

            // SERVICES
            services.AddScoped<ImesaService, MesaService>();
            services.AddScoped<IcomandaService, ComandaService>();
            services.AddScoped<IpedidoService, PedidoService>();
            services.AddScoped<IitemPedidoService, ItemPedidoService>();
            services.AddScoped<IcardapioService, CardapioService>();
            services.AddScoped<IitemCardapioService, ItemCardapioService>();
            services.AddScoped<IsetorProducaoService, SetorProducaoService>();
            services.AddScoped<IusuarioService, UsuarioService>();
            services.AddScoped<IPerfilService, PerfilService>();
            services.AddScoped<IPermissaoService, PermissaoService>();
            services.AddScoped<IperfilPermissaoService, PerfilPermissaoService>();
            services.AddScoped<IUsuarioPerfilService, UsuarioPerfilService>();

            // AUTOMAPPER
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DomainToDTOMappingProfile>();
            });

            return services;
        }
    }

}
