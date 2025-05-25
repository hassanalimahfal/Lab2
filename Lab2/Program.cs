using Lab2.Data;
using Lab2.Models;
using Lab2.Repositories.Implementations;
using Lab2.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<dbFirstLabContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("local")) );

builder.Services.AddIdentity<SysUser, IdentityRole>(
 //‰—”·Â «»‘‰ ‰ﬁ·Â «·«Ìœ‰  Ì «Ì‘ »« ﬂÊ‰ 
 Options =>
 {
     //«Ã»«—Ì Ê„«Ì ﬂ—— 
     Options.Password.RequireUppercase = false;
     //ﬂ„ ÕÃ„ «·»«”Ê—œ ÿÊ·Â —ﬁ„ 
     Options.Password.RequiredLength = 5;
     //Â· ÷—Ê—Ì —ﬁ„ 
     Options.Password.RequireDigit = false;
     //ﬂ„ ⁄œœ «·Õ—Ê› «·Ì „«  ﬂ—— ›Ì «·»«”Ê—œ 
     Options.Password.RequiredUniqueChars = 0;
     //
     Options.Password.RequireLowercase= false;
     Options.Password.RequireLowercase = false;
     Options.Password.RequireNonAlphanumeric = false;



 }


    ).AddEntityFrameworkStores<dbFirstLabContext>();
//============== Repositories =======================
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=Index}/{id?}");

app.Run();
