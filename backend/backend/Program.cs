using System.Text.Json.Serialization;
using backend.Core.AuthMapperConfig;
using backend.Core.Context;
using backend.Services.Company;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddSwaggerGen();


    // db configuration
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        var connectionUrl = builder.Configuration.GetConnectionString("DbConnectionUrl")!;
        options.UseSqlServer(connectionUrl);
    });

// scopes
builder.Services.AddScoped<ICompanyService, CompanyService>();

// automapper 
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
   
}

app.MapControllers();

app.UseHttpsRedirection();
app.Run();

