using Converter.Api.Extensions;
using Converter.Api.Middlewares;
using Converter.Application;
using Converter.Infrastructure;

// Configure services
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddJwtRsaAuth(builder.Configuration);

// Configure pipeline
var app = builder.Build();

app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Initialize default data
await app.InitializeMemoryData();
await app.AddDefaultUserAsync();

app.Run();
