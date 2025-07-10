using System;
using System.Linq;
using AutoMapper;
using YemenBooking.Core.Entities;
using YemenBooking.Core.ValueObjects;
using YemenBooking.Application.DTOs;
using YemenBooking.Application.DTOs.Users;

namespace YemenBooking.Application.Mappings
{
    /// <summary>
    /// ملف تعريف الخرائط لجميع الاستعلامات
    /// Mapping profile for all query DTOs and their corresponding entities and value objects
    /// </summary>
    public class QueryMappingProfile : Profile
    {
        public QueryMappingProfile()
        {
            // Amenity mapping
            CreateMap<Amenity, AmenityDto>();

            // Booking mapping
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.Unit.Name));

            // Booking details including payments and services
            CreateMap<Booking, BookingDetailsDto>()
                .IncludeBase<Booking, BookingDto>()
                .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments))
                .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.BookingServices.Select(bs => bs.Service)));

            // Notification mapping
            CreateMap<Notification, NotificationDto>()
                .ForMember(dest => dest.RecipientName, opt => opt.MapFrom(src => src.Recipient.Name))
                .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.Name));

            // Payment mapping
            CreateMap<Payment, PaymentDto>();

            // Property mapping
            CreateMap<Property, PropertyDto>()
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.PropertyType.Name))
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner.Name));

            // Property details mapping
            CreateMap<Property, PropertyDetailsDto>()
                .IncludeBase<Property, PropertyDto>()
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.Units))
                .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src => src.Amenities.Select(pa => pa.PropertyTypeAmenity.Amenity)));

            // Property type mapping
            CreateMap<PropertyType, PropertyTypeDto>();

            // Staff mapping
            CreateMap<Staff, StaffDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.PropertyName, opt => opt.MapFrom(src => src.Property.Name));

            // Service mapping
            CreateMap<PropertyService, ServiceDto>()
                .ForMember(dest => dest.PropertyName, opt => opt.MapFrom(src => src.Property.Name));

            // Unit mapping
            CreateMap<Unit, UnitDto>()
                .ForMember(dest => dest.PropertyName, opt => opt.MapFrom(src => src.Property.Name))
                .ForMember(dest => dest.UnitTypeName, opt => opt.MapFrom(src => src.UnitType.Name))
                .ForMember(dest => dest.PricingMethod, opt => opt.MapFrom(src => src.PricingMethod));

            // User mapping
            CreateMap<User, UserDto>();

            // Role mapping
            CreateMap<Role, RoleDto>();

            // Money value object mapping
            CreateMap<Money, MoneyDto>();

            // Contact value object mapping
            CreateMap<Contact, ContactDto>();

            // Address value object mapping
            CreateMap<Address, AddressDto>();

            // Policy mapping
            CreateMap<PropertyPolicy, PolicyDto>();

            // Property image mapping
            CreateMap<PropertyImage, PropertyImageDto>();

            // Audit log mapping
            CreateMap<AuditLog, AuditLogDto>();

            // Report mapping
            CreateMap<Report, ReportDto>()
                .ForMember(dest => dest.ReporterUserName, opt => opt.MapFrom(src => src.ReporterUser.Name))
                .ForMember(dest => dest.ReportedUserName, opt => opt.MapFrom(src => src.ReportedUser != null ? src.ReportedUser.Name : string.Empty))
                .ForMember(dest => dest.ReportedPropertyName, opt => opt.MapFrom(src => src.ReportedProperty != null ? src.ReportedProperty.Name : string.Empty));
        }
    }
} 