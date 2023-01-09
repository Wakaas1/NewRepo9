using Microsoft.AspNetCore.Authentication.Cookies;
using YSManagmentSystem.BLL.Email;
using YSManagmentSystem.BLL.Products;
using YSManagmentSystem.BLL.SuppliersService;
using YSManagmentSystem.BLL.Categories;
using YSManagmentSystem.BLL.User;
using YSManagmentSystem.BLL.UserRole;
using YSManagmentSystem.DAL.Data;
using YSManagmentSystem.BLL.OrderService;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(10);//You can set Time   
});
builder.Services.AddMvc();

// Start 

builder.Services.AddScoped<IDapperRepo, DapperRepo>();

builder.Services.AddScoped<ISalePersonsServices, SalePersonsServices>();
builder.Services.AddScoped<IDriverServices,DriverServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IRoleServices, RoleServices>();
builder.Services.AddScoped<IEmailServices, EmailServices>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<ISupplierServices, SupplierServices>();
builder.Services.AddScoped<IBrandServices, BrandServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ILocationServices, LocationServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<ICustomerServices, CustomerServices>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x => x.LoginPath = "/User/Login");
builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = false;
    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/User/Login");

});
// End
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

app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
//app.MapControllerRoute(
//    name: "customer",
//    pattern: "{controller=Customer}/{action=Index}/{id?}");

app.Run();
