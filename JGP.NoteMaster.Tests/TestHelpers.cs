namespace JGP.NoteMaster.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Bogus;
    using Core.Commands;
    using Data.EntityFramework;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Services;

    /// <summary>
    ///     Class TestHelpers.
    /// </summary>
    internal static class TestHelpers
    {
        /// <summary>
        ///     Gets the note create command.
        /// </summary>
        /// <param name="exists">if set to <c>true</c> [exists].</param>
        /// <param name="categoryId">The tag identifier.</param>
        /// <returns>NoteCreateCommand.</returns>
        public static NoteCreateCommand GetNoteCreateCommand(bool exists = false, Guid? categoryId = null)
        {
            var faker = new Faker<NoteCreateCommand>()
                .RuleFor(x => x.Id, exists ? Guid.NewGuid() : null)
                .RuleFor(x => x.BodyText, y => y.Lorem.Sentence());

            var command = faker.Generate();

            if (categoryId.HasValue)
            {
                command.CategoryId = (Guid)categoryId;
            }

            return command;
        }

        /// <summary>
        ///     Gets the note update command.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="categoryId">The tag identifier.</param>
        /// <returns>NoteUpdateCommand.</returns>
        public static NoteUpdateCommand GetNoteUpdateCommand(Guid? id = null, Guid? categoryId = null)
        {
            var faker = new Faker<NoteUpdateCommand>()
                .RuleFor(x => x.BodyText, y => y.Lorem.Sentence());

            var command = faker.Generate();

            if (id.HasValue)
            {
                command.Id = (Guid)id;
            }

            if (categoryId.HasValue)
            {
                command.CategoryId = (Guid)categoryId;
            }

            return command;
        }

        /// <summary>
        ///     Gets the category create command.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="notes">The notes.</param>
        /// <returns>CategoryCreateCommand.</returns>
        public static CategoryCreateCommand GetCategoryCreateCommand(Guid? id = null,
            List<NoteCreateCommand> notes = null)
        {
            var faker = new Faker<CategoryCreateCommand>()
                .RuleFor(x => x.Name, y => y.Lorem.Word())
                .RuleFor(x => x.Notes, notes);

            var command = faker.Generate();

            command.Id = id ?? Guid.NewGuid();

            return command;
        }

        /// <summary>
        ///     Gets the category update command.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="notes">The notes.</param>
        /// <returns>CategoryUpdateCommand.</returns>
        public static CategoryUpdateCommand GetCategoryUpdateCommand(Guid? id = null,
            List<NoteCreateCommand> notes = null)
        {
            var faker = new Faker<CategoryUpdateCommand>()
                .RuleFor(x => x.Name, y => y.Lorem.Word())
                .RuleFor(x => x.Notes, notes);

            var command = faker.Generate();

            command.Id = id ?? Guid.NewGuid();

            return command;
        }

        /// <summary>
        ///     Builds the context.
        /// </summary>
        /// <returns>NoteContext.</returns>
        public static NoteContext BuildContext()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };

            var connection = new SqliteConnection(connectionStringBuilder.ToString());

            var options = new DbContextOptionsBuilder<NoteContext>()
                .UseSqlite(connection)
                .Options;

            return new NoteContext(options);
        }

        /// <summary>
        ///     Initiates the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool InitiateDatabase(this NoteContext context)
        {
            var state = context.Database.GetDbConnection().State;
            if (state != ConnectionState.Open) context.Database.OpenConnection();
            return context.Database.EnsureCreated();
        }

        /// <summary>
        ///     Gets the service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        /// <returns>IDisposable.</returns>
        public static IDisposable GetService(this NoteContext context)
        {
                return new NoteService(context);
        }
    }
}