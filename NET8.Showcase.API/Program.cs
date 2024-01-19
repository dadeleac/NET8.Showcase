using Microsoft.EntityFrameworkCore;
using NET8.Showcase.API.Middlewares;
using NET8.Showcase.Application.Services;
using NET8.Showcase.Infrastructure.Context;
using NET8.Showcase.Infrastructure.Repositories;
using NET8.Showcase.Services.Contracts.IRepositories;
using NET8.Showcase.Services.Contracts.IServices;
using NLog;

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    builder.Services.AddDbContext<ShowcaseContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddApplicationInsightsTelemetry();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.UseMiddleware<LogMiddleware>();
    app.UseMiddleware<NLogMiddleware>();

    app.Run();




