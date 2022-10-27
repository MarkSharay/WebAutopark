using Autopark.DAL.Entities;
using Autopark.DAL.Interfaces;
using Autopark.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMvc();
//builder.Services.AddTransient<IRepository<VehicleType>, VehicleTypeRepository>(provider => new VehicleTypeRepository(builder.Configuration.GetConnectionString("Default Connection")));
builder.Services.AddTransient<IRepository<Vehicle>, VehicleRepository>(provider => new VehicleRepository(builder.Configuration.GetConnectionString("Default Connection")));
builder.Services.AddTransient<IRepository<Order>, OrderRepository>(provider => new OrderRepository(builder.Configuration.GetConnectionString("Default Connection")));
builder.Services.AddTransient<IRepository<OrderItem>, OrderItemRepository>(provider => new OrderItemRepository(builder.Configuration.GetConnectionString("Default Connection")));
builder.Services.AddTransient<IRepository<Component>, ComponentRepository>(provider => new ComponentRepository(builder.Configuration.GetConnectionString("Default Connection")));
builder.Services.AddControllersWithViews();
var app = builder.Build();

VehicleTypeRepository typeRepository = new VehicleTypeRepository(builder.Configuration.GetConnectionString("DefaultConnection"));

typeRepository.Create(new VehicleType("Bus", 1.3));
app.MapGet("/", () => "Hello world");

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
