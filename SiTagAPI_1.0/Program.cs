using SiTagAPI_1._0.Models;
using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SiTagAPI_1._0.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the DBContext
builder.Services.AddDbContext<SitagDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthServices,AuthServices>();
builder.Services.AddScoped<IFarmServices,FarmServices>();
builder.Services.AddScoped<IDivisionServices,FarmDivisionServices>();
builder.Services.AddScoped<IAnimalServices,AnimalServices>();
builder.Services.AddScoped<IDataServices,AnimalDataServices>();
builder.Services.AddScoped<IUserServices,UserServices>();
builder.Services.AddScoped<IMedicalService, MedicalServiceServices>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
