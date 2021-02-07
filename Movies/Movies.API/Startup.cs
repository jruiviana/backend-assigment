using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movies.Core;
using Movies.Core.Interfaces;
using Movies.Feature;
using Movies.Feature.Services;
using MediatR;
using Movies.API.Middleware;
using Movies.Core.Models;
using Microsoft.Extensions.Options;
using Movies.Feature.Administration.Services;
using AutoMapper;
using Movies.API.Authentication;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Movies.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SearchesDatabaseSettings>(
                     Configuration.GetSection(nameof(SearchesDatabaseSettings)));
            services.Configure<MovieSources>(
                     Configuration.GetSection(nameof(MovieSources)));

            // DI
            services.AddSingleton<ISearchesDatabaseSettings>(sp =>
               sp.GetRequiredService<IOptions<SearchesDatabaseSettings>>().Value);
            services.AddSingleton<IMovieSources>(sp =>
               sp.GetRequiredService<IOptions<MovieSources>>().Value);
            services.AddTransient<ISourceMovieService, SourceMovieService>();
            services.AddTransient<IHttpHandler, HttpClientService>();
            services.AddSingleton<IMongoDBService, MongoDBService>();

            // MediatR
            services.AddMediatR(typeof(FeaturesModule).Assembly, typeof(CoreModule).Assembly);

            services.AddControllers();

            services.AddSwaggerGen();

            // Auto-mapping
            services.AddAutoMapper(typeof(CoreModule));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            }).AddApiKeySupport(options => { });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            // Middleware
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movies V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
