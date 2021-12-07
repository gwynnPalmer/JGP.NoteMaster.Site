namespace JGP.NoteMaster.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using Core;
    using Core.Commands;

    /// <summary>
    ///     A class representing a view model object for a category object.
    /// </summary>
    public class CategoryModel
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CategoryModel" /> class.
        /// </summary>
        public CategoryModel()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CategoryModel" /> class.
        /// </summary>
        /// <param name="category">The Category.</param>
        public CategoryModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        [Required]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }


        /// <summary>
        ///     Gets the Category Create Command
        /// </summary>
        /// <returns>A Category Create Command populated with the current model values.</returns>
        public CategoryCreateCommand GetCreateCommand()
        {
            return new CategoryCreateCommand
            {
                Id = Id,
                Name = Name
            };
        }


        /// <summary>
        ///     Gets the Category Update Command
        /// </summary>
        /// <returns>A Category Update Command populated with the current model values.</returns>
        public CategoryUpdateCommand GetUpdateCommand()
        {
            return new CategoryUpdateCommand
            {
                Id = Id,
                Name = Name
            };
        }
    }
}