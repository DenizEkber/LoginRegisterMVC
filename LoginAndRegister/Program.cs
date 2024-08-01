using FluentValidation.AspNetCore;
using LoginAndRegister.Validation.FluentValidator;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
       .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginViewModelValidator>())
       .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PasswordResetViewModelValidator>())
       .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterViewModelValidator>())
       .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SetPasswordViewModelValidator>())
       .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<VerifyCodeViewModelValidator>());

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(14); // Çerezin süresi
        options.LoginPath = "/Login/Login"; // Giri? sayfas? yolu
        options.AccessDeniedPath = "/Home/AccessDenied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Authentication middleware
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

// Di?er route tan?mlamalar?n? gerekti?i gibi ekleyebilirsiniz
/*
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Register}/{action=Register}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
*/

app.Run();
