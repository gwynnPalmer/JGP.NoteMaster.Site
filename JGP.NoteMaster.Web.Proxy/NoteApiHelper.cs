namespace JGP.NoteMaster.Web.Proxy
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Models;
    using noteMaster.Packages;

    /// <summary>
    ///     Class NoteApiHelper.
    /// </summary>
    public class NoteApiHelper : INoteApiHelper
    {
        /// <summary>
        ///     The note path
        /// </summary>
        private const string CategoryPath = "v1/notes/categories";

        /// <summary>
        ///     The single note path
        /// </summary>
        private const string SingleNotePath = "v1/notes/note";

        /// <summary>
        ///     The multiple note path
        /// </summary>
        private const string MultipleNotePath = "v1/notes/notes";

        /// <summary>
        ///     The HTTP client
        /// </summary>
        private static HttpClient _httpClient;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NoteApiHelper" /> class.
        /// </summary>
        /// <param name="apiEndpoint">The API endpoint.</param>
        public NoteApiHelper(string apiEndpoint)
        {
            if (_httpClient != null) return;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiEndpoint)
            };
        }

        /// <summary>
        ///     Create category as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        public async Task<DataResult> CreateCategoryAsync(CategoryModel model)
        {
            var response = await SendPostMessageAsync(model, CategoryPath);
            return await response.Content.ReadFromJsonAsync<DataResult>();
        }

        /// <summary>
        ///     Get categories as an asynchronous operation.
        /// </summary>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<CategoryModel>> GetCategoriesAsync()
        {
            var response = await SendGetMessageAsync(CategoryPath);
            return await response.Content.ReadFromJsonAsync<List<CategoryModel>>();
        }

        /// <summary>
        ///     Update category as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        public async Task<DataResult> UpdateCategoryAsync(CategoryModel model)
        {
            var response = await SendPutMessageAsync(model, CategoryPath);
            return await response.Content.ReadFromJsonAsync<DataResult>();
        }

        /// <summary>
        ///     Delete category as an asynchronous operation.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        public async Task<DataResult> DeleteCategoryAsync(Guid categoryId)
        {
            var response = await SendDeleteMessageAsync($"{CategoryPath}/{categoryId}");
            return await response.Content.ReadFromJsonAsync<DataResult>();
        }

        /// <summary>
        ///     Create note as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        public async Task<DataResult> CreateNoteAsync(NoteModel model)
        {
            var response = await SendPostMessageAsync(model, SingleNotePath);
            return await response.Content.ReadFromJsonAsync<DataResult>();
        }

        /// <summary>
        ///     Get note by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>A Task&lt;NoteModel&gt; representing the asynchronous operation.</returns>
        public async Task<NoteModel> GetNoteByIdAsync(Guid noteId)
        {
            var response = await SendGetMessageAsync($"{SingleNotePath}/{noteId}");
            return await response.Content.ReadFromJsonAsync<NoteModel>();
        }

        /// <summary>
        ///     Get notes by category as an asynchronous operation.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<NoteModel>> GetNotesByCategoryAsync(Guid categoryId)
        {
            var response = await SendGetMessageAsync($"{MultipleNotePath}/{categoryId}");
            return await response.Content.ReadFromJsonAsync<List<NoteModel>>();
        }

        /// <summary>
        ///     Update note as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        public async Task<DataResult> UpdateNoteAsync(NoteModel model)
        {
            var response = await SendPutMessageAsync(model, SingleNotePath);
            return await response.Content.ReadFromJsonAsync<DataResult>();
        }

        /// <summary>
        ///     Delete note as an asynchronous operation.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>A Task&lt;DataResult&gt; representing the asynchronous operation.</returns>
        public async Task<DataResult> DeleteNoteAsync(Guid noteId)
        {
            var response = await SendDeleteMessageAsync($"{SingleNotePath}/{noteId}");
            return await response.Content.ReadFromJsonAsync<DataResult>();
        }

        #region HELPERS

        /// <summary>
        ///     send post message as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="route">The route.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        private async Task<HttpResponseMessage> SendPostMessageAsync(object model, string route)
        {
            try
            {
                var json = JsonSerializer.Serialize(model);
                var request = new HttpRequestMessage(HttpMethod.Post, route)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/Json")
                };

                return await _httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     send put message as an asynchronous operation.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="route">The route.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        private async Task<HttpResponseMessage> SendPutMessageAsync(object model, string route)
        {
            try
            {
                var json = JsonSerializer.Serialize(model);
                var request = new HttpRequestMessage(HttpMethod.Put, route)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/Json")
                };

                return await _httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        ///     send get message as an asynchronous operation.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        private async Task<HttpResponseMessage> SendGetMessageAsync(string route)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, route);

                return await _httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        ///     send delete message as an asynchronous operation.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        private async Task<HttpResponseMessage> SendDeleteMessageAsync(string route)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, route);

                return await _httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        #endregion
    }
}