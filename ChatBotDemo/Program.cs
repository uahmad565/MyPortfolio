using ChatBotDemo.Data;
using ChatBotDemo.Middlewares;
using ChatBotDemo.Models.Helpers;
using ChatBotDemo.Models.Interfaces;
using ChatBotDemo.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.IOTimeout = TimeSpan.FromSeconds(30);
    options.Cookie.Name = "Chatbot.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Path = "/";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddScoped<IMemoryStorage, MemoryStorage>();

builder.Services.AddHttpClient<IChatbotService, ChatbotService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ChatbotServerUrl"]);
});

//Add Sql Server
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//Configuring Session Middleware in ASP.NET Core
app.UseSession();

//Custom Middlewares
// Make sure session is enabled first
app.UseMiddleware<SessionInitializerMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
    dbContext.Database.EnsureCreated();
    dbContext.Add(new Product() { Name = "usman_" + DateTime.Now.ToString(), Price = 25 });
    dbContext.SaveChanges();
}

app.Run();
