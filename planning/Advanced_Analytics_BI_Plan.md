
# خطة تنفيذ التحليلات المتقدمة وذكاء الأعمال (Advanced Analytics & BI Plan)

## 1. مقدمة

يهدف هذا المستند إلى توفير خارطة طريق فنية لتطوير قدرات النظام من مجرد عرض تقارير أساسية إلى تقديم تحليلات متقدمة وذكاء أعمال (Business Intelligence) حقيقي. تركز هذه الخطة على استخلاص رؤى قابلة للتنفيذ (Actionable Insights) من خلال المقارنات، تحليل الاتجاهات، وفهم سلوك المستخدمين بشكل أعمق.

---

## 2. اعتبارات معمارية أساسية

قبل البدء، يجب وضع بعض الأساسات المعمارية لدعم هذه التحليلات المعقدة.

### 2.1. خدمة تتبع الأحداث (Analytics Tracker Service)

- **التحدي:** العديد من التحليلات المقترحة (مثل قمع اكتساب العملاء) تتطلب تتبع أحداث لا يتم تخزينها عادةً في كيانات قاعدة البيانات (مثل زيارات الصفحات، عمليات البحث).
- **التوصية:** إنشاء خدمة جديدة `IAnalyticsTrackerService`.
- **الغاية:** تكون مسؤولة عن تسجيل الأحداث الهامة (e.g., `UserSearched`, `PropertyViewed`) في مخزن بيانات منفصل (مثل جدول `AnalyticsEvents` في SQL، أو خدمة متخصصة مثل Azure Application Insights).
- **الإجراءات المطلوبة:**
    1.  **في `YemenBooking.Core/Interfaces/Services`:** إنشاء واجهة `IAnalyticsTrackerService` مع دوال مثل `TrackEventAsync(string eventName, Dictionary<string, string> properties)`.
    2.  **في `YemenBooking.Infrastructure/Services`:** إنشاء فئة `AnalyticsTrackerService` التي تنفذ هذه الواجهة.

### 2.2. مهام الخلفية (Background Jobs)

- **التحدي:** بعض التحليلات (مثل تحليل الأفواج) مكلفة حسابياً وقد تؤدي إلى بطء في استجابة الـ API إذا تم حسابها في الوقت الفعلي.
- **التوصية:** استخدام نظام مهام خلفية (مثل Hangfire أو Quartz.NET) لحساب هذه التحليلات بشكل دوري (مثلاً، كل ليلة) وتخزين النتائج في جداول ملخصة (Summary Tables).
- **الغاية:** ضمان استجابة سريعة جداً لواجهات برمجة التطبيقات التحليلية.

---

## 3. تحليلات للمسؤولين (Platform Admins)

### 3.1. `GetUserAcquisitionFunnelQuery` (تحليل قمع اكتساب العملاء)
- **الغاية:** فهم رحلة العميل وتحديد نقاط التسرب.
- **DTO المقترح:** `UserFunnelDto` (يحتوي على `TotalVisitors`, `TotalSearches`, `TotalPropertyViews`, `TotalBookingsCompleted`, `ConversionRates`).
- **التبعية:** يعتمد بشكل كامل على `IAnalyticsTrackerService` لجلب أعداد الأحداث.
- **الإجراءات المطلوبة:**
    - **`IAnalyticsTrackerService`:** إضافة دالة `GetEventCountsAsync(DateRange range, IEnumerable<string> eventNames)`.

### 3.2. `GetCustomerCohortAnalysisQuery` (تحليل أفواج العملاء)
- **الغاية:** قياس ولاء العملاء وقدرة المنصة على الاحتفاظ بهم.
- **DTO المقترح:** `List<CohortDto>` (يحتوي على `CohortMonth`, `TotalNewUsers`, `List<double> MonthlyRetention`).
- **التبعية:** يتطلب حسابات معقدة.
- **الإجراءات المطلوبة:**
    - **`IUserRepository`:** إضافة دالة `GetUsersByRegistrationMonthAsync(int year, int month)`.
    - **`IBookingRepository`:** إضافة دالة `GetFirstBookingDateForUsersAsync(IEnumerable<Guid> userIds)`.
    - **`IDashboardService`:** إضافة دالة `GenerateCohortAnalysisAsync(DateRange range)` التي تحتوي على منطق العمل الرئيسي (يفضل تنفيذها كـ Background Job).

### 3.3. `GetPlatformRevenueBreakdownQuery` (تحليل تفصيلي لإيرادات المنصة)
- **الغاية:** فهم مصادر الدخل المختلفة.
- **DTO المقترح:** `RevenueBreakdownDto` (يحتوي على `TotalRevenue`, `RevenueFromCommissions`, `RevenueFromServices`).
- **التبعية:** يتطلب معرفة نسبة عمولة المنصة.
- **الإجراءات المطلوبة:**
    - **كيان `Booking`:** إضافة حقل `decimal PlatformCommissionAmount { get; set; }`.
    - **`IBookingRepository`:** إضافة دالة `GetTotalCommissionAsync(DateRange range)`.

