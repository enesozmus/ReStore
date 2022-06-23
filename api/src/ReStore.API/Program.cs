using ReStore.API.Middleware;
using ReStore.Application;
using ReStore.Infrastructure;
using ReStore.Infrastructure.SeedData;

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
builder.Services.AddSwaggerGen();

#region CORS-1

builder.Services.AddCors();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
