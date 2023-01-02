using DatabaseAccess.FieldOfStudy;
using DatabaseAccess.FormularDbAccess;
using DatabaseAccess.Formulars;
using DatabaseAccess.Generic;
using DatabaseAccess.Institution;
using DatabaseAccess.Question;
using DatabaseAccess.QuestionAnswer;
using DatabaseAccess.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using UCLStudievejlederApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddMemoryCache();

builder.Services.AddScoped<GenericSql, GenericSql>();
builder.Services.AddScoped<FieldOfStudyDb, FieldOfStudyDb>();
builder.Services.AddScoped<InstitutionDb, InstitutionDb>();
builder.Services.AddScoped<QuestionDb, QuestionDb>();
builder.Services.AddScoped<QuestionAnswerDb, QuestionAnswerDb>();
builder.Services.AddScoped<AnswerOptionDb, AnswerOptionDb>();
builder.Services.AddScoped<UserDb, UserDb>();
builder.Services.AddScoped<FormularDb, FormularDb>();
builder.Services.AddScoped<FormularDbAccess, FormularDbAccess>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Formular}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
