namespace JGP.NoteMaster.Site.Areas.Standard.Views.Shared.Components.Categories
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Web.Proxy;

    /// <summary>
    /// Class CategoryListViewComponent.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.ViewComponent" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ViewComponent" />
    [ViewComponent(Name = "CategoryList")]
    public class CategoryListViewComponent : ViewComponent
    {
        /// <summary>
        /// The note API helper
        /// </summary>
        private readonly INoteApiHelper _noteApiHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryListViewComponent"/> class.
        /// </summary>
        /// <param name="noteApiHelper">The note API helper.</param>
        public CategoryListViewComponent(INoteApiHelper noteApiHelper)
        {
            _noteApiHelper = noteApiHelper;
        }

        /// <summary>
        /// Invoke as an asynchronous operation.
        /// </summary>
        /// <returns>A Task&lt;IViewComponentResult&gt; representing the asynchronous operation.</returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoryList = await _noteApiHelper.GetCategoriesAsync();
            return View("CategoryList", categoryList);
        }
    }
}
