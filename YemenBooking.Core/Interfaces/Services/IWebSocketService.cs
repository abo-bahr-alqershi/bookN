using System;
using System.Threading;
using System.Threading.Tasks;

namespace YemenBooking.Core.Interfaces.Services
{
    /// <summary>
    /// واجهة خدمة WebSocket للإشعارات
    /// WebSocket notification service interface
    /// </summary>
    public interface IWebSocketService
    {
        /// <summary>
        /// إرسال رسالة عبر WebSocket للمستخدم المحدد
        /// Send a message via WebSocket to a specific user
        /// </summary>
        Task SendMessageAsync(Guid userId, string message, CancellationToken cancellationToken = default);
    }
} 