### 3.4. `GetPlatformCancellationAnalysisQuery` (تحليل أسباب الإلغاء)
- **الغاية:** فهم الأسباب الرئيسية لإلغاء الحجوزات وتأثيرها المالي.
- **DTO المقترح:** `List<CancellationReasonDto>` (يحتوي على `Reason`, `Count`, `LostRevenue`).
- **التبعية:** يعتمد على حقل سبب الإلغاء.
- **الإجراءات المطلوبة:**
    - **كيان `Booking`:** التأكد من وجود حقل `string? CancellationReason`.
    - **`IBookingRepository`:** إضافة دالة `GetCancellationReasonsSummaryAsync(DateRange range)`.

---

## 4. تحليلات لملاك وموظفي العقارات (Property Owners/Staff)

### 4.1. `GetPropertyPerformanceComparisonQuery` (مقارنة أداء العقار)
- **الغاية:** مقارنة الأداء الحالي بفترة سابقة.
- **DTO المقترح:** `PerformanceComparisonDto` (يحتوي على `CurrentPeriodRevenue`, `PreviousPeriodRevenue`, `RevenueChangePercentage`, ومقاييس أخرى).
- **التبعية:** يتطلب جلب بيانات لفترتين زمنيتين.
- **الإجراءات المطلوبة:**
    - **`IDashboardService`:** إضافة دالة `GetPropertyPerformanceComparisonAsync(Guid propertyId, DateRange current, DateRange previous)`.

### 4.2. `GetBookingWindowAnalysisQuery` (تحليل نافذة الحجز)
- **الغاية:** فهم سلوك الحجز المسبق للعملاء.
- **DTO المقترح:** `BookingWindowDto` (يحتوي على `AverageLeadTimeInDays`, `BookingsLastMinute`, ...).
- **التبعية:** يتطلب حساب الفرق بين تاريخ الحجز وتاريخ الوصول.
- **الإجراءات المطلوبة:**
    - **`IBookingRepository`:** إضافة دالة `GetBookingLeadTimeStatsAsync(Guid propertyId)`.

### 4.3. `GetReviewSentimentAnalysisQuery` (تحليل مشاعر التقييمات)
- **الغاية:** فهم أعمق لآراء النزلاء.
- **DTO المقترح:** `ReviewSentimentDto` (يحتوي على `OverallSentimentScore`, `PositiveKeywords`, `NegativeKeywords`).
- **التبعية:** يتطلب خدمة خارجية أو مكتبة لتحليل النصوص.
- **الإجراءات المطلوبة:**
    - **خدمة جديدة:** إنشاء `ISentimentAnalysisService`.
    - **`IReviewRepository`:** إضافة دالة `GetAllCommentsForPropertyAsync(Guid propertyId)`.

---

## 5. تحليلات للعملاء (Customers)

### 5.1. `GetUserLifetimeStatsQuery` (إحصائيات المستخدم مدى الحياة)
- **الغاية:** زيادة تفاعل العميل وولائه.
- **DTO المقترح:** `UserLifetimeStatsDto` (يحتوي على `TotalNightsStayed`, `TotalMoneySpent`, `FavoriteCity`).
- **التبعية:** يتطلب تجميع بيانات من حجوزات المستخدم.
- **الإجراءات المطلوبة:**
    - **`IBookingRepository`:** إضافة دالة `GetUserLifetimeBookingStatsAsync(Guid userId)`.

### 5.2. `GetUserLoyaltyProgressQuery` (متابعة تقدم الولاء)
- **الغاية:** تحفيز العميل على الحجز أكثر.
- **DTO المقترح:** `LoyaltyProgressDto` (يحتوي على `CurrentTier`, `NextTier`, `AmountNeededForNextTier`).
- **التبعية:** يعتمد على حقول الولاء في كيان المستخدم.
- **الإجراءات المطلوبة:**
    - **كيان `User`:** التأكد من وجود `LoyaltyTier` و `TotalSpent`.
    - **خدمة جديدة:** يمكن إنشاء `ILoyaltyService` لتحديد منطق الترقيات والمزايا.

---

## 6. خارطة طريق التنفيذ

1.  **الأساسات:**
    -   إنشاء `IAnalyticsTrackerService` و `ISentimentAnalysisService`.
    -   تعديل الكيانات (`Booking`, `User`) وإضافة الحقول الجديدة.
    -   إنشاء وتطبيق ترحيل قاعدة البيانات (Database Migration).

2.  **طبقة البيانات:**
    -   تحديث واجهات المستودعات (`IRepository`) بالدوال الجديدة.
    -   تنفيذ الدوال الجديدة في فئات المستودعات (`Infrastructure`).

3.  **طبقة الخدمات:**
    -   تنفيذ الخدمات الجديدة (`AnalyticsTrackerService`, `SentimentAnalysisService`).
    -   إضافة الدوال التحليلية الجديدة إلى `IDashboardService` وتنفيذها.

4.  **طبقة التطبيق:**
    -   إنشاء جميع الـ DTOs الجديدة للتحليلات.
    -   إنشاء الـ Queries والـ Handlers لكل تحليل، والتي ستقوم باستدعاء `IDashboardService`.

5.  **طبقة الـ API:**
    -   إنشاء `AnalyticsController` جديد.
    -   إضافة نقاط نهاية (Endpoints) لكل استعلام تحليلي، مع التأكد من حمايتها بالأدوار المناسبة (Authorization).

6.  **مهام الخلفية (اختياري لكن موصى به):**
    -   إعداد أداة مثل Hangfire.
    -   إنشاء مهام متكررة لاستدعاء الخدمات التي تقوم بالتحليلات المعقدة وتخزين نتائجها.
