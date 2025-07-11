using YemenBooking.Api.Extensions;
using YemenBooking.Application.Handlers.Commands.PropertyImages;
using Microsoft.EntityFrameworkCore;
using YemenBooking.Infrastructure.Data.Context;
using YemenBooking.Core.Interfaces.Services;
using YemenBooking.Infrastructure.Services;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YemenBooking.Infrastructure.Settings;

using AutoMapper;
using YemenBooking.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configuring Swagger/OpenAPI with JWT security
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "YemenBooking API",
        Version = "v1",
        Description = "وثائق واجهة برمجة تطبيقات YemenBooking"
    });
    // تعريف أمان JWT
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "أدخل 'Bearer ' متبوعًا برمز JWT"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    // تضمين تعليقات XML
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (System.IO.File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// إضافة MediatR مع معالجات الأوامر
// Add MediatR with command handlers
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CreatePropertyImageCommandHandler).Assembly);
});

// إضافة AutoMapper لمسح ملفات Mapping الخاصة بالتطبيق
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// إضافة خدمات المشروع
// Add project services
builder.Services.AddYemenBookingServices();

// إضافة دعم Controllers لربط المتحكمات
builder.Services.AddControllers();

// تسجيل إعدادات JWT من ملفات التكوين
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// إعداد المصادقة باستخدام JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
        ValidateIssuerSigningKey = true
    };
});

// إضافة التفويض
builder.Services.AddAuthorization();

// إعداد DbContext لاستخدام SQLite
builder.Services.AddDbContext<YemenBookingDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
           .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning))
);

// إضافة HttpContextAccessor لاستخدامه في CurrentUserService
builder.Services.AddHttpContextAccessor();

// إضافة HttpClient للخدمات التي تحتاجه
builder.Services.AddHttpClient<IGeolocationService, GeolocationService>();
builder.Services.AddHttpClient<IPaymentGatewayService, PaymentGatewayService>();

// تسجيل خدمة EventPublisher
builder.Services.AddScoped<IEventPublisher, EventPublisherService>();

var app = builder.Build();

// حذف إعدادات middleware الفردية
// app.UseHttpsRedirection();
// app.UseAuthentication();
// app.UseAuthorization();
// app.MapControllers();

// استخدام امتداد لتكوين كافة middleware الخاصة بالتطبيق
app.UseYemenBookingMiddlewares();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

