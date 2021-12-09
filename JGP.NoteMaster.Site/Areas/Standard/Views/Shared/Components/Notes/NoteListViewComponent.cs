namespace JGP.NoteMaster.Site.Areas.Standard.Views.Shared.Components.Notes
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Web.Proxy;

    /// <summary>
    /// Class NoteListViewComponent.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.ViewComponent" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ViewComponent" />
    [ViewComponent(Name = "NoteList")]
    public class NoteListViewComponent : ViewComponent
    {
        /// <summary>
        /// The note API helper
        /// </summary>
        private readonly INoteApiHelper _noteApiHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteListViewComponent"/> class.
        /// </summary>
        /// <param name="noteApiHelper">The note API helper.</param>
        public NoteListViewComponent(INoteApiHelper noteApiHelper)
        {
            _noteApiHelper = noteApiHelper;
        }

        /// <summary>
        /// Invoke as an asynchronous operation.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>A Task&lt;IViewComponentResult&gt; representing the asynchronous operation.</returns>
        public async Task<IViewComponentResult> InvokeAsync(Guid categoryId)
        {
            var noteList = await _noteApiHelper.GetNotesByCategoryAsync(categoryId);
            return View("NoteList", noteList);
        }
    }
}
