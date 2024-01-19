using Shifts.Infrastructure.Sheets;
using Shifts.Infrastructure.Calender;
using Shifts.Infrastructure.GoogleAuth;
using Shifts.Presentation.GraphQL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Application";
    options.DefaultSignInScheme = "External";
})
.AddCookie("Application")
.AddCookie("External") // Add this line
    .AddGoogle(o =>
    {
        o.ClientId = builder.Configuration["GoogleSheetsConfig:client_id"];
        o.ClientSecret = builder.Configuration["GoogleSheetsConfig:client_secret"];
    });

builder.Services.AddExcelService();
builder.Services.AddDb(builder.Configuration);
builder.Services.AddApplicationLogicServices();
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policyBuilder =>
        policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGoogleAuthServices(builder.Configuration);
builder.Services.AddGoogleSheets(builder.Configuration);
builder.Services.AddCalendarServices(builder.Configuration);
builder.Services.AddGraphQL();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.MapGraphQL();
app.Run();
