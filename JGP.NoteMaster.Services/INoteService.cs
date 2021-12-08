namespace JGP.NoteMaster.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core;
    using Core.Commands;
    using noteMaster.Packages;

    /// <summary>
    ///     Interface INoteService
    ///     Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface INoteService : IDisposable
    {
        /// <summary>
        ///     Create note as an asynchronous operation.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        Task<DataResult> CreateNoteAsync(NoteCreateCommand command);

        /// <summary>
        ///     Get note as an asynchronous operation.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>A Task&lt;Note&gt; representing the asynchronous operation.</returns>
        Task<Note> GetNoteByIdAsync(Guid noteId);

        /// <summary>
        ///     Update note as an asynchronous operation.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        Task<DataResult> UpdateNoteAsync(NoteUpdateCommand command);

        /// <summary>
        ///     Delete note as an asynchronous operation.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        Task<DataResult> DeleteNoteAsync(Guid noteId);

        /// <summary>
        ///     Get categories as an asynchronous operation.
        /// </summary>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        Task<List<Category>> GetCategoriesAsync();

        /// <summary>
        ///     Creates the category asynchronous.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>Task&lt;DataResult&gt;.</returns>
        Task<DataResult> CreateCategoryAsync(CategoryCreateCommand command);

        /// <summary>
        ///     Gets the category by identifier asynchronous.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>Task&lt;Category&gt;.</returns>
        Task<Category> GetCategoryByIdAsync(Guid categoryId);

        /// <summary>
        ///     Updates the category asynchronous.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>Task&lt;DataResult&gt;.</returns>
        Task<DataResult> UpdateCategoryAsync(CategoryUpdateCommand command);

        /// <summary>
        ///     Deletes the category asynchronous.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>Task&lt;DataResult&gt;.</returns>
        Task<DataResult> DeleteCategoryAsync(Guid categoryId);

        /// <summary>
        ///     Get notes by tag as an asynchronous operation.
        /// </summary>
        /// <param name="tagId">The tag identifier.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        Task<List<Note>> GetNotesByCategoryAsync(Guid tagId);
    }
}