using Application.Mapping;
using Infra.Context;
using Infra.Identity;
using InfraIOC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Triad.Authorization;
using Triad.Middleware;


var builder = WebApplication.CreateBuilder(args);

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Dependências gerais
builder.Services.AddHttpContextAccessor();


//Authorization
builder.Services.AddAuthorization(); 
builder.Services.AddSingleton<
    IAuthorizationPolicyProvider,
    PermissaoPolicyProvider>();
builder.Services.AddScoped<
    IAuthorizationHandler,
    PermissaoHandler>();


//Runtime, migration passa pelo factory
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("POSTGRES")
    ));

//Runtime, passa pelo factory de identity
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("POSTGRES")
    ));

// Identity
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        // Configurações básicas (ajuste conforme precisar)
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;

        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();
// configurando o jwt
var jwtSettings = builder.Configuration.GetSection("Jwt");

var jwtKey = jwtSettings["Key"];

if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new Exception("Jwt:Key não configurada.");
}
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey)
        )
    };
});

//injeção de dependencia
builder.Services.AddInfrastructure(builder.Configuration);

//swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Triad API",
        Version = "v1"
    });


    //  Definição do JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Digite: Bearer {seu token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // Exigir token nos endpoints
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
// BUILD
var app = builder.Build();


// PIPELINE



//configurando Seed de roles
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var appDbContext =
        services.GetRequiredService<AppDbContext>();

    var identityContext =
        services.GetRequiredService<IdentityContext>();

    await appDbContext.Database.MigrateAsync();
    await identityContext.Database.MigrateAsync();

    var roleManager =
        services.GetRequiredService<RoleManager<IdentityRole>>();

    var userManager =
        services.GetRequiredService<UserManager<ApplicationUser>>();

    await SeedIdentity.SeedRolesAsync(roleManager);
    await SeedIdentity.SeedAdminAsync(userManager);
    // await SeedIdentity.SeedUserAsync(userManager);
}

//swagger
    app.UseSwagger();
    app.UseSwaggerUI();


//implementando ExceptionMiddleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
