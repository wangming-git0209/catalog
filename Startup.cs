using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CATALOG.Repositories;
using CATALOG.Repository;
using CATALOG.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace CATALOG
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
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
            

            // services.AddSingleton<IMongoClient>(serviceProvicer => {
            //     var settings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>(); 
            //     return new MongoClient(settings.ConnectionString);
               
            // });

            services.AddSingleton<IMongoClient>(serviceProvider => {
                var settings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                return new MongoClient(settings.ConnectionString); 
            });
            
            services.AddSingleton<IItemsRepository, MongoDbItemsRepository>();
            services.AddControllers(option => {
                option.SuppressAsyncSuffixInActionNames = false; 
            });
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CATALOG v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization(); // Add it here
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        
    }
}
