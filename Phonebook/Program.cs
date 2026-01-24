using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Phonebook.Data;
using Phonebook.Services;

var builder = WebApplication.CreateBuilder(args);
//string connectionString = builder.Configuration.GetConnectionString("EmailSettings:ConnectionString");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<PhonebookContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PhonebookContext>()
    .AddDefaultTokenProviders();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddPageRoute("/Identity/Account/Register", "");
});
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddEmailClient(builder.Configuration.GetConnectionString("AzureConnectionString"));
});

builder.Services.AddTransient<IEmailSender, AzureEmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
