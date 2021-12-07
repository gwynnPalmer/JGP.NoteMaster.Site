namespace JGP.NoteMaster.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using Core;
    using Core.Commands;

    /// <summary>
    ///     A class representing a view model object for a tag object.
    /// </summary>
    public class TagModel
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TagModel" /> class.
        /// </summary>
        public TagModel()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TagModel" /> class.
        /// </summary>
        /// <param name="tag">The Tag.</param>
        public TagModel(Tag tag)
        {
            Id = tag.Id;
            CategoryId = tag.CategoryId;
            Name = tag.Name;
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        [Required]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the category id.
        /// </summary>
        [Required]
        [JsonPropertyName("categoryId")]
        public Guid CategoryId { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }


        /// <summary>
        ///     Gets the Tag Create Command
        /// </summary>
        /// <returns>A Tag Create Command populated with the current model values.</returns>
        public TagCreateCommand GetCreateCommand()
        {
            return new TagCreateCommand
            {
                Id = Id,
                CategoryId = CategoryId,
                Name = Name
            };
        }


        /// <summary>
        ///     Gets the Tag Update Command
        /// </summary>
        /// <returns>A Tag Update Command populated with the current model values.</returns>
        public TagUpdateCommand GetUpdateCommand()
        {
            return new TagUpdateCommand
            {
                Id = Id,
                CategoryId = CategoryId,
                Name = Name
            };
        }
    }
}