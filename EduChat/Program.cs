using EduChat.hubs;
using EduChat.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
using MySql.Data.MySqlClient;
using Tewr.Blazor.FileReader;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Blazored.Toast;



var builder = WebApplication.CreateBuilder(args);
string connectionString = "Server=localhost;Database=EduChat;User=root;Password=M3g5t1l4c05t3;Port=3306;" ;
//"Server=localhost;Database=educhat;Uid=root;Pwd=Jadzia123!;Port=3306;";
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<MySqlConnection>(_ => new MySqlConnection(connectionString));
builder.Services.AddSingleton<UserService>();

builder.Services.AddScoped<UserService>();
builder.Services.AddBlazoredToast();

builder.Services.AddFileReaderService(options => options.InitializeOnFirstCall = true);
builder.Services.AddMudServices();

builder.Services.AddBlazorise(options => { })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

builder.Services.AddScoped<ChatService>();


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

app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub();
    endpoints.MapHub<ChatHub>("/chatHub");
    endpoints.MapFallbackToPage("/_Host");
});
//app.MapFallbackToPage("/_Host");

app.Run();
