using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NsSchool.Business.Managers;
using NsSchool.Business.Services;
using NsSchool.Data.Context;
using NsSchool.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<NsSchoolDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));
builder.Services.AddScoped<IPersonService, PersonManager>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementManager>();
builder.Services.AddScoped<IClassRoomService, ClassRoomManager>();
builder.Services.AddScoped<IStudentService, StudentManager>();
builder.Services.AddScoped<IParentService, ParentManager>();
builder.Services.AddScoped<ITeacherService, TeacherManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IClassRoomTeacherService, ClassRoomTeacherManager>();
builder.Services.AddScoped<IClassRoomStudentService, ClassRoomStudentManager>();
builder.Services.AddScoped<ICartServiceDetail, CartDeteailManager>();

builder.Services.AddDataProtection(); // Þifreleme yapýlacak.

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
	options.LoginPath = new PathString("/Dash/Index"); // oturum açýnca ana sayfaya at
	options.LogoutPath = new PathString("/Auth/Index"); // oturum kapatýlýnca ana sayfaya at
	options.AccessDeniedPath = new PathString("/Home/Index"); // yetkim olmayan bir sayfaya gitmek istiyorsam , ana sayfaya at.
});



var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "areas",
	pattern: ("{area:exists}/{controller=Dashboard}/{action=Index}/{id?}")
	);

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=home}/{action=index}/{id?}"
	);
app.UseStaticFiles();

app.UseStatusCodePagesWithReExecute("/Error/Error", "?code={0}");

app.Run();
