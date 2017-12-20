﻿using BusinessLayer;
using BusinessLayer.Repositories;
using BusinessLayer.Services;
using Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IDatabaseContext, DatabaseContext>();
            services.AddTransient<ITransportMeanRepository, TransportMeanRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();


            services.AddMvc(options => options.Filters.Add(new ValidateModelStateFilter()));

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "My User API", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Too V1"); });

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}