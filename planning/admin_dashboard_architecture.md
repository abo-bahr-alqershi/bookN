# هيكلة لوحة التحكم الإدارية - React TypeScript + Vite

## 📁 هيكل المجلدات العام

```
src/
├── components/              # مكونات قابلة للإعادة الاستخدام
│   ├── ui/                 # مكونات UI الأساسية
│   ├── common/             # مكونات مشتركة
│   └── charts/             # مكونات الرسوم البيانية
├── pages/                  # صفحات التطبيق
│   ├── admin/              # صفحات الإدارة
│   ├── property/           # صفحات إدارة العقارات
│   └── shared/             # صفحات مشتركة
├── hooks/                  # Custom React Hooks
├── services/               # خدمات API
├── types/                  # تعريفات TypeScript
├── utils/                  # دوال مساعدة
├── stores/                 # إدارة الحالة (Zustand/Redux)
├── constants/              # الثوابت
├── layouts/                # تخطيطات الصفحات
└── assets/                 # الصور والأيقونات
```

## 🏗️ معمارية لوحة التحكم الإدارية

### 1. الصفحة الرئيسية (Dashboard)
```typescript
// src/pages/admin/AdminDashboard.tsx
- معلومات عامة (KPIs)
- رسوم بيانية للإحصائيات
- آخر الأنشطة
- تنبيهات مهمة
```

**المكونات المطلوبة:**
- `GetAdminDashboard` - البيانات الأساسية
- `GetAdminActivityLogs` - سجل الأنشطة
- `GetBookingTrends` - اتجاهات الحجوزات
- `GetFinancialSummary` - الملخص المالي
- `GetPlatformRevenueBreakdown` - تحليل الإيرادات

### 2. إدارة المستخدمين (Users Management)
```typescript
// src/pages/admin/UsersManagement/
├── UsersList.tsx           # قائمة المستخدمين
├── UserDetails.tsx         # تفاصيل المستخدم
├── UserForm.tsx            # نموذج إضافة/تعديل
├── UserRoles.tsx           # إدارة الأدوار
└── UserActivity.tsx        # نشاط المستخدم
```

**الوظائف المطلوبة:**
- `GetAllUsers` - عرض كل المستخدمين
- `GetUserById` - تفاصيل مستخدم محدد
- `CreateUser` - إنشاء مستخدم جديد
- `UpdateUser` - تحديث بيانات المستخدم
- `ActivateUser` / `DeactivateUser` - تفعيل/إلغاء تفعيل
- `AssignUserRole` - تعيين الأدوار
- `GetUserRoles` - عرض الأدوار

### 3. إدارة العقارات (Properties Management)
```typescript
// src/pages/admin/PropertiesManagement/
├── PropertiesList.tsx      # قائمة العقارات
├── PropertyDetails.tsx     # تفاصيل العقار
├── PropertyApproval.tsx    # موافقة العقارات
├── PropertyTypes.tsx       # أنواع العقارات
├── PropertyFields.tsx      # حقول العقارات
└── PropertyImages.tsx      # صور العقارات
```

**الوظائف المطلوبة:**
- `GetAllProperties` - عرض كل العقارات
- `GetPendingProperties` - العقارات في انتظار الموافقة
- `ApproveProperty` / `RejectProperty` - موافقة/رفض العقار
- `GetAllPropertyTypes` - أنواع العقارات
- `CreatePropertyType` - إنشاء نوع عقار جديد
- `GetAllFieldTypes` - أنواع الحقول
- `CreateFieldType` - إنشاء حقل جديد

### 4. إدارة الحجوزات (Bookings Management)
```typescript
// src/pages/admin/BookingsManagement/
├── BookingsList.tsx        # قائمة الحجوزات
├── BookingDetails.tsx      # تفاصيل الحجز
├── BookingAnalytics.tsx    # تحليلات الحجوزات
├── BookingReports.tsx      # تقارير الحجوزات
└── BookingTimeline.tsx     # الجدول الزمني للحجوزات
```

