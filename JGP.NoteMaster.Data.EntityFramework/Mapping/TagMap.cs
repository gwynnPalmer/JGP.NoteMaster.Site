namespace JGP.NoteMaster.Data.EntityFramework.Mapping
{
    using Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    ///     Maps the specified entity of type Tag.
    /// </summary>
    internal class TagMap : IEntityTypeConfiguration<Tag>
    {
        /// <summary>
        ///     Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);

            // Table & Column Mappings
            builder.ToTable("Tags", "dbo");
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.CategoryId).HasColumnName("CategoryId");
            builder.Property(x => x.Name).HasColumnName("Name");

            // Relationships
            builder.HasOne(x => x.Category)
                .WithMany(x => x.Tags)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}