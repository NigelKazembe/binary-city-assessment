using bcity_assessment;
using bcity_assessment.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


var connectionString = builder.Configuration.GetConnectionString("ClientContactsDatabase");
builder.Services.AddDbContext<BcityAssessmentContext>(options => options.UseMySql(connectionString, new MySqlServerVersion("8.0.41")));

builder.Services.AddScoped<ClientCodeService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<ContactService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();