**الوظائف المطلوبة:**
- `GetBookingsByDateRange` - الحجوزات بفترة محددة
- `GetBookingsByStatus` - الحجوزات حسب الحالة
- `GetBookingTrends` - اتجاهات الحجوزات
- `GetBookingWindowAnalysis` - تحليل نافذة الحجوزات
- `GetBookingReport` - تقارير الحجوزات

### 5. إدارة المدفوعات (Payments Management)
```typescript
// src/pages/admin/PaymentsManagement/
├── PaymentsList.tsx        # قائمة المدفوعات
├── PaymentDetails.tsx      # تفاصيل الدفع
├── PaymentMethods.tsx      # طرق الدفع
├── RefundsManagement.tsx   # إدارة المبالغ المردودة
└── PaymentAnalytics.tsx    # تحليلات المدفوعات
```

**الوظائف المطلوبة:**
- `GetPaymentsByMethod` - المدفوعات حسب الطريقة
- `GetPaymentsByStatus` - المدفوعات حسب الحالة
- `ProcessPayment` - معالجة الدفع
- `RefundPayment` - إرجاع المبلغ
- `VoidPayment` - إلغاء الدفع

### 6. إدارة المراجعات (Reviews Management)
```typescript
// src/pages/admin/ReviewsManagement/
├── ReviewsList.tsx         # قائمة المراجعات
├── ReviewModeration.tsx    # مراجعة التعليقات
├── ReviewAnalytics.tsx     # تحليلات المراجعات
└── ReviewReports.tsx       # تقارير المراجعات
```

**الوظائف المطلوبة:**
- `GetPendingReviews` - المراجعات في انتظار الموافقة
- `ApproveReview` - موافقة المراجعة
- `GetReviewSentimentAnalysis` - تحليل المشاعر
- `DeleteReview` - حذف المراجعة

### 7. إدارة التقارير (Reports Management)
```typescript
// src/pages/admin/ReportsManagement/
├── ReportsList.tsx         # قائمة التقارير
├── ReportBuilder.tsx       # منشئ التقارير
├── ReportScheduler.tsx     # جدولة التقارير
├── CustomReports.tsx       # التقارير المخصصة
└── ReportExport.tsx        # تصدير التقارير
```

**الوظائف المطلوبة:**
- `GetAllReports` - عرض كل التقارير
- `CreateReport` - إنشاء تقرير جديد
- `ExportDashboardReport` - تصدير التقرير
- `GetRevenueReport` - تقرير الإيرادات
- `GetOccupancyReport` - تقرير الإشغال

### 8. إدارة النظام (System Management)
```typescript
// src/pages/admin/SystemManagement/
├── SystemSettings.tsx      # إعدادات النظام
├── AuditLogs.tsx          # سجلات المراجعة
├── SystemHealth.tsx       # صحة النظام
├── BackupRestore.tsx      # النسخ الاحتياطي
└── SystemNotifications.tsx # إشعارات النظام
```

**الوظائف المطلوبة:**
- `GetAuditLogs` - سجلات المراجعة
- `GetSystemNotifications` - إشعارات النظام
- `CreateNotification` - إنشاء إشعار

## 🏗️ معمارية لوحة التحكم لملاك العقارات

### 1. الصفحة الرئيسية (Owner Dashboard)
```typescript
// src/pages/property/PropertyOwnerDashboard.tsx
- إحصائيات العقارات
- الحجوزات الحالية
- الإيرادات
- معدل الإشغال
```

**المكونات المطلوبة:**
- `GetOwnerDashboard` - لوحة التحكم الرئيسية
- `GetPropertiesByOwner` - عقارات المالك
- `GetBookingsByProperty` - حجوزات العقارات
- `GetPropertyPerformance` - أداء العقارات

### 2. إدارة العقارات (My Properties)
```typescript
// src/pages/property/MyProperties/
├── PropertiesList.tsx      # قائمة عقاراتي
├── PropertyForm.tsx        # إضافة/تعديل عقار
├── PropertyDetails.tsx     # تفاصيل العقار
├── PropertyImages.tsx      # صور العقار
├── PropertyPolicies.tsx    # سياسات العقار
├── PropertyServices.tsx    # خدمات العقار
└── PropertyUnits.tsx       # وحدات العقار
```

