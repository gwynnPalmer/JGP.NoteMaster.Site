namespace JGP.NoteMaster.Data.EntityFramework.Mapping
{
    using Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    ///     Maps the specified entity of type Note.
    /// </summary>
    internal class NoteMap : IEntityTypeConfiguration<Note>
    {
        /// <summary>
        ///     Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties

            // Table & Column Mappings
            builder.ToTable("Notes", "dbo");
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.TagId).HasColumnName("TagId");
            builder.Property(x => x.Body).HasColumnName("Body");

            // Relationships
            builder.HasOne(x => x.Tag)
                .WithMany(x => x.Notes)
                .HasForeignKey(x => x.TagId);
        }
    }
}