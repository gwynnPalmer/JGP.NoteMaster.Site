namespace JGP.noteMaster.Packages
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    ///     Class DataResult.
    /// </summary>
    public class DataResult
    {
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is success.
        /// </summary>
        /// <value><c>true</c> if this instance is success; otherwise, <c>false</c>.</value>
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }

        /// <summary>
        ///     Gets or sets the affected count.
        /// </summary>
        /// <value>The affected count.</value>
        [JsonPropertyName("affectedCount")]
        public int AffectedCount { get; set; }

        /// <summary>
        ///     Gets or sets the information items.
        /// </summary>
        /// <value>The information items.</value>
        [JsonPropertyName("infoItems")]
        public Dictionary<string, string> InfoItems { get; set; }

        /// <summary>
        ///     Gets or sets the errors.
        /// </summary>
        /// <value>The errors.</value>
        [JsonPropertyName("errors")]
        public List<KeyValuePair<string, string>> Errors { get; set; }

        /// <summary>
        ///     Gets the success data result.
        /// </summary>
        /// <param name="affectedCount">The affected count.</param>
        /// <returns>DataResult.</returns>
        public static DataResult GetSuccessDataResult(int affectedCount)
        {
            return new DataResult
            {
                IsSuccess = true,
                AffectedCount = affectedCount
            };
        }

        /// <summary>
        ///     Gets the not found data result.
        /// </summary>
        /// <param name="notFoundMessage">The not found message.</param>
        /// <returns>DataResult.</returns>
        public static DataResult GetNotFoundDataResult(string notFoundMessage = null)
        {
            return new DataResult
            {
                IsSuccess = false,
                AffectedCount = 0,
                Errors = new List<KeyValuePair<string, string>>
                {
                    new("NotFound", notFoundMessage)
                }
            };
        }

        /// <summary>
        ///     Gets the error data result.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>DataResult.</returns>
        public static DataResult GetErrorDataResult(Exception exception)
        {
            return new DataResult
            {
                IsSuccess = false,
                AffectedCount = 0,
                Errors = new List<KeyValuePair<string, string>>
                {
                    new(nameof(exception), exception.Message)
                }
            };
        }

        /// <summary>
        ///     Gets the error data result.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>JGP.noteMaster.Packages.DataResult.</returns>
        public static DataResult GetErrorDataResult(string errorMessage)
        {
            return new DataResult
            {
                IsSuccess = false,
                AffectedCount = 0,
                Errors = new List<KeyValuePair<string, string>>
                {
                    new("Error", errorMessage)
                }
            };
        }
    }
}