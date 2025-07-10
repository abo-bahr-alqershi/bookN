using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YemenBooking.Core.Entities;

namespace YemenBooking.Infrastructure.Data.Configurations;

/// <summary>
/// تكوين كيان نوع الحقل
/// FieldType entity configuration
/// </summary>
public class FieldTypeConfiguration : IEntityTypeConfiguration<FieldType>
{
    public void Configure(EntityTypeBuilder<FieldType> builder)
    {
        builder.ToTable("FieldTypes");

        builder.HasKey(ft => ft.Id);

        builder.Property(ft => ft.Id)
            .HasColumnName("FieldTypeId")
            .IsRequired();

        builder.Property(ft => ft.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(ft => ft.DeletedAt)
            .HasColumnType("datetime");

        builder.Property(ft => ft.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(ft => ft.DisplayName)
            .HasMaxLength(100);

        builder.Property(ft => ft.ValidationRules)
            .HasColumnType("nvarchar(max)");

        builder.HasIndex(ft => ft.Name)
            .IsUnique();

        builder.HasQueryFilter(ft => !ft.IsDeleted);
    }
} 