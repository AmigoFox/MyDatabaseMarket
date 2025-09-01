var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы для MVC
builder.Services.AddControllersWithViews();

// Добавляем сервисы для Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Сначала Blazor маршруты
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Потом MVC маршруты
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();