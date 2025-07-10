using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenBooking.Core.Entities;

namespace YemenBooking.Infrastructure.Data.Configurations;

/// <summary>
/// تكوين كيان المراجعة
/// Review entity configuration
/// </summary>
public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .IsRequired()
            .HasComment("معرف التقييم الفريد");

        builder.Property(r => r.BookingId)
            .IsRequired()
            .HasComment("معرف الحجز");

        builder.Property(r => r.Cleanliness)
            .IsRequired()
            .HasComment("تقييم النظافة");

        builder.Property(r => r.Service)
            .IsRequired()
            .HasComment("تقييم الخدمة");

        builder.Property(r => r.Location)
            .IsRequired()
            .HasComment("تقييم الموقع");

        builder.Property(r => r.Value)
            .IsRequired()
            .HasComment("تقييم القيمة");

        builder.Property(r => r.Comment)
            .HasColumnType("nvarchar(max)")
            .HasComment("تعليق التقييم");

        builder.Property(r => r.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired()
            .HasComment("تاريخ إنشاء التقييم");

        builder.Property(r => r.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false)
            .HasComment("حالة الحذف الناعم");

        builder.Property(r => r.DeletedAt)
            .HasColumnType("datetime")
            .HasComment("تاريخ الحذف");

        // Indexes
        builder.HasIndex(r => r.BookingId)
            .IsUnique()
            .HasDatabaseName("IX_Reviews_BookingId");

        builder.HasIndex(r => r.IsDeleted)
            .HasDatabaseName("IX_Reviews_IsDeleted");

        // Relationships
        builder.HasOne(r => r.Booking)
            .WithMany(b => b.Reviews)
            .HasForeignKey(r => r.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Property)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.BookingId);

        // Check constraints for rating values (1-5)
        builder.HasCheckConstraint("CK_Reviews_Cleanliness", "[Cleanliness] >= 1 AND [Cleanliness] <= 5");
        builder.HasCheckConstraint("CK_Reviews_Service", "[Service] >= 1 AND [Service] <= 5");
        builder.HasCheckConstraint("CK_Reviews_Location", "[Location] >= 1 AND [Location] <= 5");
        builder.HasCheckConstraint("CK_Reviews_Value", "[Value] >= 1 AND [Value] <= 5");

        builder.HasQueryFilter(r => !r.IsDeleted);
    }
}
