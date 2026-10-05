using System.Security.Claims;
using System.Text;
using Infra;
using LinqToDB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Service.Security;

var builder = WebApplication.CreateBuilder(args);

var options = new DataOptions<MyDatabaseConnection>(
    new DataOptions().UseSQLite (" Data Source=../Infra/db.db"));


var jwtKey= builder.Configuration["Jwt:Key"] 
            ?? throw new InvalidOperationException("Jwt: Key is missing");

var jwtIssuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException("Jwt: Issuer is missing");

var jwtAudience = builder.Configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException("Jwt: Audience is missing");

var jwtExpiresMinutes = builder.Configuration.GetValue<int>(
    "Jwt:ExpiresMinutes",
    60);

//Jwt config validation(below)
//(should both above(Token service register) and below be  reformat ?,
//If yes where and naming)

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtIssuer,

        ValidateAudience = true,
        ValidAudience = jwtAudience,

        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),

        NameClaimType = ClaimTypes.Name,
        RoleClaimType = ClaimTypes.Role
    };
});

builder.Services.AddAuthorization();
builder.Services.AddScoped<MyDatabaseConnection>(_ => new MyDatabaseConnection(options));
builder.Services.AddScoped<Seeder>();

builder.Services.AddScoped<ITokenService>(_ => new JwtTokenService(
    jwtKey,
    jwtIssuer,
    jwtAudience,
    jwtExpiresMinutes));




builder.Services.AddScoped<MyDatabaseConnection>(_ =>
    new MyDatabaseConnection(options));

builder.Services.AddScoped<Seeder>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<IPasswordHasher,Argon2PasswordHasher>();
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();
builder.Services.AddCors();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<MyExceptionHandler>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetService<Seeder>();
    seeder.Seed();
}

app.UseExceptionHandler();
app.UseCors(config =>config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetIsOriginAllowed(_ => true));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseOpenApi();
app.UseSwaggerUi();
app.Run();