# دليل تنفيذ معالجات الاستعلامات الشاملة
# Complete Query Handlers Implementation Guide

## 📚 فهرس المحتويات | Table of Contents

- [1. نظرة عامة | Overview](#overview)
- [2. القواعد الأساسية | Basic Rules](#basic-rules)
- [3. الاستعلامات المطلوب تنفيذها | Required Query Handlers](#required-queries)
- [4. التوصيات المعمارية | Architectural Recommendations](#architecture)
- [5. أمثلة التنفيذ | Implementation Examples](#examples)

## 1. نظرة عامة | Overview {#overview}

هذا الدليل يحتوي على جميع معالجات الاستعلامات (Query Handlers) المطلوب تنفيذها في نظام إدارة الضيافة اليمنية، مع التوصيات المفصلة للمنطق والتحققات والأمان.

**المبادئ الأساسية:**
- استخدام نمط CQRS للفصل بين القراءة والكتابة
- تحسين الأداء من خلال Read Models محسّنة
- دعم الصفحات والفلترة والبحث
- التحقق من الصلاحيات والأمان
- التعامل مع الأخطاء بشكل احترافي

---

## 2. القواعد الأساسية | Basic Rules {#basic-rules}

### بنية معالج الاستعلام الأساسية:

```csharp
/// <summary>
/// معالج استعلام [اسم الاستعلام]
/// [Query Name] query handler
/// </summary>
public class [QueryName]Handler : IRequestHandler<[QueryName], [ReturnType]>
{
    #region Dependencies
    private readonly I[Repository]Repository _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<[QueryName]Handler> _logger;
    #endregion

    #region Constructor
    public [QueryName]Handler(
        I[Repository]Repository repository,
        ICurrentUserService currentUserService,
        ILogger<[QueryName]Handler> logger)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _logger = logger;
    }
    #endregion

    #region Main Handler
    public async Task<[ReturnType]> Handle([QueryName] request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("معالجة استعلام: {QueryName}", nameof([QueryName]));

            // 1. التحقق من صحة المدخلات
            // 2. التحقق من الصلاحيات
            // 3. تنفيذ الاستعلام
            // 4. تطبيق الفلترة والصفحات
            // 5. إرجاع النتيجة

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطأ في معالجة استعلام: {QueryName}", nameof([QueryName]));
            throw;
        }
    }
    #endregion
}
```

### التحققات المطلوبة في كل معالج:

1. **التحقق من المدخلات**: Null checks, validation rules
2. **التحقق من الصلاحيات**: User authorization, data access permissions
3. **التحقق من وجود البيانات**: Entity existence validation
4. **التعامل مع الأخطاء**: Proper exception handling and logging

---

## 3. الاستعلامات المطلوب تنفيذها | Required Query Handlers {#required-queries}

### 🏠 **Property Management Queries**
ز
#### **GetAllPropertiesQueryHandler**
```csharp
/// <summary>
/// الحصول على جميع العقارات مع الفلترة والصفحات
/// Get all properties with filtering and pagination
/// </summary>
```

**المتطلبات:**
- دعم الصفحات (Pagination)
- البحث النصي (Title, Description, Location)
- فلترة حسب النوع، الحالة، المحافظة، السعر
- ترتيب حسب التاريخ، السعر، التقييم
- عرض الصور الرئيسية فقط
- إخفاء العقارات المحذوفة أو غير المنشورة

**التحققات:**
- التحقق من صحة PageNumber و PageSize
- التحقق من صلاحيات العرض حسب حالة العقار
- تطبيق فلاتر الأمان للمستخدم العادي

---

#### **GetPropertyByIdQueryHandler**
```csharp
/// <summary>
/// الحصول على تفاصيل عقار معين
/// Get property details by ID
/// </summary>
```

**المتطلبات:**
- عرض كامل تفاصيل العقار
- تضمين الصور، الخدمات، المرافق، الوحدات
- عرض التقييمات والمراجعات
- حساب متوسط السعر والتقييم
- عرض معلومات المالك (محدودة)

**التحققات:**
- التحقق من وجود العقار
- التحقق من حالة النشر للزوار
- صلاحيات عرض العقارات المعلقة/المرفوضة

---

### 🏨 **Unit Management Queries**

#### **GetAvailableUnitsQueryHandler**
```csharp
/// <summary>
/// الحصول على الوحدات المتاحة للحجز
/// Get available units for booking
/// </summary>
```

**المتطلبات:**
- فلترة حسب تواريخ الإقامة
- التحقق من عدم وجود حجوزات متضاربة
- عرض الأسعار المحدثة
- فلترة حسب عدد الضيوف والمرافق

**التحققات:**
- صحة تواريخ البحث
- توفر الوحدة للحجز
- حالة الوحدة النشطة

---

### 📅 **Booking Management Queries**

#### **GetBookingsByUserQueryHandler**
```csharp
/// <summary>
/// الحصول على حجوزات المستخدم
/// Get user's bookings
/// </summary>
```

**المتطلبات:**
- عرض جميع الحجوزات (مؤكدة، معلقة، ملغاة)
- ترتيب حسب التاريخ
- إمكانية الفلترة حسب الحالة والتاريخ
- عرض تفاصيل الدفع والخدمات

**التحققات:**
- التحقق من هوية المستخدم
- صلاحيات عرض الحجوزات

---

### 💳 **Payment Management Queries**

#### **GetPaymentsByBookingQueryHandler**
```csharp
/// <summary>
/// الحصول على مدفوعات حجز معين
/// Get payments for specific booking
/// </summary>
```

**المتطلبات:**
- عرض جميع المدفوعات المرتبطة بالحجز
- تفاصيل طرق الدفع والحالة
- حساب المبالغ المدفوعة والمتبقية
- سجل المعاملات المالية

**التحققات:**
- التحقق من ملكية الحجز أو صلاحيات الإدارة
- التحقق من وجود الحجز

---

### 📊 **Analytics Management Queries**

#### **GetTopPerformingPropertiesQueryHandler**
```csharp
/// <summary>
/// الحصول على العقارات الأفضل أداءً
/// Get top performing properties
/// </summary>
```

**المتطلبات:**
- ترتيب حسب معدل الإشغال
- حساب الإيرادات الإجمالية
- متوسط التقييمات
- عدد الحجوزات المكتملة
- معدل الإلغاء

**التحققات:**
- صلاحيات الوصول للتحليلات
- فترة زمنية صحيحة للتحليل

---

### 📈 **Dashboard Queries**

#### **GetAdminDashboardQueryHandler**
```csharp
/// <summary>
/// الحصول على بيانات لوحة المسؤول
/// Get admin dashboard data
/// </summary>
```

**المتطلبات:**
- إحصائيات عامة (إجمالي العقارات، المستخدمين، الحجوزات)
- الإيرادات اليومية/الشهرية
- العقارات والحجوزات المعلقة
- أحدث الأنشطة والتقارير

**التحققات:**
- صلاحيات المسؤول فقط
- تجميع البيانات بكفاءة

---

### 🔍 **Search and Filter Queries**

#### **SearchPropertiesQueryHandler**
```csharp
/// <summary>
/// البحث المتقدم في العقارات
/// Advanced property search
/// </summary>
```

**المتطلبات:**
- البحث النصي في العنوان والوصف
- فلترة جغرافية (محافظة، مديرية)
- فلترة حسب السعر والتقييم
- فلترة حسب المرافق والخدمات
- ترتيب متعدد المعايير

**التحققات:**
- صحة معايير البحث
- حدود نتائج البحث

---

## 4. أمثلة تطبيقية متقدمة | Advanced Implementation Examples {#examples}

### مثال شامل: GetPropertyDetailsQueryHandler

```csharp
/// <summary>
/// معالج استعلام الحصول على تفاصيل العقار الكاملة
/// Complete property details query handler
/// </summary>
public class GetPropertyDetailsQueryHandler : IRequestHandler<GetPropertyDetailsQuery, PropertyDetailsDto>
{
    #region Dependencies
    private readonly IPropertyRepository _propertyRepository;
    private readonly IPropertyImageRepository _imageRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<GetPropertyDetailsQueryHandler> _logger;
    #endregion

    #region Constructor
    public GetPropertyDetailsQueryHandler(
        IPropertyRepository propertyRepository,
        IPropertyImageRepository imageRepository,
        IReviewRepository reviewRepository,
        ICurrentUserService currentUserService,
        ILogger<GetPropertyDetailsQueryHandler> logger)
    {
        _propertyRepository = propertyRepository;
        _imageRepository = imageRepository;
        _reviewRepository = reviewRepository;
        _currentUserService = currentUserService;
        _logger = logger;
    }
    #endregion

    #region Handler Implementation
    public async Task<PropertyDetailsDto> Handle(GetPropertyDetailsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("جاري معالجة استعلام تفاصيل العقار: {PropertyId}", request.PropertyId);

            // 1. التحقق من صحة المدخلات
            if (request.PropertyId <= 0)
            {
                throw new ValidationException("معرف العقار غير صحيح");
            }

            // 2. الحصول على العقار الأساسي
            var property = await _propertyRepository.GetByIdAsync(request.PropertyId);
            if (property == null)
            {
                throw new NotFoundException($"العقار بالمعرف {request.PropertyId} غير موجود");
            }

            // 3. التحقق من صلاحيات العرض
            await ValidateViewPermissions(property);

            // 4. الحصول على البيانات المرتبطة
            var images = await _imageRepository.GetByPropertyIdAsync(request.PropertyId);
            var reviews = await _reviewRepository.GetByPropertyIdAsync(request.PropertyId, 1, 10);
            var availableUnits = await GetAvailableUnits(request.PropertyId, request.CheckIn, request.CheckOut);

            // 5. بناء DTO الاستجابة
            var result = new PropertyDetailsDto
            {
                Id = property.Id,
                Title = property.Title,
                Description = property.Description,
                Location = new LocationDto
                {
                    Governorate = property.Governorate,
                    Directorate = property.Directorate,
                    Address = property.Address,
                    Latitude = property.Latitude,
                    Longitude = property.Longitude
                },
                Images = images.Select(img => new PropertyImageDto
                {
                    Id = img.Id,
                    Url = img.Url,
                    Caption = img.Caption,
                    IsMain = img.IsMain
                }).ToList(),
                Reviews = new ReviewSummaryDto
                {
                    TotalCount = reviews.TotalCount,
                    AverageRating = reviews.Items.Any() ? reviews.Items.Average(r => r.Rating) : 0,
                    RecentReviews = reviews.Items.Take(5).Select(r => new ReviewDto
                    {
                        Id = r.Id,
                        Rating = r.Rating,
                        Comment = r.Comment,
                        UserName = r.User.FirstName,
                        CreatedDate = r.CreatedDate
                    }).ToList()
                },
                AvailableUnits = availableUnits,
                Owner = new PropertyOwnerDto
                {
                    Id = property.Owner.Id,
                    Name = $"{property.Owner.FirstName} {property.Owner.LastName}",
                    Avatar = property.Owner.Avatar,
                    JoinDate = property.Owner.CreatedDate
                }
            };

            _logger.LogInformation("تم معالجة استعلام تفاصيل العقار بنجاح: {PropertyId}", request.PropertyId);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطأ في معالجة استعلام تفاصيل العقار: {PropertyId}", request.PropertyId);
            throw;
        }
    }

    #region Private Methods
    private async Task ValidateViewPermissions(Property property)
    {
        var currentUser = await _currentUserService.GetCurrentUserAsync();
        
        // العقارات المنشورة متاحة للجميع
        if (property.Status == PropertyStatus.Published)
            return;

        // العقارات غير المنشورة تتطلب صلاحيات خاصة
        if (currentUser == null)
        {
            throw new UnauthorizedAccessException("يجب تسجيل الدخول لعرض هذا العقار");
        }

        // المالك يمكنه رؤية عقاره
        if (property.OwnerId == _currentUserService.UserId)
            return;

        // المسؤولون يمكنهم رؤية جميع العقارات
        if (await _currentUserService.IsInRoleAsync("Admin"))
            return;

        throw new UnauthorizedAccessException("ليس لديك صلاحية لعرض هذا العقار");
    }

    private async Task<List<UnitAvailabilityDto>> GetAvailableUnits(int propertyId, DateTime? checkIn, DateTime? checkOut)
    {
        // إذا لم يتم تحديد تواريخ، عرض جميع الوحدات
        if (!checkIn.HasValue || !checkOut.HasValue)
        {
            return await _propertyRepository.GetUnitsByPropertyIdAsync(propertyId);
        }

        // البحث عن الوحدات المتاحة في التواريخ المحددة
        return await _propertyRepository.GetAvailableUnitsAsync(propertyId, checkIn.Value, checkOut.Value);
    }
    #endregion
}
```

### مثال: GetBookingTrendsQueryHandler

```csharp
/// <summary>
/// معالج استعلام اتجاهات الحجوزات
/// Booking trends analysis query handler
/// </summary>
public class GetBookingTrendsQueryHandler : IRequestHandler<GetBookingTrendsQuery, BookingTrendsDto>
{
    #region Dependencies
    private readonly IBookingRepository _bookingRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<GetBookingTrendsQueryHandler> _logger;
    #endregion

    #region Handler Implementation
    public async Task<BookingTrendsDto> Handle(GetBookingTrendsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. التحقق من الصلاحيات
            await ValidateAnalyticsPermissions();

            // 2. تحديد الفترة الزمنية
            var endDate = request.EndDate ?? DateTime.UtcNow;
            var startDate = request.StartDate ?? endDate.AddMonths(-12);

            // 3. الحصول على بيانات الحجوزات
            var bookings = await _bookingRepository.GetBookingsInPeriodAsync(startDate, endDate);

            // 4. تحليل الاتجاهات
            var monthlyTrends = bookings
                .GroupBy(b => new { b.CreatedDate.Year, b.CreatedDate.Month })
                .Select(g => new MonthlyTrendDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    BookingCount = g.Count(),
                    Revenue = g.Sum(b => b.TotalAmount),
                    AverageBookingValue = g.Average(b => b.TotalAmount),
                    CancellationRate = (double)g.Count(b => b.Status == BookingStatus.Cancelled) / g.Count() * 100
                })
                .OrderBy(t => t.Year).ThenBy(t => t.Month)
                .ToList();

            // 5. حساب معدلات النمو
            CalculateGrowthRates(monthlyTrends);

            return new BookingTrendsDto
            {
                MonthlyTrends = monthlyTrends,
                PeriodSummary = CalculatePeriodSummary(bookings),
                SeasonalPatterns = AnalyzeSeasonalPatterns(monthlyTrends)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطأ في معالجة استعلام اتجاهات الحجوزات");
            throw;
        }
    }

    private async Task ValidateAnalyticsPermissions()
    {
        var currentUser = await _currentUserService.GetCurrentUserAsync();
        if (currentUser == null || !await _currentUserService.IsInRoleAsync("Admin", "PropertyManager"))
        {
            throw new UnauthorizedAccessException("ليس لديك صلاحية للوصول للتحليلات");
        }
    }

    private void CalculateGrowthRates(List<MonthlyTrendDto> trends)
    {
        for (int i = 1; i < trends.Count; i++)
        {
            var current = trends[i];
            var previous = trends[i - 1];
            
            current.BookingGrowthRate = previous.BookingCount > 0 
                ? ((double)(current.BookingCount - previous.BookingCount) / previous.BookingCount) * 100 
                : 0;
            
            current.RevenueGrowthRate = previous.Revenue > 0 
                ? ((current.Revenue - previous.Revenue) / previous.Revenue) * 100 
                : 0;
        }
    }
    #endregion
}
```

---
/// الحصول على حجوزات المستخدم الحالي
/// Get current user's bookings
/// </summary>
```

**المتطلبات:**
- عرض حجوزات المستخدم حسب الحالة
- ترتيب حسب التاريخ (القادمة أولاً)
- إمكانية الإلغاء/التعديل حسب السياسة
- تفاصيل الدفع والفواتير

**التحققات:**
- التحقق من تسجيل الدخول
- فلترة حسب معرف المستخدم

---

#### **GetPropertyBookingsQueryHandler**
```csharp
/// <summary>
/// الحصول على حجوزات عقار معين (للمالك)
/// Get property bookings (for owner)
/// </summary>
```

**المتطلبات:**
- عرض حجوزات العقار للمالك
- إحصائيات الأداء
- تقارير الإيرادات
- إدارة التوفر

**التحققات:**
- التحقق من ملكية العقار
- صلاحيات المالك أو الموظف

---

#### **GetBookingByIdQueryHandler**
```csharp
/// <summary>
/// الحصول على تفاصيل حجز معين
/// Get booking details by ID
/// </summary>
```

**المتطلبات:**
- تفاصيل كاملة للحجز
- معلومات الدفع والفواتير
- تاريخ العمليات (Timeline)
- إمكانيات الإجراءات المتاحة

**التحققات:**
- التحقق من ملكية الحجز أو صلاحية العرض
- فلترة المعلومات الحساسة

---

### 💰 **Payment Management Queries**

#### **GetAllPaymentsQueryHandler**
```csharp
/// <summary>
/// الحصول على جميع المدفوعات (للمسؤولين)
/// Get all payments (for admins)
/// </summary>
```

**المتطلبات:**
- فلترة متقدمة (حالة، طريقة الدفع، تاريخ، مبلغ)
- تقارير مالية
- إحصائيات الإيرادات
- تتبع المعاملات المشبوهة

**التحققات:**
- صلاحيات المسؤول المالي فقط
- تشفير المعلومات الحساسة

---

#### **GetPaymentsByBookingQueryHandler**
```csharp
/// <summary>
/// الحصول على مدفوعات حجز معين
/// Get payments for specific booking
/// </summary>
```

**المتطلبات:**
- تاريخ جميع المدفوعات
- حالة كل دفعة
- تفاصيل الاسترداد إن وجد
- روابط الفواتير

**التحققات:**
- التحقق من ملكية الحجز
- فلترة المعلومات المالية الحساسة

---

#### **GetMyPaymentsQueryHandler**
```csharp
/// <summary>
/// الحصول على مدفوعات المستخدم
/// Get user's payments
/// </summary>
```

**المتطلبات:**
- تاريخ جميع مدفوعات المستخدم
- فلترة حسب الحالة والتاريخ
- روابط الفواتير والإيصالات
- إمكانية طلب استرداد

**التحققات:**
- التحقق من تسجيل الدخول
- فلترة حسب معرف المستخدم

---

### 📊 **Analytics & Reports Queries**

#### **GetDashboardStatsQueryHandler**
```csharp
/// <summary>
/// الحصول على إحصائيات لوحة التحكم
/// Get dashboard statistics
/// </summary>
```

**المتطلبات:**
- إحصائيات عامة للنظام
- مقاييس الأداء الرئيسية (KPIs)
- رسوم بيانية للاتجاهات
- مقارنات زمنية

**التحققات:**
- صلاحيات المسؤول أو المالك
- فلترة البيانات حسب الصلاحيات

---

#### **GetPropertyAnalyticsQueryHandler**
```csharp
/// <summary>
/// الحصول على تحليلات عقار معين
/// Get property analytics
/// </summary>
```

**المتطلبات:**
- إحصائيات الحجوزات والإيرادات
- معدلات الإشغال
- تقييمات العملاء
- مقارنة مع العقارات المشابهة

**التحققات:**
- التحقق من ملكية العقار
- صلاحيات عرض الإحصائيات

---

#### **GetFinancialReportsQueryHandler**
```csharp
/// <summary>
/// الحصول على التقارير المالية
/// Get financial reports
/// </summary>
```

**المتطلبات:**
- تقارير الإيرادات والمصروفات
- تقارير الضرائب
- تحليل الأرباح
- مقارنات دورية

**التحققات:**
- صلاحيات المسؤول المالي فقط
- تشفير المعلومات المالية

---

### 🏗️ **Property Structure Queries**

#### **GetPropertyTypesQueryHandler**
```csharp
/// <summary>
/// الحصول على أنواع العقارات
/// Get property types
/// </summary>
```

**المتطلبات:**
- قائمة أنواع العقارات المفعلة
- أنواع الوحدات لكل نوع عقار
- الحقول المخصصة المرتبطة
- المرافق المتاحة لكل نوع

**التحققات:**
- عرض الأنواع المفعلة فقط للعامة
- عرض جميع الأنواع للمسؤولين

---

#### **GetUnitTypesByPropertyTypeQueryHandler**
```csharp
/// <summary>
/// الحصول على أنواع الوحدات حسب نوع العقار
/// Get unit types by property type
/// </summary>
```

**المتطلبات:**
- قائمة أنواع الوحدات للنوع المحدد
- الحقول المخصصة لكل نوع وحدة
- قواعد التسعير الافتراضية
- المرافق المتاحة

**التحققات:**
- التحقق من وجود نوع العقار
- عرض الأنواع المفعلة فقط

---

#### **GetFieldTypesQueryHandler**
```csharp
/// <summary>
/// الحصول على أنواع الحقول المخصصة
/// Get custom field types
/// </summary>
```

**المتطلبات:**
- قائمة أنواع الحقول المتاحة
- قواعد التحقق لكل نوع
- أمثلة الاستخدام
- التصنيفات والمجموعات

**التحققات:**
- صلاحيات المسؤول لإدارة الحقول
- عرض الأنواع المفعلة للمطورين

---

### 🎯 **Amenities & Services Queries**

#### **GetAllAmenitiesQueryHandler**
```csharp
/// <summary>
/// الحصول على جميع المرافق
/// Get all amenities
/// </summary>
```

**المتطلبات:**
- قائمة جميع المرافق المتاحة
- تصنيف المرافق حسب الفئة
- دعم البحث والفلترة
- حالة التفعيل لكل مرفق

**التحققات:**
- عرض المرافق المفعلة فقط للعامة
- عرض جميع المرافق للمسؤولين

---

#### **GetPropertyAmenitiesQueryHandler**
```csharp
/// <summary>
/// الحصول على مرافق عقار معين
/// Get property amenities
/// </summary>
```

**المتطلبات:**
- قائمة مرافق العقار
- حالة التوفر لكل مرفق
- التكلفة الإضافية إن وجدت
- وصف تفصيلي للمرفق

**التحققات:**
- التحقق من وجود العقار
- فلترة حسب حالة النشر

---

#### **GetAvailableServicesQueryHandler**
```csharp
/// <summary>
/// الحصول على الخدمات المتاحة
/// Get available services
/// </summary>
```

**المتطلبات:**
- قائمة الخدمات المتاحة للحجز
- أسعار وتفاصيل كل خدمة
- شروط وأحكام الخدمة
- مقدمي الخدمة

**التحققات:**
- عرض الخدمات المفعلة فقط
- فلترة حسب الموقع الجغرافي

---

### 🔍 **Search & Filtering Queries**

#### **SearchPropertiesQueryHandler**
```csharp
/// <summary>
/// البحث المتقدم في العقارات
/// Advanced property search
/// </summary>
```

**المتطلبات:**
- بحث نصي متقدم (Elasticsearch)
- فلترة متعددة المعايير
- ترتيب حسب الصلة والأولوية
- اقتراحات البحث الذكية
- حفظ معايير البحث

**التحققات:**
- تحسين الاستعلامات للأداء
- منع هجمات SQL Injection
- حدود البحث للمستخدمين

---

#### **GetSearchSuggestionsQueryHandler**
```csharp
/// <summary>
/// الحصول على اقتراحات البحث
/// Get search suggestions
/// </summary>
```

**المتطلبات:**
- اقتراحات ذكية حسب السياق
- البحث التلقائي (Autocomplete)
- اقتراحات شائعة
- تاريخ البحث الشخصي

**التحققات:**
- حماية خصوصية المستخدم
- فلترة المحتوى غير المناسب

---

### 📱 **Notification Queries**

#### **GetMyNotificationsQueryHandler**
```csharp
/// <summary>
/// الحصول على إشعارات المستخدم
/// Get user notifications
/// </summary>
```

**المتطلبات:**
- قائمة إشعارات المستخدم
- فلترة حسب النوع والحالة
- دعم الصفحات
- عدد الإشعارات غير المقروءة

**التحققات:**
- التحقق من تسجيل الدخول
- فلترة حسب معرف المستخدم

---

#### **GetNotificationByIdQueryHandler**
```csharp
/// <summary>
/// الحصول على تفاصيل إشعار معين
/// Get notification details
/// </summary>
```

**المتطلبات:**
- تفاصيل كاملة للإشعار
- تحديد حالة القراءة
- روابط الإجراءات المرتبطة
- سجل تسليم الإشعار

**التحققات:**
- التحقق من ملكية الإشعار
- تحديث حالة القراءة

---

### 📝 **Review & Rating Queries**

#### **GetPropertyReviewsQueryHandler**
```csharp
/// <summary>
/// الحصول على مراجعات عقار معين
/// Get property reviews
/// </summary>
```

**المتطلبات:**
- قائمة مراجعات العقار
- ترتيب حسب التاريخ أو التقييم
- فلترة حسب التقييم
- إحصائيات التقييم

**التحققات:**
- عرض المراجعات المعتمدة فقط
- فلترة المحتوى غير المناسب

---

#### **GetMyReviewsQueryHandler**
```csharp
/// <summary>
/// الحصول على مراجعات المستخدم
/// Get user's reviews
/// </summary>
```

**المتطلبات:**
- قائمة مراجعات المستخدم
- حالة كل مراجعة
- ردود أصحاب العقارات
- إمكانية التعديل

**التحققات:**
- التحقق من تسجيل الدخول
- فلترة حسب معرف المستخدم

---

### 🚨 **Report Management Queries**

#### **GetAllReportsQueryHandler**
```csharp
/// <summary>
/// الحصول على جميع البلاغات (للمسؤولين)
/// Get all reports (for admins)
/// </summary>
```

**المتطلبات:**
- قائمة جميع البلاغات
- فلترة حسب النوع والحالة
- ترتيب حسب الأولوية
- إحصائيات البلاغات

**التحققات:**
- صلاحيات المسؤول فقط
- فلترة المحتوى الحساس

---

#### **GetReportsByPropertyQueryHandler**
```csharp
/// <summary>
/// الحصول على بلاغات عقار معين
/// Get reports for specific property
/// </summary>
```

**المتطلبات:**
- قائمة بلاغات العقار
- تصنيف حسب نوع البلاغ
- حالة معالجة كل بلاغ
- تاريخ الإجراءات المتخذة

**التحققات:**
- صلاحيات المسؤول أو المالك
- حماية خصوصية المبلغين

---

### 👥 **User Management Queries**

#### **GetAllUsersQueryHandler**
```csharp
/// <summary>
/// الحصول على جميع المستخدمين (للمسؤولين)
/// Get all users (for admins)
/// </summary>
```

**المتطلبات:**
- قائمة جميع المستخدمين
- فلترة حسب الدور والحالة
- إحصائيات النشاط
- دعم البحث المتقدم

**التحققات:**
- صلاحيات المسؤول فقط
- فلترة المعلومات الحساسة

---

#### **GetUserProfileQueryHandler**
```csharp
/// <summary>
/// الحصول على ملف المستخدم الشخصي
/// Get user profile
/// </summary>
```

**المتطلبات:**
- معلومات الملف الشخصي
- إحصائيات النشاط
- التفضيلات والإعدادات
- سجل العمليات الأخيرة

**التحققات:**
- التحقق من ملكية الملف الشخصي
- فلترة المعلومات الخاصة

---

### 📄 **Dynamic Fields Queries**

#### **GetPropertyFieldValuesQueryHandler**
```csharp
/// <summary>
/// الحصول على قيم الحقول المخصصة للعقار
/// Get property custom field values
/// </summary>
```

**المتطلبات:**
- قيم جميع الحقول المخصصة
- تجميع حسب المجموعات
- التحقق من صحة القيم
- دعم أنواع البيانات المختلفة

**التحققات:**
- التحقق من وجود العقار
- صلاحيات عرض الحقول الخاصة

---

#### **GetUnitFieldValuesQueryHandler**
```csharp
/// <summary>
/// الحصول على قيم الحقول المخصصة للوحدة
/// Get unit custom field values
/// </summary>
```

**المتطلبات:**
- قيم حقول الوحدة المخصصة
- وراثة القيم من العقار الأب
- تخصيص القيم للوحدة
- التحقق من التناسق

**التحققات:**
- التحقق من وجود الوحدة
- صلاحيات عرض تفاصيل الوحدة

---

### 📊 **Availability & Pricing Queries**

#### **CheckUnitAvailabilityQueryHandler**
```csharp
/// <summary>
/// التحقق من توفر الوحدة في فترة معينة
/// Check unit availability for specific period
/// </summary>
```

**المتطلبات:**
- فحص التوفر للفترة المطلوبة
- عرض الفترات المتاحة البديلة
- حساب الأسعار للفترة
- معلومات الحد الأدنى للإقامة

**التحققات:**
- التحقق من صحة التواريخ
- فحص قواعد الحجز

---

#### **GetPricingDetailsQueryHandler**
```csharp
/// <summary>
/// الحصول على تفاصيل التسعير
/// Get pricing details
/// </summary>
```

**المتطلبات:**
- تفاصيل أسعار الوحدة
- العروض والخصومات المتاحة
- الرسوم الإضافية
- سياسات الإلغاء وتأثيرها على السعر

**التحققات:**
- التحقق من صحة فترة التسعير
- تطبيق قواعد التسعير الديناميكي

---

## 4. التوصيات المعمارية | Architectural Recommendations {#architecture}

### 🏗️ **بنية المشروع | Project Structure**

```
YemenBooking.Application/
├── Handlers/
│   ├── Queries/
│   │   ├── PropertyManagement/
│   │   │   ├── GetAllPropertiesQueryHandler.cs
│   │   │   ├── GetPropertyByIdQueryHandler.cs
│   │   │   ├── GetMyPropertiesQueryHandler.cs
│   │   │   └── ...
│   │   ├── BookingManagement/
│   │   ├── PaymentManagement/
│   │   ├── Analytics/
│   │   ├── UserManagement/
│   │   └── ...
│   └── ...
├── DTOs/
│   ├── Queries/
│   └── Common/
└── ...
```

### 📝 **نمط التنفيذ الموحد | Unified Implementation Pattern**

```csharp
// 1. تعريف الاستعلام
public class GetAllPropertiesQuery : IRequest<PaginatedResult<PropertyDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public PropertyStatus? Status { get; set; }
    public string? Governorate { get; set; }
    public PropertySortBy SortBy { get; set; } = PropertySortBy.CreatedDate;
    public SortDirection SortDirection { get; set; } = SortDirection.Descending;
}

// 2. تعريف معالج الاستعلام
public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, PaginatedResult<PropertyDto>>
{
    // التبعيات والمنطق...
}
```

### 🔒 **أمان الاستعلامات | Query Security**

```csharp
/// <summary>
/// التحقق من صلاحيات الوصول للبيانات
/// Validate data access permissions
/// </summary>
private async Task<bool> ValidateDataAccessAsync(Guid resourceId, string operation)
{
    var userId = _currentUserService.UserId;
    var userRole = _currentUserService.Role;

    // تطبيق قواعد الصلاحيات حسب الدور
    return userRole switch
    {
        "Admin" => true,
        "PropertyOwner" => await CheckOwnershipAsync(userId, resourceId),
        "Staff" => await CheckStaffPermissionsAsync(userId, resourceId, operation),
        "Guest" => operation == "Read" && await CheckPublicAccessAsync(resourceId),
        _ => false
    };
}
```

### ⚡ **تحسين الأداء | Performance Optimization**

```csharp
/// <summary>
/// تطبيق تحسينات الأداء
/// Apply performance optimizations
/// </summary>
private IQueryable<Property> ApplyOptimizations(IQueryable<Property> query)
{
    return query
        .AsSplitQuery() // لتجنب Cartesian explosion
        .AsNoTracking() // لاستعلامات القراءة فقط
        .Include(p => p.PropertyType)
        .Include(p => p.MainImage)
        .Include(p => p.Location)
        .Where(p => !p.IsDeleted) // فلترة المحذوف
        .Where(p => p.IsPublished); // فلترة غير المنشور للعامة
}
```

### 📊 **التعامل مع الصفحات | Pagination Handling**

```csharp
/// <summary>
/// تطبيق الصفحات والفلترة
/// Apply pagination and filtering
/// </summary>
private async Task<PaginatedResult<T>> CreatePaginatedResultAsync<T>(
    IQueryable<T> query, 
    int pageNumber, 
    int pageSize,
    CancellationToken cancellationToken)
{
    var totalCount = await query.CountAsync(cancellationToken);
    var items = await query
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync(cancellationToken);

    return new PaginatedResult<T>
    {
        Data = items,
        PageNumber = pageNumber,
        PageSize = pageSize,
        TotalCount = totalCount,
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
    };
}
```

---

## 5. أمثلة التنفيذ | Implementation Examples {#examples}

### 📋 **مثال شامل: GetAllPropertiesQueryHandler**

```csharp
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.DTOs;
using YemenBooking.Application.Queries.PropertyManagement;
using YemenBooking.Core.Entities;
using YemenBooking.Core.Enums;
using YemenBooking.Core.Interfaces.Repositories;
using YemenBooking.Core.Interfaces.Services;

namespace YemenBooking.Application.Handlers.Queries.PropertyManagement;

/// <summary>
/// معالج استعلام الحصول على جميع العقارات مع الفلترة والصفحات
/// Get all properties with filtering and pagination query handler
/// 
/// يعالج طلب الحصول على قائمة العقارات ويشمل:
/// - دعم الصفحات والفلترة المتقدمة
/// - البحث النصي في العنوان والوصف
/// - فلترة حسب النوع والحالة والموقع
/// - ترتيب متعدد المعايير
/// - تطبيق قواعد الأمان والصلاحيات
/// - تحسين الأداء للاستعلامات الكبيرة
/// 
/// Handles get all properties request and includes:
/// - Advanced pagination and filtering support
/// - Text search in title and description
/// - Filter by type, status, and location
/// - Multi-criteria sorting
/// - Security and authorization rules
/// - Performance optimization for large queries
/// </summary>
public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, PaginatedResult<PropertyDto>>
{
    #region Dependencies
    private readonly IPropertyRepository _propertyRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<GetAllPropertiesQueryHandler> _logger;
    #endregion

    #region Constructor
    /// <summary>
    /// المنشئ - حقن التبعيات
    /// Constructor - Dependency injection
    /// </summary>
    public GetAllPropertiesQueryHandler(
        IPropertyRepository propertyRepository,
        ICurrentUserService currentUserService,
        ILogger<GetAllPropertiesQueryHandler> logger)
    {
        _propertyRepository = propertyRepository;
        _currentUserService = currentUserService;
        _logger = logger;
    }
    #endregion

    #region Main Handler
    /// <summary>
    /// معالج الاستعلام الرئيسي
    /// Main query handler
    /// </summary>
    public async Task<PaginatedResult<PropertyDto>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("بدء معالجة استعلام الحصول على جميع العقارات - الصفحة: {PageNumber}، الحجم: {PageSize}", 
                request.PageNumber, request.PageSize);

            // التحقق من صحة المدخلات
            // Validate input parameters
            ValidateInputParameters(request);

            // بناء الاستعلام الأساسي
            // Build base query
            var query = await BuildBaseQueryAsync(cancellationToken);

            // تطبيق الفلاتر
            // Apply filters
            query = ApplyFilters(query, request);

            // تطبيق البحث النصي
            // Apply text search
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = ApplyTextSearch(query, request.SearchTerm);
            }

            // تطبيق الترتيب
            // Apply sorting
            query = ApplySorting(query, request.SortBy, request.SortDirection);

            // تنفيذ الاستعلام مع الصفحات
            // Execute query with pagination
            var result = await ExecutePaginatedQueryAsync(query, request.PageNumber, request.PageSize, cancellationToken);

            _logger.LogInformation("تم الحصول على {Count} عقار من إجمالي {Total} عقار", 
                result.Data.Count, result.TotalCount);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطأ في معالجة استعلام الحصول على جميع العقارات");
            throw;
        }
    }
    #endregion

    #region Private Methods

    /// <summary>
    /// التحقق من صحة معاملات المدخلات
    /// Validate input parameters
    /// </summary>
    private void ValidateInputParameters(GetAllPropertiesQuery request)
    {
        if (request.PageNumber < 1)
            throw new ArgumentException("رقم الصفحة يجب أن يكون أكبر من صفر", nameof(request.PageNumber));

        if (request.PageSize < 1 || request.PageSize > 100)
            throw new ArgumentException("حجم الصفحة يجب أن يكون بين 1 و 100", nameof(request.PageSize));
    }

    /// <summary>
    /// بناء الاستعلام الأساسي مع التحسينات
    /// Build base query with optimizations
    /// </summary>
    private async Task<IQueryable<Property>> BuildBaseQueryAsync(CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;
        var userRole = _currentUserService.Role;

        var query = _propertyRepository.GetQueryable()
            .AsNoTracking() // تحسين للقراءة فقط
            .AsSplitQuery() // تجنب Cartesian explosion
            .Include(p => p.PropertyType)
            .Include(p => p.Location)
            .Include(p => p.Owner)
            .Include(p => p.Images.Where(i => i.IsMainImage && !i.IsDeleted))
            .Where(p => !p.IsDeleted); // استبعاد المحذوف

        // تطبيق فلاتر الأمان حسب دور المستخدم
        // Apply security filters based on user role
        query = userRole switch
        {
            "Admin" => query, // المسؤول يرى كل شيء
            "PropertyOwner" => query.Where(p => p.IsPublished || p.OwnerId == currentUserId), // المالك يرى عقاراته + المنشور
            _ => query.Where(p => p.IsPublished && p.Status == PropertyStatus.Approved) // الزوار يرون المنشور والمعتمد فقط
        };

        return await Task.FromResult(query);
    }

    /// <summary>
    /// تطبيق الفلاتر على الاستعلام
    /// Apply filters to query
    /// </summary>
    private IQueryable<Property> ApplyFilters(IQueryable<Property> query, GetAllPropertiesQuery request)
    {
        // فلترة حسب النوع
        if (request.PropertyTypeId.HasValue)
            query = query.Where(p => p.PropertyTypeId == request.PropertyTypeId.Value);

        // فلترة حسب الحالة
        if (request.Status.HasValue)
            query = query.Where(p => p.Status == request.Status.Value);

        // فلترة حسب المحافظة
        if (!string.IsNullOrWhiteSpace(request.Governorate))
            query = query.Where(p => p.Location.Governorate == request.Governorate);

        // فلترة حسب المديرية
        if (!string.IsNullOrWhiteSpace(request.District))
            query = query.Where(p => p.Location.District == request.District);

        // فلترة حسب السعر
        if (request.MinPrice.HasValue)
            query = query.Where(p => p.PricePerNight >= request.MinPrice.Value);

        if (request.MaxPrice.HasValue)
            query = query.Where(p => p.PricePerNight <= request.MaxPrice.Value);

        // فلترة حسب التقييم
        if (request.MinRating.HasValue)
            query = query.Where(p => p.AverageRating >= request.MinRating.Value);

        return query;
    }

    /// <summary>
    /// تطبيق البحث النصي
    /// Apply text search
    /// </summary>
    private IQueryable<Property> ApplyTextSearch(IQueryable<Property> query, string searchTerm)
    {
        var normalizedTerm = searchTerm.Trim().ToLower();

        return query.Where(p => 
            p.Name.ToLower().Contains(normalizedTerm) ||
            p.Description.ToLower().Contains(normalizedTerm) ||
            p.Location.Address.ToLower().Contains(normalizedTerm) ||
            p.Location.Governorate.ToLower().Contains(normalizedTerm) ||
            p.Location.District.ToLower().Contains(normalizedTerm));
    }

    /// <summary>
    /// تطبيق الترتيب
    /// Apply sorting
    /// </summary>
    private IQueryable<Property> ApplySorting(IQueryable<Property> query, PropertySortBy sortBy, SortDirection direction)
    {
        return sortBy switch
        {
            PropertySortBy.Name => direction == SortDirection.Ascending 
                ? query.OrderBy(p => p.Name) 
                : query.OrderByDescending(p => p.Name),
            
            PropertySortBy.Price => direction == SortDirection.Ascending 
                ? query.OrderBy(p => p.PricePerNight) 
                : query.OrderByDescending(p => p.PricePerNight),
            
            PropertySortBy.Rating => direction == SortDirection.Ascending 
                ? query.OrderBy(p => p.AverageRating) 
                : query.OrderByDescending(p => p.AverageRating),
            
            PropertySortBy.CreatedDate => direction == SortDirection.Ascending 
                ? query.OrderBy(p => p.CreatedAt) 
                : query.OrderByDescending(p => p.CreatedAt),
            
            _ => query.OrderByDescending(p => p.CreatedAt) // الافتراضي
        };
    }

    /// <summary>
    /// تنفيذ الاستعلام مع الصفحات
    /// Execute paginated query
    /// </summary>
    private async Task<PaginatedResult<PropertyDto>> ExecutePaginatedQueryAsync(
        IQueryable<Property> query, 
        int pageNumber, 
        int pageSize, 
        CancellationToken cancellationToken)
    {
        // حساب العدد الكلي
        var totalCount = await query.CountAsync(cancellationToken);

        // الحصول على البيانات للصفحة المطلوبة
        var properties = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        // تحويل إلى DTOs
        var propertyDtos = properties.Select(MapToDto).ToList();

        return new PaginatedResult<PropertyDto>
        {
            Data = propertyDtos,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    /// <summary>
    /// تحويل Entity إلى DTO
    /// Map entity to DTO
    /// </summary>
    private PropertyDto MapToDto(Property property)
    {
        return new PropertyDto
        {
            Id = property.Id,
            Name = property.Name,
            Description = property.Description,
            PropertyType = property.PropertyType?.Name,
            PricePerNight = property.PricePerNight,
            AverageRating = property.AverageRating,
            TotalReviews = property.TotalReviews,
            MainImageUrl = property.Images?.FirstOrDefault()?.Url,
            Location = new LocationDto
            {
                Governorate = property.Location.Governorate,
                District = property.Location.District,
                Address = property.Location.Address,
                Latitude = property.Location.Latitude,
                Longitude = property.Location.Longitude
            },
            Owner = new OwnerDto
            {
                Id = property.Owner.Id,
                FullName = property.Owner.FullName,
                Avatar = property.Owner.ProfilePicture
            },
            Status = property.Status,
            IsPublished = property.IsPublished,
            CreatedAt = property.CreatedAt,
            UpdatedAt = property.UpdatedAt
        };
    }

    #endregion
}
```

### 🏨 **مثال متقدم: GetMyBookingsQueryHandler**

```csharp
/// <summary>
/// معالج استعلام الحصول على حجوزات المستخدم الحالي
/// Get current user bookings query handler
/// </summary>
public class GetMyBookingsQueryHandler : IRequestHandler<GetMyBookingsQuery, PaginatedResult<BookingDto>>
{
    #region Dependencies
    private readonly IBookingRepository _bookingRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<GetMyBookingsQueryHandler> _logger;
    #endregion

    #region Constructor
    public GetMyBookingsQueryHandler(
        IBookingRepository bookingRepository,
        ICurrentUserService currentUserService,
        ILogger<GetMyBookingsQueryHandler> logger)
    {
        _bookingRepository = bookingRepository;
        _currentUserService = currentUserService;
        _logger = logger;
    }
    #endregion

    #region Main Handler
    public async Task<PaginatedResult<BookingDto>> Handle(GetMyBookingsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("معالجة استعلام حجوزات المستخدم: {UserId}", _currentUserService.UserId);

            // التحقق من تسجيل الدخول
            var userId = _currentUserService.UserId;
            if (userId == Guid.Empty)
                throw new UnauthorizedAccessException("يجب تسجيل الدخول للوصول للحجوزات");

            // بناء الاستعلام
            var query = BuildUserBookingsQuery(userId);

            // تطبيق الفلاتر
            query = ApplyBookingFilters(query, request);

            // الترتيب (الحجوزات القادمة أولاً)
            query = query.OrderBy(b => b.CheckInDate)
                         .ThenByDescending(b => b.CreatedAt);

            // تنفيذ الاستعلام مع الصفحات
            var result = await ExecuteBookingQueryAsync(query, request.PageNumber, request.PageSize, cancellationToken);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطأ في معالجة استعلام حجوزات المستخدم");
            throw;
        }
    }
    #endregion

    #region Private Methods
    
    private IQueryable<Booking> BuildUserBookingsQuery(Guid userId)
    {
        return _bookingRepository.GetQueryable()
            .AsNoTracking()
            .Include(b => b.Unit)
                .ThenInclude(u => u.Property)
                    .ThenInclude(p => p.Images.Where(i => i.IsMainImage))
            .Include(b => b.Payments)
            .Where(b => b.GuestId == userId && !b.IsDeleted);
    }

    private IQueryable<Booking> ApplyBookingFilters(IQueryable<Booking> query, GetMyBookingsQuery request)
    {
        // فلترة حسب الحالة
        if (request.Status.HasValue)
            query = query.Where(b => b.Status == request.Status.Value);

        // فلترة حسب التاريخ
        if (request.FromDate.HasValue)
            query = query.Where(b => b.CheckInDate >= request.FromDate.Value);

        if (request.ToDate.HasValue)
            query = query.Where(b => b.CheckOutDate <= request.ToDate.Value);

        // فلترة الحجوزات القادمة/السابقة
        if (request.BookingType.HasValue)
        {
            var today = DateTime.UtcNow.Date;
            query = request.BookingType.Value switch
            {
                BookingType.Upcoming => query.Where(b => b.CheckInDate >= today),
                BookingType.Past => query.Where(b => b.CheckOutDate < today),
                BookingType.Current => query.Where(b => b.CheckInDate <= today && b.CheckOutDate >= today),
                _ => query
            };
        }

        return query;
    }

    private async Task<PaginatedResult<BookingDto>> ExecuteBookingQueryAsync(
        IQueryable<Booking> query, 
        int pageNumber, 
        int pageSize, 
        CancellationToken cancellationToken)
    {
        var totalCount = await query.CountAsync(cancellationToken);
        
        var bookings = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var bookingDtos = bookings.Select(MapBookingToDto).ToList();

        return new PaginatedResult<BookingDto>
        {
            Data = bookingDtos,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    private BookingDto MapBookingToDto(Booking booking)
    {
        return new BookingDto
        {
            Id = booking.Id,
            BookingNumber = booking.BookingNumber,
            Status = booking.Status,
            CheckInDate = booking.CheckInDate,
            CheckOutDate = booking.CheckOutDate,
            TotalAmount = booking.TotalAmount,
            Property = new PropertySummaryDto
            {
                Id = booking.Unit.Property.Id,
                Name = booking.Unit.Property.Name,
                MainImageUrl = booking.Unit.Property.Images?.FirstOrDefault()?.Url,
                Location = booking.Unit.Property.Location.Address
            },
            Unit = new UnitSummaryDto
            {
                Id = booking.Unit.Id,
                Name = booking.Unit.Name,
                Type = booking.Unit.UnitType?.Name
            },
            PaymentStatus = DeterminePaymentStatus(booking.Payments),
            CanCancel = CanCancelBooking(booking),
            CanModify = CanModifyBooking(booking),
            CreatedAt = booking.CreatedAt
        };
    }

    private PaymentStatus DeterminePaymentStatus(ICollection<Payment> payments)
    {
        if (!payments.Any()) return PaymentStatus.Pending;
        
        var totalPaid = payments.Where(p => p.Status == PaymentStatus.Completed)
                               .Sum(p => p.Amount.Value);
        var totalRequired = payments.Sum(p => p.Amount.Value);

        return totalPaid >= totalRequired ? PaymentStatus.Completed : PaymentStatus.Partial;
    }

    private bool CanCancelBooking(Booking booking)
    {
        // منطق تحديد إمكانية الإلغاء حسب السياسة
        var hoursUntilCheckIn = (booking.CheckInDate - DateTime.UtcNow).TotalHours;
        return booking.Status == BookingStatus.Confirmed && hoursUntilCheckIn > 24;
    }

    private bool CanModifyBooking(Booking booking)
    {
        // منطق تحديد إمكانية التعديل
        var hoursUntilCheckIn = (booking.CheckInDate - DateTime.UtcNow).TotalHours;
        return booking.Status == BookingStatus.Confirmed && hoursUntilCheckIn > 48;
    }

    #endregion
}
```

---

## 📊 **قائمة شاملة لجميع الاستعلامات الموجودة | Complete Query List**

### 📋 **إحصائيات الاستعلامات | Query Statistics**
- **العدد الإجمالي**: 113 استعلام
- **المجالات المغطاة**: 25 مجال
- **الحالة**: ✅ تم مسح جميع الاستعلامات الموجودة

---

### 🏠 **إدارة العقارات | Property Management (12 استعلام)**
1. `GetAllPropertiesQuery` - الحصول على جميع العقارات مع الفلترة
2. `GetPropertyByIdQuery` - تفاصيل عقار محدد
3. `GetPropertyDetailsQuery` - التفاصيل الكاملة للعقار
4. `GetPropertyForEditQuery` - بيانات العقار للتحرير
5. `GetPropertiesByOwnerQuery` - عقارات مالك محدد
6. `GetPropertiesByTypeQuery` - العقارات حسب النوع
7. `GetPropertiesByCityQuery` - العقارات حسب المدينة
8. `GetPropertiesNearLocationQuery` - العقارات القريبة من موقع
9. `GetFeaturedPropertiesQuery` - العقارات المميزة
10. `GetPendingPropertiesQuery` - العقارات المعلقة
11. `GetRecommendedPropertiesQuery` - العقارات المقترحة
12. `SearchPropertiesQuery` - البحث في العقارات

### 🏨 **إدارة الوحدات | Unit Management (8 استعلامات)**
1. `GetUnitByIdQuery` - تفاصيل وحدة محددة
2. `GetUnitDetailsQuery` - التفاصيل الكاملة للوحدة
3. `GetUnitForEditQuery` - بيانات الوحدة للتحرير
4. `GetUnitsByPropertyQuery` - وحدات عقار محدد
5. `GetUnitsByTypeQuery` - الوحدات حسب النوع
6. `GetAvailableUnitsQuery` - الوحدات المتاحة
7. `GetUnitAvailabilityQuery` - توفر الوحدة
8. `GetUnitPriceQuery` - سعر الوحدة

### 📅 **إدارة الحجوزات | Booking Management (7 استعلامات)**
1. `GetBookingByIdQuery` - تفاصيل حجز محدد
2. `GetBookingsByUserQuery` - حجوزات مستخدم محدد
3. `GetBookingsByPropertyQuery` - حجوزات عقار محدد
4. `GetBookingsByUnitQuery` - حجوزات وحدة محددة
5. `GetBookingsByStatusQuery` - الحجوزات حسب الحالة
6. `GetBookingsByDateRangeQuery` - الحجوزات في فترة معينة
7. `GetBookingServicesQuery` - خدمات الحجز

### 💳 **إدارة المدفوعات | Payment Management (5 استعلامات)**
1. `GetPaymentByIdQuery` - تفاصيل دفعة محددة
2. `GetPaymentsByUserQuery` - مدفوعات مستخدم محدد
3. `GetPaymentsByBookingQuery` - مدفوعات حجز محدد
4. `GetPaymentsByStatusQuery` - المدفوعات حسب الحالة
5. `GetPaymentsByMethodQuery` - المدفوعات حسب طريقة الدفع

### 👥 **إدارة المستخدمين | User Management (7 استعلامات)**
1. `GetAllUsersQuery` - جميع المستخدمين
2. `GetUserByIdQuery` - تفاصيل مستخدم محدد
3. `GetUserByEmailQuery` - البحث بالبريد الإلكتروني
4. `GetCurrentUserQuery` - المستخدم الحالي
5. `GetUsersByRoleQuery` - المستخدمين حسب الدور
6. `GetUserRolesQuery` - أدوار المستخدم
7. `SearchUsersQuery` - البحث في المستخدمين

### ⭐ **إدارة التقييمات | Review Management (4 استعلامات)**
1. `GetReviewsByPropertyQuery` - تقييمات عقار محدد
2. `GetReviewsByUserQuery` - تقييمات مستخدم محدد
3. `GetReviewByBookingQuery` - تقييم حجز محدد
4. `GetPendingReviewsQuery` - التقييمات المعلقة

### 📊 **التحليلات المتقدمة | Analytics Management (9 استعلامات)**
1. `GetTopPerformingPropertiesQuery` - العقارات الأفضل أداءً
2. `GetPropertyPerformanceQuery` - أداء عقار محدد
3. `GetPropertyPerformanceComparisonQuery` - مقارنة أداء العقارات
4. `GetBookingTrendsQuery` - اتجاهات الحجوزات
5. `GetCustomerCohortAnalysisQuery` - تحليل مجموعات العملاء
6. `GetUserAcquisitionFunnelQuery` - قمع اكتساب المستخدمين
7. `GetBookingWindowAnalysisQuery` - تحليل نافذة الحجز
8. `GetPlatformRevenueBreakdownQuery` - تفصيل إيرادات المنصة
9. `GetPlatformCancellationAnalysisQuery` - تحليل الإلغاءات

### 📈 **لوحات المعلومات | Dashboard (9 استعلامات)**
1. `GetAdminDashboardQuery` - لوحة المسؤول
2. `GetOwnerDashboardQuery` - لوحة المالك
3. `GetCustomerDashboardQuery` - لوحة العميل
4. `GetFinancialSummaryQuery` - الملخص المالي
5. `GetOccupancyRateQuery` - معدل الإشغال
6. `GetPopularDestinationsQuery` - الوجهات الشائعة
7. `GetPropertyRatingStatsQuery` - إحصائيات تقييم العقارات
8. `GetReviewSentimentAnalysisQuery` - تحليل مشاعر التقييمات
9. `GetUserLifetimeStatsQuery` - إحصائيات المستخدم مدى الحياة

### 📋 **التقارير والتحليلات | Reports Analytics (6 استعلامات)**
1. `GetBookingReportQuery` - تقرير الحجوزات
2. `GetRevenueReportQuery` - تقرير الإيرادات
3. `GetOccupancyReportQuery` - تقرير الإشغال
4. `GetCustomerReportQuery` - تقرير العملاء
5. `GetPropertyImageStatsQuery` - إحصائيات صور العقارات
6. `GetUserLoyaltyProgressQuery` - تقدم ولاء المستخدم

### 🔧 **إدارة أنواع العقارات | Property Type Management (4 استعلامات)**
1. `GetAllPropertyTypesQuery` - جميع أنواع العقارات
2. `GetPropertyTypeByIdQuery` - تفاصيل نوع عقار محدد
3. `GetUnitTypeByIdQuery` - تفاصيل نوع وحدة محددة
4. `GetUnitTypesByPropertyTypeQuery` - أنواع الوحدات حسب نوع العقار

### 🔍 **إدارة البحث والفلترة | Search Filter Management (5 استعلامات)**
1. `GetSearchFiltersQuery` - فلاتر البحث
2. `GetSearchFilterByIdQuery` - فلتر بحث محدد
3. `SearchPropertiesQuery` - البحث في العقارات
4. `SearchUsersQuery` - البحث في المستخدمين
5. `SearchFieldTypesQuery` - البحث في أنواع الحقول

### 🏷️ **إدارة أنواع الحقول | Field Type Management (3 استعلامات)**
1. `GetAllFieldTypesQuery` - جميع أنواع الحقول
2. `GetFieldTypeByIdQuery` - تفاصيل نوع حقل محدد
3. `SearchFieldTypesQuery` - البحث في أنواع الحقول

### 📋 **إدارة مجموعات الحقول | Field Group Management (2 استعلام)**
1. `GetFieldGroupByIdQuery` - تفاصيل مجموعة حقول محددة
2. `GetFieldGroupsByPropertyTypeQuery` - مجموعات الحقول حسب نوع العقار

### 🔧 **إدارة قيم حقول العقارات | Property Field Value Management (3 استعلامات)**
1. `GetPropertyFieldValuesQuery` - قيم حقول العقار
2. `GetPropertyFieldValueByIdQuery` - قيمة حقل عقار محددة
3. `GetPropertyFieldValuesGroupedQuery` - قيم الحقول مجمعة

### 🏠 **إدارة حقول أنواع العقارات | Property Type Field Management (4 استعلامات)**
1. `GetUnitTypeFieldsQuery` - حقول نوع العقار
2. `GetUnitTypeFieldByIdQuery` - حقل نوع عقار محدد
3. `GetUnitTypeFieldsGroupedQuery` - حقول نوع العقار مجمعة
4. `GetSearchableFieldsQuery` - الحقول القابلة للبحث

### 🏨 **إدارة قيم حقول الوحدات | Unit Field Value Management (3 استعلامات)**
1. `GetUnitFieldValuesQuery` - قيم حقول الوحدة
2. `GetUnitFieldValueByIdQuery` - قيمة حقل وحدة محددة
3. `GetUnitFieldValuesGroupedQuery` - قيم الحقول مجمعة

### 🖼️ **إدارة صور العقارات | Property Image Management (2 استعلام)**
1. `GetPropertyImagesQuery` - صور العقار
2. `GetUnitImagesQuery` - صور الوحدة

### 🛠️ **إدارة الخدمات | Service Management (3 استعلامات)**
1. `GetServiceByIdQuery` - تفاصيل خدمة محددة
2. `GetServicesByTypeQuery` - الخدمات حسب النوع
3. `GetPropertyServicesQuery` - خدمات العقار

### ⭐ **إدارة المرافق | Amenity Management (3 استعلامات)**
1. `GetAllAmenitiesQuery` - جميع المرافق
2. `GetAmenitiesByPropertyQuery` - مرافق عقار محدد
3. `GetAmenitiesByPropertyTypeQuery` - المرافق حسب نوع العقار

### 👨‍💼 **إدارة الموظفين | Staff Management (3 استعلامات)**
1. `GetStaffByPropertyQuery` - موظفو عقار محدد
2. `GetStaffByUserQuery` - موظف بمعرف المستخدم
3. `GetStaffByPositionQuery` - الموظفون حسب المنصب

### 📝 **إدارة التقارير | Report Management (4 استعلامات)**
1. `GetAllReportsQuery` - جميع التقارير
2. `GetReportByIdQuery` - تفاصيل تقرير محدد
3. `GetReportsByPropertyQuery` - تقارير عقار محدد
4. `GetReportsByReportedUserQuery` - التقارير حسب المستخدم المبلغ عنه

### 📜 **إدارة السياسات | Policy Management (3 استعلامات)**
1. `GetPolicyByIdQuery` - تفاصيل سياسة محددة
2. `GetPoliciesByTypeQuery` - السياسات حسب النوع
3. `GetPropertyPoliciesQuery` - سياسات العقار

### 🔔 **إدارة الإشعارات | Notification Management (2 استعلام)**
1. `GetSystemNotificationsQuery` - إشعارات النظام
2. `GetUserNotificationsQuery` - إشعارات المستخدم

### 👥 **إدارة الأدوار | Role Management (1 استعلام)**
1. `GetAllRolesQuery` - جميع الأدوار

### 📊 **إدارة سجلات التدقيق | Audit Log Management (1 استعلام)**
1. `GetAuditLogsQuery` - سجلات التدقيق

### 🎯 **استعلامات إضافية | Additional Queries (4 استعلامات)**
1. `GetPropertyFormFieldsQuery` - حقول نموذج العقار
2. `GetPropertyAmenitiesQuery` - مرافق العقار
3. `GetUserActivityLogQuery` - سجل نشاط المستخدم
4. `GetPropertyPerformanceQuery` - أداء العقار

---

## 📋 **قائمة المراجعة النهائية | Final Checklist**

### ✅ **متطلبات التنفيذ الأساسية:**

- [ ] **الأمان والصلاحيات**: تطبيق قواعد الوصول لكل استعلام
- [ ] **تحسين الأداء**: استخدام AsNoTracking و AsSplitQuery
- [ ] **التعامل مع الأخطاء**: Proper exception handling و logging
- [ ] **الصفحات والفلترة**: دعم متقدم للصفحات والفلترة
- [ ] **التوثيق**: توثيق شامل لكل معالج بالعربية والإنجليزية
- [ ] **اختبار الوحدة**: Unit tests لكل معالج
- [ ] **اختبار التكامل**: Integration tests للسيناريوهات المعقدة

### 🔧 **معايير الجودة:**

- [ ] **قابلية القراءة**: كود واضح ومنظم
- [ ] **قابلية الصيانة**: فصل المسؤوليات وإعادة الاستخدام
- [ ] **الأداء**: استعلامات محسّنة ومفهرسة
- [ ] **الأمان**: تشفير البيانات الحساسة وفلترة المدخلات
- [ ] **المراقبة**: Logging شامل للتتبع والمراقبة

### 📊 **مقاييس النجاح:**

- [ ] **زمن الاستجابة**: أقل من 500ms للاستعلامات البسيطة
- [ ] **الذاكرة**: استهلاك ذاكرة محسّن
- [ ] **الاستقرار**: معدل أخطاء أقل من 0.1%
- [ ] **الأمان**: عدم تسرب بيانات حساسة
- [ ] **تجربة المستخدم**: واجهات سريعة ومتجاوبة

---

## 🏁 **الخلاصة | Conclusion**

هذا الدليل يوفر إطار عمل شامل لتطوير جميع معالجات الاستعلامات في نظام إدارة الضيافة اليمنية. يجب اتباع المعايير والتوصيات المذكورة لضمان بناء نظام قوي وآمن وقابل للتطوير.

**الخطوات التالية:**
1. تطوير معالجات الاستعلامات حسب الأولوية
2. إجراء اختبارات شاملة لكل معالج
3. تحسين الأداء والفهرسة
4. مراجعة الأمان والصلاحيات
5. توثيق المطورين وأدلة الاستخدام

---

**تاريخ الإنشاء:** ديسمبر 2024  
**الإصدار:** 1.0  
**المطور:** فريق تطوير نظام إدارة الضيافة اليمنية
