using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Hogwarts.Api;


public class Startup
{
    public IConfiguration Configuration { get; set; }

    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    public void ConfigureServices (IServiceCollection services)
    {        
        services.AddAutoMapper(typeof(Startup));
        

        //In-Memory Database learn more https://marcionizzola.medium.com/utilizando-entity-framework-in-memory-no-c-9865b1183c84
        services.AddDbContext<ApplicationDbContext>(options=>options.UseInMemoryDatabase(Configuration.GetConnectionString("hogwarts")));

        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen( c=> 
            { 
            c.SwaggerDoc("v1", new(){ Title = "Hogwarts.Api", Version = "v1"});
            });

        // Enum Doc WebApiSwagger  learn more https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1269  
        services.AddControllersWithViews().AddJsonOptions(options=>options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


    }

    public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endppoints =>
        {
            endppoints.MapControllers();
        });
    }

            

}