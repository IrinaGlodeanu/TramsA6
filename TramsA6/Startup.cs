using System;
using System.Text;
using AutoMapper;
using BusinessLayer;
using BusinessLayer.Repositories;
using BusinessLayer.Services;
using Domain.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence.PersistenceFolder;
using Swashbuckle.AspNetCore.Swagger;
using TramsA6.Filters;

namespace TramsA6
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("UsersConnectionString");
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection));


            services.AddAutoMapper();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IDatabaseContext, DatabaseContext>();
            services.AddTransient<ITransportMeanRepository, TransportMeanRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();


            services.AddMvc(options => options.Filters.Add(new ValidateModelStateFilter()));

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "My User API", Version = "v1"}); });

            services.AddAuthentication(opts =>
                {
                    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                // Add Jwt token support
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        // standard configuration
                        ValidIssuer = Configuration["Auth:Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"])),
                        ValidAudience = Configuration["Auth:Jwt:Audience"],
                        ClockSkew = TimeSpan.Zero,

                        // security switches
                        RequireExpirationTime = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = true
                    };
                    cfg.IncludeErrorDetails = true;
                });


            services.AddAntiforgery(options => { options.HeaderName = "X-XSRF-TOKEN"; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAntiforgery antiforgery)
        {
            //Manually handle setting XSRF cookie. Needed because HttpOnly has to be set to false so that
            //Angular is able to read/access the cookie.
            app.Use((context, next) =>
            {
                if (context.Request.Method == HttpMethods.Get &&
                    (string.Equals(context.Request.Path.Value, "/", StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(context.Request.Path.Value, "/home/index", StringComparison.OrdinalIgnoreCase)))
                {
                    var tokens = antiforgery.GetAndStoreTokens(context);
                    context.Response.Cookies.Append("XSRF-TOKEN",
                        tokens.RequestToken,
                        new CookieOptions {HttpOnly = false});
                }

                return next();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Too V1"); });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}