using Api.BusinessLayer;
using Api.Interfaces;
using Api.Models;
using Api.Repositories;
using Helper.CorrelationId;
using Microsoft.OpenApi.Models;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Employee Benefit Cost Calculation Api",
                Description = "Api to support employee benefit cost calculations"
            });
        });

        var allowLocalhost = "allow localhost";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(allowLocalhost,
                policy => { policy.WithOrigins("http://localhost:3000", "http://localhost"); });
        });


        builder.Services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();


        builder.Services.AddSingleton<IEmployeeRepository<Employee>, EmployeeRepository>();
        builder.Services.AddSingleton<IDependentRepository<Dependent>, DependentRepository>();
        builder.Services.AddSingleton<IDataAccessLayer, DataAccessLayer>();
        builder.Services.AddScoped<IDeductions, Deductions>();
        builder.Services.AddScoped<IDependentsBusinessLayer, DependentBusinessLayer>();
        builder.Services.AddScoped<IEmployeeBusinessLayer, EmployeeBusinessLayer>();


        var app = builder.Build();

// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(allowLocalhost);

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseMiddleware<CorrelationIdMiddleware>();

        app.Run();
    }
}