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
            TagId = note.TagId;
            Body = note.Body;
            Tag = note.Tag.Name;
            Category = note.Tag.Category.Name;
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
        [JsonPropertyName("tagId")]
        public Guid TagId { get; set; }

        /// <summary>
        ///     Gets or sets the body.
        /// </summary>
        [JsonPropertyName("body")]
        public string Body { get; set; }

        /// <summary>
        ///     Gets or sets the tag.
        /// </summary>
        /// <value>The tag.</value>
        [JsonPropertyName("tag")]
        public string Tag { get; set; }

        /// <summary>
        ///     Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        [JsonPropertyName("category")]
        public string Category { get; set; }


        /// <summary>
        ///     Gets the Note Create Command
        /// </summary>
        /// <returns>A Note Create Command populated with the current model values.</returns>
        public NoteCreateCommand GetCreateCommand()
        {
            var command = new NoteCreateCommand
            {
                Id = Id,
                TagId = TagId,
                Body = Body
            };

            if (Id != default)
            {
                command.Id = Id;
            }

            return command;
        }


        /// <summary>
        ///     Gets the Note Update Command
        /// </summary>
        /// <returns>A Note Update Command populated with the current model values.</returns>
        public NoteUpdateCommand GetUpdateCommand()
        {
            return new NoteUpdateCommand
            {
                Id = Id,
                TagId = TagId,
                Body = Body
            };
        }
    }
}