**الوظائف المطلوبة:**
- `CreateProperty` - إنشاء عقار جديد
- `UpdateProperty` - تحديث العقار
- `GetPropertyForEdit` - بيانات العقار للتعديل
- `CreatePropertyImage` - إضافة صور
- `CreatePropertyPolicy` - إنشاء سياسة
- `CreatePropertyService` - إضافة خدمة

### 3. إدارة الوحدات (Units Management)
```typescript
// src/pages/property/UnitsManagement/
├── UnitsList.tsx           # قائمة الوحدات
├── UnitForm.tsx            # إضافة/تعديل وحدة
├── UnitAvailability.tsx    # توفر الوحدات
├── UnitPricing.tsx         # تسعير الوحدات
└── UnitImages.tsx          # صور الوحدات
```

**الوظائف المطلوبة:**
- `CreateUnit` - إنشاء وحدة جديدة
- `UpdateUnit` - تحديث الوحدة
- `UpdateUnitAvailability` - تحديث التوفر
- `GetUnitsByProperty` - وحدات العقار
- `BulkUpdateUnitAvailability` - تحديث جماعي للتوفر

### 4. إدارة الحجوزات (Bookings Management)
```typescript
// src/pages/property/BookingsManagement/
├── BookingsList.tsx        # قائمة الحجوزات
├── BookingDetails.tsx      # تفاصيل الحجز
├── BookingCalendar.tsx     # تقويم الحجوزات
├── CheckInOut.tsx          # تسجيل الدخول/الخروج
└── BookingServices.tsx     # خدمات الحجز
```

**الوظائف المطلوبة:**
- `GetBookingsByProperty` - حجوزات العقار
- `ConfirmBooking` - تأكيد الحجز
- `CheckIn` / `CheckOut` - تسجيل الدخول/الخروج
- `CompleteBooking` - إكمال الحجز
- `AddServiceToBooking` - إضافة خدمة للحجز

### 5. إدارة الموظفين (Staff Management)
```typescript
// src/pages/property/StaffManagement/
├── StaffList.tsx           # قائمة الموظفين
├── StaffForm.tsx           # إضافة/تعديل موظف
├── StaffSchedule.tsx       # جدول العمل
└── StaffPerformance.tsx    # أداء الموظفين
```

**الوظائف المطلوبة:**
- `AddStaff` - إضافة موظف
- `UpdateStaff` - تحديث بيانات الموظف
- `RemoveStaff` - إزالة موظف
- `GetStaffByProperty` - موظفي العقار

### 6. التقارير والتحليلات (Reports & Analytics)
```typescript
// src/pages/property/ReportsAnalytics/
├── PropertyAnalytics.tsx   # تحليلات العقار
├── RevenueReports.tsx      # تقارير الإيرادات
├── OccupancyReports.tsx    # تقارير الإشغال
├── CustomerReports.tsx     # تقارير العملاء
└── PerformanceComparison.tsx # مقارنة الأداء
```

**الوظائف المطلوبة:**
- `GetPropertyPerformance` - أداء العقار
- `GetRevenueReport` - تقرير الإيرادات
- `GetOccupancyReport` - تقرير الإشغال
- `GetPropertyPerformanceComparison` - مقارنة الأداء

## 🔧 المكونات التقنية المطلوبة

### 1. إدارة الحالة (State Management)
```typescript
// Zustand stores
├── authStore.ts            # حالة المصادقة
├── userStore.ts            # حالة المستخدمين
├── propertyStore.ts        # حالة العقارات
├── bookingStore.ts         # حالة الحجوزات
└── notificationStore.ts    # حالة الإشعارات
```

### 2. خدمات API (API Services)
```typescript
// src/services/
├── api.ts                  # إعدادات API الأساسية
├── auth.service.ts         # خدمات المصادقة
├── admin.service.ts        # خدمات الإدارة
├── property.service.ts     # خدمات العقارات
├── booking.service.ts      # خدمات الحجوزات
└── upload.service.ts       # خدمات رفع الملفات
```

