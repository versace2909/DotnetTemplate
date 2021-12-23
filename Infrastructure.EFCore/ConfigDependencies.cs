using Application.DataAccess;
using Application.Interfaces.Books;
using FluentValidation.AspNetCore;
using Infrastructure.EFCore.DataAccess;
using Infrastructure.EFCore.Implementations;
using Infrastructure.Mapper;
using Infrastructure.Validators.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EFCore;

public static class ConfigDependencies
{
    public static void RegisterEfInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddScoped<IBookServices, BookService>();

        service.AddAutoMapper(typeof(BookMappingProfile));

        service.AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<CreateBookDTOValidator>(); });
        service.AddDbContext<AppEfDbContext<long>>(cfg =>
        {
            // cfg.UseSqlServer(configuration.GetConnectionString("appConnectionString"));
            cfg.UseInMemoryDatabase("temp");
        });

        service.AddScoped<IUnitOfWork<long>, BaseUnitOfWork<long>>();
    }
}