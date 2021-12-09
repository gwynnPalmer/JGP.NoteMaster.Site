namespace JGP.NoteMaster.Web.Proxy
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using noteMaster.Packages;

    public interface INoteApiHelper
    {
        /// <summary>
        ///     Create category as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        Task<DataResult> CreateCategoryAsync(CategoryModel model);

        /// <summary>
        ///     Get categories as an asynchronous operation.
        /// </summary>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        Task<List<CategoryModel>> GetCategoriesAsync();

        /// <summary>
        ///     Update category as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        Task<DataResult> UpdateCategoryAsync(CategoryModel model);

        /// <summary>
        ///     Delete category as an asynchronous operation.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        Task<DataResult> DeleteCategoryAsync(Guid categoryId);

        /// <summary>
        ///     Create note as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        Task<DataResult> CreateNoteAsync(NoteModel model);

        /// <summary>
        ///     Get note by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>A Task&lt;NoteModel&gt; representing the asynchronous operation.</returns>
        Task<NoteModel> GetNoteByIdAsync(Guid noteId);

        /// <summary>
        ///     Get notes by category as an asynchronous operation.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        Task<List<NoteModel>> GetNotesByCategoryAsync(Guid categoryId);

        /// <summary>
        ///     Update note as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        Task<DataResult> UpdateNoteAsync(NoteModel model);

        /// <summary>
        ///     Delete note as an asynchronous operation.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        Task<DataResult> DeleteNoteAsync(Guid noteId);
    }
}