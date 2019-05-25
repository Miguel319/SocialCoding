using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialCoding.API.Data;
using SocialCoding.API.Data.IRepositorio;
using SocialCoding.API.Data.LogicaNegocios;

namespace SocialCoding.API {
    public class Startup {
        public Startup (IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) 
        {
            services.AddDbContext<SocialCodingContext> (
                x => x.UseSqlite (
                    Configuration.GetConnectionString ("ConexionDb")));

            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);
            services.AddCors ();

            #region LogicaNegocios
                 // AddScoped --> El servicio es creado una vez por petición
                 services.AddScoped<IAuth, Auth> ();

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) 
        {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseCors (x => x.AllowAnyOrigin ().AllowAnyMethod ().AllowAnyHeader ());
            app.UseMvc ();
        }
    }
}