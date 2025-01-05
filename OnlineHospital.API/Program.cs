using CommonLibrary.Extensions;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using OnlineHospital.DB.Model;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityExt();

builder.Services.ConfigureApplicationCookie(options =>
{

    var cookieBuilder = new CookieBuilder();
    cookieBuilder.Name = "OnlineHospitalAppCookie";
    options.LoginPath = new PathString("/Home/SignIn");
    options.LogoutPath = new PathString("/Member/LogOut");
    options.AccessDeniedPath = new PathString("/Member/AccessDenied");
    options.Cookie = cookieBuilder;
    options.ExpireTimeSpan = TimeSpan.FromDays(60);
    options.SlidingExpiration = true;
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


var app = builder.Build();
//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
//    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

//    // Admin rol� ve kullan�c� olu�turma i�lemleri burada yap�l�r
//    string roleName = "Admin";
//    string adminEmail = "canncizmeci@gmail.com";


//    if (!await roleManager.RoleExistsAsync(roleName))
//    {
//        var roleResult = await roleManager.CreateAsync(new AppRole { Name = roleName });
//        if (!roleResult.Succeeded)
//        {
//            Console.WriteLine($"Rol olu�turulamad�: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
//            return;
//        }
//    }

//    var user = await userManager.FindByEmailAsync(adminEmail);


//    if (!await userManager.IsInRoleAsync(user!, roleName))
//    {
//        var addToRoleResult = await userManager.AddToRoleAsync(user!, roleName);
//        if (!addToRoleResult.Succeeded)
//        {
//            Console.WriteLine($"Rol atanamad�: {string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))}");
//        }
//    }

//    Console.WriteLine($"'{adminEmail}' kullan�c�s� '{roleName}' rol�ne ba�ar�yla atand�.");
//}


app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
