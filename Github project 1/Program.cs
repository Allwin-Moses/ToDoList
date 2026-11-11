using Github_project_1.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<DataAccess>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Todo/View1");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todo}/{action=View1}/{id?}");

app.Run();
