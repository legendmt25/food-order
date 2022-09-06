using Service;
using Repository;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(cors =>
{
    CorsPolicy corsPolicy = new CorsPolicyBuilder().AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().Build();
    cors.AddDefaultPolicy(corsPolicy);
}
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configuring Open API with jwt token security

builder.Services.AddSwaggerGen(options =>
{
    OpenApiServer server = new OpenApiServer();
    server.Url = "http://localhost:5000";

    options.AddServer(server);
    OpenApiReference openApiReference = new OpenApiReference();
    openApiReference.Id = JwtBearerDefaults.AuthenticationScheme;
    openApiReference.Type = ReferenceType.SecurityScheme;

    OpenApiSecurityScheme jwtSecurityScheme = new OpenApiSecurityScheme();
    jwtSecurityScheme.BearerFormat = "Bearer";
    jwtSecurityScheme.Name = "jwt";
    jwtSecurityScheme.In = ParameterLocation.Header;
    jwtSecurityScheme.Type = SecuritySchemeType.Http;
    jwtSecurityScheme.Scheme = JwtBearerDefaults.AuthenticationScheme;
    jwtSecurityScheme.Description = "Bearer: [jwt]";
    jwtSecurityScheme.Reference = openApiReference;


    OpenApiSecurityRequirement openApiSecurityRequirement = new OpenApiSecurityRequirement();
    openApiSecurityRequirement.Add(jwtSecurityScheme, Array.Empty<string>());

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    options.AddSecurityRequirement(openApiSecurityRequirement);
});

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlite"), migrations => migrations.MigrationsAssembly("Repository"));
});

// Configuring Asp.net core identity with Jwt

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    IConfigurationSection jwtConfig = builder.Configuration.GetSection("jwtConfig");
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig.GetValue<string>("validIssuer"),
        ValidAudience = jwtConfig.GetValue<string>("validAudience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.GetValue<string>("secretKey")))
    };
});

builder.Services.AddIdentity<AppUser, IdentityRole<int>>(options =>
{
    options.User.RequireUniqueEmail = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<ApplicationContext>()
.AddDefaultTokenProviders();

// Dependency injection

builder.Services.AddTransient<FoodRepository>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<ShoppingCartRepository>();
builder.Services.AddTransient<OrderRepository>();

builder.Services.AddScoped<FoodService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<ShoppingCartService>();
builder.Services.AddScoped<OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
