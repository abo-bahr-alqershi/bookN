erDiagram
    %% ----- العلاقات الجديدة -----
    properties ||--o{ property_images : "لها"
    units ||--o{ property_images : "لها"

    %% ----- الكيانات الجديدة -----
    property_images {
        string image_id PK
        string property_id FK NULL
        string unit_id FK NULL
        string name
        string url
        long size_bytes
        string type
        string category
        string caption
        string alt_text
        json tags
        boolean is_main
        int sort_order
        int views
        int downloads
        datetime uploaded_at
        int display_order
        string status
        boolean is_main_image
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }

    %% ----- إضافة خصائص الحذف الناعم لجميع الجداول -----
    users {
        string user_id PK
        string name
        string email
        string password
        string phone
        datetime created_at
        boolean is_active
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }
    
    roles {
        string role_id PK
        string name "admin, owner, manager, customer"
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }
    
    user_roles {
        string user_id FK
        string role_id FK
        datetime assigned_at
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }
    
    property_types {
        string type_id PK
        string name "فندق، شاليه، استراحة، فيلا، شقة"
        string description
        json default_amenities "مرافق افتراضية للنوع"
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }
    
    properties {
        string property_id PK
        string owner_id FK
        string type_id FK
        string name
        string address
        string city
        decimal latitude
        decimal longitude
        integer star_rating
        text description
        boolean is_approved
        datetime created_at
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }
    
    unit_types {
        string unit_type_id PK
        string property_type_id FK
        string name "غرفة مزدوجة، جناح، شاليه كامل، فيلا"
        integer max_capacity
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }
    
    units {
        string unit_id PK
        string property_id FK
        string unit_type_id FK
        string name "الوحدة A، الجناح الملكي"
        decimal base_price
        json custom_features "مسبح خاص، شرفة"
        boolean is_available
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }
    
    bookings {
        string booking_id PK
        string user_id FK
        string unit_id FK
        datetime check_in
        datetime check_out
        integer guests_count
        decimal total_price
        string status "مؤكّد، معلّق، ملغى"
        datetime booked_at
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }
    
    payments {
        string payment_id PK
        string booking_id FK
        decimal amount
        string method "بطاقة، نقدي، محفظة"
        string transaction_id
        string status "ناجح، فاشل، معلّق"
        datetime payment_date
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }
    
    property_services {
        string service_id PK
        string property_id FK
        string name "نقل من المطار، سبا، غسيل ملابس"
        decimal price
        string pricing_model "ثابت، للشخص، للليلة"
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }
    
    booking_services {
        string booking_id FK
        string service_id FK
        integer quantity
        decimal total_price
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }
    
    property_policies {
        string policy_id PK
        string property_id FK
        string policy_type "إلغاء، دخول، أطفال، حيوانات"
        text description
        json rules "غرامة 50% إذا ألغيت قبل 3 أيام"
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }
    
    staff {
        string staff_id PK
        string user_id FK
        string property_id FK
        string position "مدير، موظف استقبال، نظافة"
        json permissions
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }
    
    reviews {
        string review_id PK
        string booking_id FK
        integer cleanliness
        integer service
        integer location
        integer value
        text comment
        datetime created_at
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }
    
    admin_actions {
        string action_id PK
        string admin_id FK
        string target_id
        string target_type "property, user, booking"
        string action_type "create, update, delete, approve"
        datetime timestamp
        json changes
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }

    %% ----- العلاقات الحالية -----
    property_types ||--o{ property_type_amenities : "يحدد"
    amenities }o--|| property_type_amenities : "مرتبط"
    property_type_amenities }o--o{ property_amenities : "مستند_على"
    users ||--o{ user_roles : "يحدد"
    users ||--o{ properties : "يملك"
    users ||--o{ staff : "يعمل_كـ"
    property_types ||--o{ properties : "يصنف"
    properties ||--o{ units : "يحتوي"
    properties ||--o{ property_services : "يقدم"
    units ||--o{ bookings : "محجوزة_في"
    bookings ||--o{ payments : "لها"
    bookings ||--o{ booking_services : "تتضمن"
    properties ||--o{ reviews : "تتلقى"
    properties ||--o{ property_policies : "تطبق"
    staff }o--|| properties : "يدير"
    user_roles }o--|| roles : "مرتبط"
    booking_services }o--|| property_services : "تستخدم"
    reviews }o--|| bookings : "مبنية_على"
    units }o--|| unit_types : "ينتمي_إلى"
    property_types ||--o{ unit_types : "يحدد"

    %% ----- بقية الجداول -----
    property_type_amenities {
        string pta_id PK
        string property_type_id FK
        string amenity_id FK
        boolean is_default
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }

    amenities {
        string amenity_id PK
        string name "واي فاي، مسبح، جيم، إفطار"
        string description
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }

    property_amenities {
        string pa_id PK
        string property_id FK
        string pta_id FK
        boolean is_available
        decimal extra_cost
        boolean is_deleted DEFAULT false
        datetime deleted_at NULL
    }