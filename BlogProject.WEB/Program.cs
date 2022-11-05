using BlogProject.DAL.Context;
using BlogProject.DAL.Repositories.Concrete;
using BlogProject.DAL.Repositories.Interfaces.Concrete;
using BlogProject.WEB.Models.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database baðlantý yolu tanýmlanýr.
builder.Services.AddDbContext<ProjectContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseLazyLoadingProxies(true);
});

// Identity kütüphanesini kullanmak için
builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ProjectContext>();

// Kimlik doðrulama için
builder.Services.AddAuthentication();

// IOC pattern deseni CORE için kullanýlýr. AddScope denerek bu durum tanýmlanýr. Yani kodu yazarken interface olarak yazdýðýmýz halde kendisi repository sýnýfýnda gövdeleri olan metotlarý tanýyacaktýr.
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IUserFollowedCategoryRepository, UserFollowedCategoryRepository>();
builder.Services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();

// AutoMapper sýnýfýnýn komutlarýnýn kullanýlmasý için komutlar yazýlýr.
builder.Services.AddAutoMapper(typeof(Mapping));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

// Kimlik doðrulama için
app.UseAuthentication();

// Yetkilendirme
app.UseAuthorization();

// Arealarýn rotasý yani yollarý belirtilir. Burada önemli nokta önce areanýn yolu kontrol edilmesi için default route komutunun üstüne yazýlmalýdýr.
app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
