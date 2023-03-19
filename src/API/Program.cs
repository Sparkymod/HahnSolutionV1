using System.Reflection;
using API.Extensions;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Extensions methods to add services, repository and validators automatically.
builder.Services.AddServicesAndRepositoryWithInterface();
builder.Services.AddValidators();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HahnSolutionV1.Api", Version = "v1" });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HahnSolutionV1.Api v1"));

// This will catch any top exception thrown by the current task. This doesn't handle async tasks
app.UseCustomExceptionMiddleware();

// Add Endpoints
app.MapContactRoutes();

app.Run();