using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Travel.Route.Api.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddVersionedApiExplorer(
             options =>
             {
                 options.GroupNameFormat = "'v'VVV";
                 options.SubstituteApiVersionInUrl = true;
             });

            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });

            var startupAssembly = Assembly.GetEntryAssembly();

            services.AddSwaggerGen(c =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    var assemblyDetails = startupAssembly.GetCustomAttribute<AssemblyProductAttribute>();

                    c.SwaggerDoc(description.GroupName, new OpenApiInfo()
                    {
                        Title = $"{assemblyDetails.Product} {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                        Description = "# CRUD - Rota de Viagem # API - Description"
                    });
                }

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Input the JWT like: Bearer {your token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        public static void UseSwaggerPage(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(sw =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    sw.SwaggerEndpoint($"./swagger/{description.GroupName}/swagger.json", $"Travel.Route.Api - {description.GroupName.ToUpperInvariant()}");
                }

                sw.RoutePrefix = string.Empty;
            });
        }
    }
}
