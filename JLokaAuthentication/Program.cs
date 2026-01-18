using JLokaAuthentication.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add the db context
builder.Services.AddDbContext<AppDbContext>();
// Add the identity model and attach to db context
builder.Services.AddIdentityCore<AppUser>()
    // this is not addded by default
    .AddSignInManager() //
    //  If you use the AddIdentity() method, you do not need to call the AddRoles() method. 
    // The AddIdentity() method will call the AddRoles() method internally.
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
//Add authentication with options
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var secret = builder.Configuration["JwtConfig:secret"];
    var issuer = builder.Configuration["JwtConfig:ValidIssuer"];
    var audience = builder.Configuration["JwtConfig:ValidAudiences"];

    if (secret is null || issuer is null || audience is null)
    {
        throw new ApplicationException("Jwt is not set in the configuration");
    }

    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidIssuer = issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
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
                    new String[] {}
                }
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole(AppRoles.Administrator));
    options.AddPolicy("RequireVipUserRole", policy => policy.RequireRole(AppRoles.VipUser));
    options.AddPolicy("RequireUserRole", policy => policy.RequireRole(AppRoles.User));
    options.AddPolicy("RequireUserRoleOrVipUserRole", policy => policy.RequireRole(AppRoles.User, AppRoles.VipUser));
    options.AddPolicy(AppAuthorizationPolicies.RequireAccessNumber, policy => policy.RequireClaim(AppClaimTypes.AccessNumber));
    options.AddPolicy(AppAuthorizationPolicies.RequireDrivingLicenseNumber, policy => policy.RequireClaim(AppClaimTypes.DrivingLicenseNumber));
    // Here we can set multiple values not Russia
    options.AddPolicy(AppAuthorizationPolicies.RequireCountry, policy => policy.RequireClaim(ClaimTypes.Country, "Russia", "Syria"));
    options.AddPolicy(AppAuthorizationPolicies.RequireDrivingLicenseAndAccessNumber, policy => policy.RequireAssertion(context =>
    {
        var hasDrivingLicense = context.User.HasClaim(c => c.Type == AppClaimTypes.DrivingLicenseNumber);
        var hasAccessNumber = context.User.HasClaim(c => c.Type == AppClaimTypes.AccessNumber);

        return hasDrivingLicense && hasAccessNumber;
    }));
    // Adding custom policy implementation
    options.AddPolicy(AppAuthorizationPolicies.SpecialPremiumContent, policy =>
    {
        policy.Requirements.Add(new SpecialPremiumContentRequirement("Russia"));
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, SpecialPremiumContentAuthorizationHandler>();

builder.Services.Configure<IdentityOptions>(options => {
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
    // User settings
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Check if the roles exist, if not, create them
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    // Ensure the database is created.
    var dbContext = services.GetRequiredService<AppDbContext>();
    //dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    if (!await roleManager.RoleExistsAsync(AppRoles.User))
    {
        await roleManager.CreateAsync(new IdentityRole(AppRoles.User));
    }

    if (!await roleManager.RoleExistsAsync(AppRoles.VipUser))
    {
        await roleManager.CreateAsync(new IdentityRole(AppRoles.VipUser));
    }

    if (!await roleManager.RoleExistsAsync(AppRoles.Administrator))
    {
        await roleManager.CreateAsync(new IdentityRole(AppRoles.Administrator));
    }
}

app.MapControllers();
app.Run();
