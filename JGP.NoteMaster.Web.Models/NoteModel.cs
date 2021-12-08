namespace JGP.NoteMaster.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using Core;
    using Core.Commands;

    /// <summary>
    ///     A class representing a view model object for a note object.
    /// </summary>
    public class NoteModel
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NoteModel" /> class.
        /// </summary>
        public NoteModel()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NoteModel" /> class.
        /// </summary>
        /// <param name="note">The Note.</param>
        public NoteModel(Note note)
        {
            Id = note.Id;
            BodyText = note.BodyText;
            CategoryId = note.CategoryId;

            if (note.Category != null)
            {
                Category = new CategoryModel(note.Category);
                CategoryText = note.Category.Name;
            }
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        [Required]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the tag id.
        /// </summary>
        [Required]
        [JsonPropertyName("categoryId")]
        public Guid CategoryId { get; set; }

        /// <summary>
        ///     Gets or sets the body.
        /// </summary>
        [JsonPropertyName("body")]
        public string BodyText { get; set; }

        /// <summary>
        ///     Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        [JsonPropertyName("category")]
        public string CategoryText { get; set; }

        public CategoryModel Category { get; set; }

        /// <summary>
        ///     Gets the Note Create Command
        /// </summary>
        /// <returns>A Note Create Command populated with the current model values.</returns>
        public NoteCreateCommand GetCreateCommand()
        {
            var command = new NoteCreateCommand
            {
                Id = Id,
                CategoryId = CategoryId,
                BodyText = BodyText,
            };

            if (Id != default)
            {
                command.Id = Id;
            }

            if (Category != null)
            {
                command.Category = Category.GetCreateCommand();
            }

            return command;
        }


        /// <summary>
        ///     Gets the Note Update Command
        /// </summary>
        /// <returns>A Note Update Command populated with the current model values.</returns>
        public NoteUpdateCommand GetUpdateCommand()
        {
            var command = new NoteUpdateCommand
            {
                Id = Id,
                CategoryId = CategoryId,
                BodyText = BodyText,
            };

            if (Category != null)
            {
                command.Category = Category.GetCreateCommand();
            }

            return command;
        }
    }
}