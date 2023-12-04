using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using PopsicleFactory.Core.Interfaces;
using PopsicleFactory.Core.Interfaces.Repositories;
using PopsicleFactory.InMemoryRepository;

namespace PopsicleFactory.Api;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PopsicleFactory v1"));
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    public void ConfigureServices(IServiceCollection services)
    {

        services
            .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PopsicleFactory.Api", Version = "v1" });
                    c.SchemaFilter<EnumSchemaFilter>();
                }
            );

        services
            .AddSingleton<UnitOfWorkFactory, InMemoryUnitOfWorkFactory>()
            .AddSingleton<PopsicleRepository, PopsicleInMemoryRepository>();

        services.AddControllers(
                setupActions =>
                {
                    setupActions.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                    setupActions.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));
                    setupActions.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                }
            )
            .AddJsonOptions(opts =>
            {
                var enumConverter = new JsonStringEnumConverter();
                opts.JsonSerializerOptions.Converters.Add(enumConverter);
            });
    }

}