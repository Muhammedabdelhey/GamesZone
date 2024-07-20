using GameZone.Services;
var builder = WebApplication.CreateBuilder(args);

#region add dbContext
var connectionString = builder.Configuration.GetConnectionString("homeConnection") ?? throw new NullReferenceException("Connection String Not found");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Bind Services
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IPlatfromsService, PlatfromsService>();
builder.Services.AddScoped<IGamesService, GamesService>();
#endregion

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
