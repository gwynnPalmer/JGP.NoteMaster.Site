namespace JGP.NoteMaster.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Core.Commands;
    using Data.EntityFramework;
    using Microsoft.EntityFrameworkCore;
    using noteMaster.Packages;

    /// <summary>
    ///     Class NoteService.
    ///     Implements the <see cref="JGP.NoteMaster.Services.INoteService" />
    /// </summary>
    /// <seealso cref="JGP.NoteMaster.Services.INoteService" />
    public class NoteService : INoteService
    {
        /// <summary>
        ///     The note context
        /// </summary>
        private readonly INoteContext _noteContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NoteService" /> class.
        /// </summary>
        /// <param name="noteContext">The note context.</param>
        public NoteService(INoteContext noteContext)
        {
            _noteContext = noteContext;
        }

        /// <summary>
        ///     Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            _noteContext.Dispose();
        }


        #region CATEGORIES

        /// <summary>
        ///     Create category as an asynchronous operation.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        public async Task<DataResult> CreateCategoryAsync(CategoryCreateCommand command)
        {
            try
            {
                var category = new Category(command);

                await _noteContext.Categories.AddAsync(category);
                var affectedRecords = await _noteContext.SaveChangesAsync();

                return DataResult.GetSuccessDataResult(affectedRecords);
            }
            catch (Exception e)
            {
                return DataResult.GetErrorDataResult(e);
            }
        }

        /// <summary>
        ///     Get categories as an asynchronous operation.
        /// </summary>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _noteContext.Categories
                .Include(x => x.Tags)
                .ThenInclude(x => x.Notes)
                .AsNoTracking().ToListAsync();
        }

        /// <summary>
        ///     Get category by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>A Task&lt;Category&gt; representing the asynchronous operation.</returns>
        public async Task<Category> GetCategoryByIdAsync(Guid categoryId)
        {
            return await _noteContext.Categories
                .Include(x => x.Tags)
                .ThenInclude(x => x.Notes)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == categoryId);
        }

        /// <summary>
        ///     Update category as an asynchronous operation.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        public async Task<DataResult> UpdateCategoryAsync(CategoryUpdateCommand command)
        {
            try
            {
                var category = await _noteContext.Categories
                    .Include(x => x.Tags)
                    .ThenInclude(x => x.Notes)
                    .FirstOrDefaultAsync(x => x.Id == command.Id);
                if (category == null)
                    return DataResult.GetNotFoundDataResult($"Cannot locate existing category for ID: '{command.Id}'");

                category.Update(command);
                var affectedRecords = await _noteContext.SaveChangesAsync();
                return DataResult.GetSuccessDataResult(affectedRecords);
            }
            catch (Exception e)
            {
                return DataResult.GetErrorDataResult(e);
            }
        }

        /// <summary>
        ///     Delete category as an asynchronous operation.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        public async Task<DataResult> DeleteCategoryAsync(Guid categoryId)
        {
            try
            {
                var category = await _noteContext.Categories
                    .Include(x => x.Tags)
                    .ThenInclude(x => x.Notes)
                    .FirstOrDefaultAsync(x => x.Id == categoryId);
                if (category == null)
                    return DataResult.GetNotFoundDataResult($"Cannot locate existing category for ID: '{categoryId}'");

                _noteContext.Categories.Remove(category);
                var affectedCount = await _noteContext.SaveChangesAsync();

                return DataResult.GetSuccessDataResult(affectedCount);
            }
            catch (Exception e)
            {
                return DataResult.GetErrorDataResult(e);
            }
        }

        #endregion

        #region TAGS

        /// <summary>
        ///     Get tags by category as an asynchronous operation.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<Tag>> GetTagsByCategoryAsync(Guid categoryId)
        {
            return await _noteContext.Tags
                .Where(x => x.CategoryId == categoryId)
                .Include(x => x.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        #endregion

        #region NOTES

        /// <summary>
        ///     Create note as an asynchronous operation.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        public async Task<DataResult> CreateNoteAsync(NoteCreateCommand command)
        {
            try
            {
                var note = new Note(command);

                await _noteContext.Notes.AddAsync(note);
                var affectedRecords = await _noteContext.SaveChangesAsync();

                return DataResult.GetSuccessDataResult(affectedRecords);
            }
            catch (Exception e)
            {
                return DataResult.GetErrorDataResult(e);
            }
        }

        /// <summary>
        ///     Get note as an asynchronous operation.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>A Task&lt;Note&gt; representing the asynchronous operation.</returns>
        public async Task<Note> GetNoteByIdAsync(Guid noteId)
        {
            try
            {
                return await _noteContext.Notes
                    .Include(x => x.Tag)
                    .ThenInclude(x => x.Category)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == noteId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        ///     Get notes by tag as an asynchronous operation.
        /// </summary>
        /// <param name="tagId">The tag identifier.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<Note>> GetNotesByTagAsync(Guid tagId)
        {
            return await _noteContext.Notes
                .Include(x => x.Tag)
                .ThenInclude(x => x.Category)
                .AsNoTracking()
                .Where(x => x.TagId == tagId)
                .ToListAsync();
        }

        /// <summary>
        ///     Update note as an asynchronous operation.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        public async Task<DataResult> UpdateNoteAsync(NoteUpdateCommand command)
        {
            try
            {
                var note = await _noteContext.Notes
                    .Include(x => x.Tag)
                    .ThenInclude(x => x.Category)
                    .FirstOrDefaultAsync(x => x.Id == command.Id);
                if (note == null)
                    return DataResult.GetNotFoundDataResult($"Cannot locate existing note for ID: '{command.Id}'");


                note.Update(command);
                var affectedRecords = await _noteContext.SaveChangesAsync();
                return DataResult.GetSuccessDataResult(affectedRecords);
            }
            catch (Exception e)
            {
                return DataResult.GetErrorDataResult(e);
            }
        }

        /// <summary>
        ///     Delete note as an asynchronous operation.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        public async Task<DataResult> DeleteNoteAsync(Guid noteId)
        {
            try
            {
                var note = await _noteContext.Notes
                    .FirstOrDefaultAsync(x => x.Id == noteId);
                if (note == null)
                    return DataResult.GetNotFoundDataResult($"Cannot locate existing note for ID: '{noteId}'");

                _noteContext.Notes.Remove(note);
                var affectedCount = await _noteContext.SaveChangesAsync();

                return DataResult.GetSuccessDataResult(affectedCount);
            }
            catch (Exception e)
            {
                return DataResult.GetErrorDataResult(e);
            }
        }

        #endregion
    }
}