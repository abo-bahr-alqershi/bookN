using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace YemenBooking.Api.Extensions
{
    /// <summary>
    /// امتدادات لتكوين الmiddleware الخاصة بتطبيق YemenBooking
    /// Extensions for configuring YemenBooking middleware
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// يهيئ جميع middleware: إعادة التوجيه لـHTTPS، المصادقة، التفويض، OpenAPI وربط المتحكمات
        /// Configures middleware: HTTPS redirection, authentication, authorization, OpenAPI and controllers
        /// </summary>
        public static WebApplication UseYemenBookingMiddlewares(this WebApplication app)
        {
            // إعادة التوجيه إلى HTTPS
            app.UseHttpsRedirection();

            // المصادقة باستخدام JWT
            app.UseAuthentication();

            // التفويض
            app.UseAuthorization();

            // تفعيل Swagger UI في بيئة التطوير
            if (app.Environment.IsDevelopment())
            {
                // توليد مستند Swagger JSON
                app.UseSwagger();
                // تشغيل واجهة Swagger UI
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "YemenBooking API V1");
                    c.RoutePrefix = string.Empty; // الواجهة عند الجذر
                });
            }

            // ربط المتحكمات بنظام التوجيه
            app.MapControllers();

            return app;
        }
    }
} 