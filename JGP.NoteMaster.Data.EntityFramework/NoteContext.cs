namespace JGP.NoteMaster.Data.EntityFramework
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    /// <summary>
    ///     Interface INoteContext
    ///     Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface INoteContext : IDisposable
    {
        /// <summary>
        ///     Saves the changes.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int SaveChanges();

        /// <summary>
        ///     Saves the changes.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">if set to <c>true</c> [accept all changes on success].</param>
        /// <returns>System.Int32.</returns>
        int SaveChanges(bool acceptAllChangesOnSuccess);

        /// <summary>
        ///     Saves the changes asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());

        /// <summary>
        ///     Saves the changes asynchronous.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">if set to <c>true</c> [accept all changes on success].</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new());

        /// <summary>
        ///     Entries the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>EntityEntry&lt;TEntity&gt;.</returns>
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        ///     Entries the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>EntityEntry.</returns>
        EntityEntry Entry(object entity);

        #region DBSETS

        /// <summary>
        ///     Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        ///     Gets or sets the tags.
        /// </summary>
        /// <value>The tags.</value>
        public DbSet<Tag> Tags { get; set; }

        /// <summary>
        ///     Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public DbSet<Note> Notes { get; set; }

        #endregion
    }

    /// <summary>
    ///     Class NoteContext.
    ///     Implements the <see cref="Microsoft.EntityFrameworkCore.DbContext" />
    ///     Implements the <see cref="JGP.NoteMaster.Data.EntityFramework.INoteContext" />
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    /// <seealso cref="JGP.NoteMaster.Data.EntityFramework.INoteContext" />
    public class NoteContext : DbContext, INoteContext
    {
        /// <summary>
        ///     The connection string
        /// </summary>
        private readonly string _connectionString = @"Server=.;Database=NoteMaster;Trusted_Connection=True;";

        /// <summary>
        ///     Initializes a new instance of the <see cref="NoteContext" /> class.
        /// </summary>
        public NoteContext()
        {
            base.Database.SetCommandTimeout(1000);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NoteContext" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public NoteContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NoteContext" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public NoteContext(DbContextOptions<NoteContext> options) : base(options)
        {
        }

        /// <summary>
        ///     <para>
        ///         Override this method to configure the database (and other options) to be used for this context.
        ///         This method is called for each instance of the context that is created.
        ///         The base implementation does nothing.
        ///     </para>
        ///     <para>
        ///         In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may
        ///         not have been passed
        ///         to the constructor, you can use
        ///         <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        ///         the options have already been set, and skip some or all of the logic in
        ///         <see
        ///             cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />
        ///         .
        ///     </para>
        /// </summary>
        /// <param name="optionsBuilder">
        ///     A builder used to create or modify options for this context. Databases (and other extensions)
        ///     typically define extension methods on this object that allow you to configure the context.
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString,
                    x => x.EnableRetryOnFailure(3, TimeSpan.FromSeconds(3), null));
        }

        /// <summary>
        ///     Override this method to further configure the model that was discovered by convention from the entity types
        ///     exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting
        ///     model may be cached
        ///     and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">
        ///     The builder being used to construct the model for this context. Databases (and other extensions) typically
        ///     define extension methods on this object that allow you to configure aspects of the model that are specific
        ///     to a given database.
        /// </param>
        /// <remarks>
        ///     If a model is explicitly set on the options for this context (via
        ///     <see
        ///         cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />
        ///     )
        ///     then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tables.
            // Views.
        }

        #region DBSETS

        /// <summary>
        ///     Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        ///     Gets or sets the tags.
        /// </summary>
        /// <value>The tags.</value>
        public DbSet<Tag> Tags { get; set; }

        /// <summary>
        ///     Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public DbSet<Note> Notes { get; set; }

        #endregion
    }
}