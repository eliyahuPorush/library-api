using System.Text.Json;
using Api.Middlewares;
using Application.Mapping;
using Dal;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Interfaces;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var config = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{environment}.json")
    .Build();
// declare services
builder.Services.AddLogging(builder => builder.AddConsole());
builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IAuthorsService, AuthorsService>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddDbContext<BookStoreDbContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("DB"));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
builder.Services.AddSwaggerGen();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionMiddleware>();
app.MapControllers();
app.UsePathBase(new PathString("/api"));
app.UseRouting();

app.Run();










