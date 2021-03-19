using Web.API.Classes;
using Web.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Web.API
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
            services.AddDbContext<ShopContext>(options => options.UseInMemoryDatabase("Shop"));
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options => 
                {
                    // options.SuppressModelStateInvalidFilter = true;
                }
                );

            /*services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:51959";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "hps-api";

                    options.TokenValidationParameters =
                    new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateAudience = false
                    };

                });*/

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("https://localhost:44375")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            services.AddApiVersioning(options => {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
            });

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();               
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            //app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>{endpoints.MapControllers();});

            app.UseSwagger();
            app.UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions) 
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());    
                    }
                }
               );
        }
    }

}

// Chame o swagger para ver as API's
// https://localhost:5001/swagger/index.htm 

//Criando docker image: docker build -t webapi:1.0 -f .dockerfile .
//container: docker run -it --rm -p 8081:80 webapi:1.0
//Dentro do container 
// https://localhost:8081/swagger/index.htm 

//Se for usar o POSTMAN, adicione o header : X-API-Version e a versão da API: 1.0 ou 2.0


