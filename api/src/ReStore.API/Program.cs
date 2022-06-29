using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReStore.API.Middleware;
using ReStore.API.Services;
using ReStore.Application;
using ReStore.Infrastructure;
using ReStore.Infrastructure.SeedData;
using Swashbuckle.AspNetCore.Filters;
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

builder.Services.AddSwaggerGen(options =>
{
     options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
     {
          Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
          In = ParameterLocation.Header,
          Name = "Authorization",
          Type = SecuritySchemeType.ApiKey
     });

     options.OperationFilter<SecurityRequirementsOperationFilter>();
});

#endregion


#region CORS-1

builder.Services.AddCors();

#endregion


#region Jwt Bearer

builder.Services.AddAuthentication(options =>
{
     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
         options.TokenValidationParameters = new TokenValidationParameters
         {
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                 .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
              ValidateIssuer = false,
              ValidateAudience = false
         };
    });

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
