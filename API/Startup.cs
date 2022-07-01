using API.Extensions;
using API.Middleware;
using API.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
            StaticConfig = config;
        }

        public IConfiguration Configuration { get; }
        public static IConfiguration StaticConfig { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices(_config);
            services.AddCors();
            services.AddIdentityServices(_config);
            services.AddControllers();
            services.AddSignalR();
            services.AddSwaggerDocumentation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExeptionMiddleware>();
            app.UseStaticFiles();
            app.UseRouting();
           //  app.UseHttpsRedirection();
            app.UseCors(policy =>
            policy.AllowAnyHeader().AllowCredentials().AllowAnyMethod().WithOrigins("http://localhost:4200"));
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseDefaultFiles();
            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Fallback");
                endpoints.MapHub<PresenceHub>("hubs/presence");
                endpoints.MapHub<DashboardHub>("hubs/dashboard");
                // endpoints.MapHub<MessageHub>("hubs/message");
            });
        }
    }
}
