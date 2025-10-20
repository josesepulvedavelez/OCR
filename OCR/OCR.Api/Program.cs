using OCR.Api.Middlewares;
using OCR.Application.IService;
using OCR.Application.Service;

var builder = WebApplication.CreateBuilder(args);

// Configuration services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "OCR API", Version = "v1" });
});

// Configuration CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// service registration
builder.Services.AddScoped<IIdCardService, IdCardService>();

var app = builder.Build();

// Middleware exceptions
app.UseMiddleware<ExceptionMiddleware>();

// Swagger Development and Production
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OCR API v1");
    c.RoutePrefix = "swagger"; // URL: https://ocrcolombia.azurewebsites.net/swagger/index.html
});

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
