namespace JGP.NoteMaster.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using noteMaster.Packages;
    using Services;
    using Web.Models;

    /// <summary>
    ///     Class NoteController.
    ///     Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiVersion("1")]
    [ApiController]
    [Route("v{version:apiVersion}/notes")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class NoteController : ControllerBase
    {
        /// <summary>
        ///     The note service
        /// </summary>
        private readonly INoteService _noteService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NoteController" /> class.
        /// </summary>
        /// <param name="noteService">The note service.</param>
        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        /// <summary>
        ///     Helloes this instance.
        /// </summary>
        /// <returns>System.String.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<string> Hello()
        {
            await Task.Delay(1000);
            return "Hello World!";
        }

        #region CATEGORIES

        /// <summary>
        ///     Create category as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        public async Task<DataResult> CreateCategoryAsync([Required] CategoryModel model)
        {
            return await _noteService.CreateCategoryAsync(model.GetCreateCommand());
        }

        /// <summary>
        ///     Get categories as an asynchronous operation.
        /// </summary>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        [HttpGet("categories")]
        [ProducesResponseType(typeof(List<CategoryModel>), StatusCodes.Status200OK)]
        public async Task<List<CategoryModel>> GetCategoriesAsync()
        {
            var categories = await _noteService.GetCategoriesAsync();
            return categories.Select(x => new CategoryModel(x)).ToList();
        }

        #endregion

        #region NOTES

        /// <summary>
        ///     Create note as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        [HttpPost("note")]
        [ProducesResponseType(typeof(DataResult), StatusCodes.Status200OK)]
        public async Task<DataResult> CreateNoteAsync([Required] NoteModel model)
        {
            return await _noteService.CreateNoteAsync(model.GetCreateCommand());
        }

        /// <summary>
        ///     Get note by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>A Task&lt;NoteModel&gt; representing the asynchronous operation.</returns>
        [HttpGet("note/{noteId:guid}")]
        [ProducesResponseType(typeof(NoteModel), StatusCodes.Status200OK)]
        public async Task<NoteModel> GetNoteByIdAsync([Required] Guid noteId)
        {
            var note = await _noteService.GetNoteByIdAsync(noteId);
            return new NoteModel(note);
        }

        /// <summary>
        ///     Get notes by tag as an asynchronous operation.
        /// </summary>
        /// <param name="tagId">The tag identifier.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        [HttpGet("notes/{tagId:guid}")]
        [ProducesResponseType(typeof(List<NoteModel>), StatusCodes.Status200OK)]
        public async Task<List<NoteModel>> GetNotesByTagAsync([Required] Guid tagId)
        {
            var notes = await _noteService.GetNotesByCategoryAsync(tagId);
            return notes.Select(x => new NoteModel(x)).ToList();
        }

        /// <summary>
        ///     Update note as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        [HttpPut("note")]
        [ProducesResponseType(typeof(DataResult), StatusCodes.Status200OK)]
        public async Task<DataResult> UpdateNoteAsync([Required] NoteModel model)
        {
            return await _noteService.UpdateNoteAsync(model.GetUpdateCommand());
        }

        /// <summary>
        ///     Delete note as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        [HttpDelete("note")]
        [ProducesResponseType(typeof(DataResult), StatusCodes.Status200OK)]
        public async Task<DataResult> DeleteNoteAsync([Required] NoteModel model)
        {
            return await _noteService.DeleteNoteAsync(model.Id);
        }

        #endregion
    }
}