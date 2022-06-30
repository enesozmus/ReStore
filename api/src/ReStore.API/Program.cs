using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReStore.API.Middleware;
using ReStore.API.Services;
using ReStore.Application;
using ReStore.Infrastructure;
using ReStore.Infrastructure.SeedData;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region @ Layers @

builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();

#endregion

builder.Services.AddControllers()
        .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddEndpointsApiExplorer();

#region User Service

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHttpContextAccessor();

#endregion


#region Swagger

builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
     {
          Description = "Jwt auth header",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey,
          Scheme = "Bearer"
     });
     c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
});

#endregion


#region CORS-1

builder.Services.AddCors();

#endregion

ConfigurationManager configuration = builder.Configuration;

#region Jwt Bearer

builder.Services.AddAuthentication(options =>
{
     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(opt =>
                {
                     opt.TokenValidationParameters = new TokenValidationParameters
                     {
                          ValidateIssuer = false,
                          ValidateAudience = false,
                          ValidateLifetime = true,
                          ValidateIssuerSigningKey = true,
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                             .GetBytes(configuration["JWTSettings:TokenKey"]))
                     };
                });
builder.Services.AddAuthorization();
builder.Services.AddScoped<TokenService>();

#endregion

var app = builder.Build();

#region ExceptionMiddleware

app.UseMiddleware<ExceptionMiddleware>();

#endregion


#region @ SeedData@

DbInitializer.Initialize(app);

#endregion

if (app.Environment.IsDevelopment())
{
     app.UseSwagger();
     app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region CORS-2

app.UseCors(opt =>
{
     opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
});

#endregion

#region Identity

app.UseAuthentication();
app.UseAuthorization();

#endregion

app.MapControllers();

app.Run();
