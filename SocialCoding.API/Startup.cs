using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SocialCoding.API.Data;
using SocialCoding.API.Data.IRepositorio;
using SocialCoding.API.Data.LogicaNegocios;
using SocialCoding.API.Helpers;

namespace SocialCoding.API {
    public class Startup {
        public Startup (IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<SocialCodingContext> (
                x => x.UseSqlite (
                    Configuration.GetConnectionString ("ConexionDb")));

            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);
            services.AddCors ();

            #region LogicaNegocios
            // AddScoped --> El servicio es creado una vez por petición
            services.AddScoped<IAuth, Auth> ();
            #endregion

            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (op => {
                    op.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey (Encoding.ASCII
                    .GetBytes (Configuration.GetSection ("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseExceptionHandler (builder =>
                    builder.Run (async contexto => {
                        contexto.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                        var error = contexto.Features.Get<IExceptionHandlerFeature> ();

                        if (error != null) {
                            contexto.Response.AgregarError(error.Error.Message);
                            await contexto.Response.WriteAsync (error.Error.Message);
                        }
                    })
                );
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseCors (x => x.AllowAnyOrigin ().AllowAnyMethod ().AllowAnyHeader ());
            app.UseAuthentication ();
            app.UseMvc ();
        }
    }
}