### 3. مكونات UI قابلة للإعادة الاستخدام
```typescript
// src/components/ui/
├── Button.tsx              # أزرار
├── Input.tsx               # حقول الإدخال
├── Modal.tsx               # النوافذ المنبثقة
├── DataTable.tsx           # جداول البيانات
├── Charts/                 # الرسوم البيانية
├── Forms/                  # النماذج
├── DatePicker.tsx          # منتقي التاريخ
├── ImageUpload.tsx         # رفع الصور
└── StatusBadge.tsx         # شارات الحالة
```

### 4. التوجيه والتنقل (Routing)
```typescript
// src/routes/
├── AdminRoutes.tsx         # مسارات الإدارة
├── PropertyRoutes.tsx      # مسارات العقارات
├── ProtectedRoute.tsx      # المسارات المحمية
└── RoleBasedRoute.tsx      # المسارات حسب الدور
```

## 📊 مكتبات إضافية مقترحة

### 1. مكتبات أساسية
```json
{
  "dependencies": {
    "react": "^18.2.0",
    "react-dom": "^18.2.0",
    "typescript": "^5.0.0",
    "vite": "^4.4.0",
    "react-router-dom": "^6.15.0",
    "zustand": "^4.4.0",
    "axios": "^1.5.0",
    "react-hook-form": "^7.45.0",
    "react-query": "^3.39.0"
  }
}
```

### 2. مكتبات UI والتصميم
```json
{
  "dependencies": {
    "tailwindcss": "^3.3.0",
    "headlessui": "^1.7.0",
    "heroicons": "^2.0.0",
    "react-datepicker": "^4.16.0",
    "react-dropzone": "^14.2.0"
  }
}
```

### 3. مكتبات الرسوم البيانية
```json
{
  "dependencies": {
    "recharts": "^2.8.0",
    "chart.js": "^4.4.0",
    "react-chartjs-2": "^5.2.0"
  }
}
```

### 4. مكتبات إضافية مهمة
```json
{
  "dependencies": {
    "react-table": "^7.8.0",
    "react-select": "^5.7.0",
    "react-toastify": "^9.1.0",
    "date-fns": "^2.30.0",
    "lodash": "^4.17.0"
  }
}
```

## 🚀 وظائف إضافية مهمة (اختيارية)

### 1. إدارة الإشعارات المتقدمة
```typescript
// GetNotificationsByType - تصنيف الإشعارات
// CreateBulkNotifications - إشعارات جماعية
// ScheduleNotifications - جدولة الإشعارات
```

### 2. تحليلات متقدمة
```typescript
// GetCustomerRetentionAnalysis - تحليل الاحتفاظ بالعملاء
// GetSeasonalTrends - الاتجاهات الموسمية
// GetCompetitorAnalysis - تحليل المنافسين
```

### 3. إدارة المحتوى
```typescript
// CreateContentTemplate - قوالب المحتوى
// ManageEmailTemplates - قوالب البريد الإلكتروني
// CreatePromotionalCampaigns - الحملات الترويجية
```

### 4. تقارير مخصصة
```typescript
// CreateCustomReport - تقارير مخصصة
// SaveReportTemplate - حفظ قوالب التقارير
// ScheduleReportDelivery - جدولة تسليم التقارير
```

## 🛡️ اعتبارات الأمان والأداء

### 1. الأمان
- تشفير البيانات الحساسة
- تحديد الصلاحيات حسب الأدوار
- تسجيل جميع العمليات الحساسة
- حماية ضد CSRF و XSS

### 2. الأداء
- تحميل البيانات بشكل تدريجي (Lazy Loading)
- تخزين مؤقت للبيانات (Caching)
- ضغط الصور والملفات
- تحسين الاستعلامات

### 3. تجربة المستخدم
- واجهة مستخدم متجاوبة
- تحميل سريع للصفحات
- رسائل خطأ واضحة
- إشعارات فورية للعمليات

هذه الهيكلة توفر نظام إدارة شامل ومتكامل يغطي جميع احتياجات الإدارة وملاك العقارات مع إمكانية التوسع والتطوير المستقبلي.