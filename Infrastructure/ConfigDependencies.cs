using Application.Interfaces.Books;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Infrastructure.Implementations;
using Infrastructure.Mapper;
using Infrastructure.Validators.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigDependencies
{
    public static void RegisterInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddScoped<IBookServices, BookServices>();

        service.AddAutoMapper(typeof(BookMappingProfile));
        
        service.AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<CreateBookDTOValidator>(); });
        service.AddDbContext<AppDbContext>(cfg =>
        {
            cfg.UseSqlServer(configuration.GetConnectionString("appConnectionString"));
            // cfg.UseInMemoryDatabase("temp");
        });
    }
}