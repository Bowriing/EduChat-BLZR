using EduChat.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);
string connectionString = "Server=localhost;Database=EduChat;User=root;Password=M3g5t1l4c05t3;Port=3306;" ;
//"Server=localhost;Database=educhat;Uid=root;Pwd=Jadzia123!;Port=3306;";
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<MySqlConnection>(_ => new MySqlConnection(connectionString));
builder.Services.AddSingleton<UserService>();
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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
