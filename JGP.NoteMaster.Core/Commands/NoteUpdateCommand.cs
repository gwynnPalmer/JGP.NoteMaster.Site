namespace JGP.NoteMaster.Core.Commands
{
    using System;

    /// <summary>
    /// A class representing a command for updates to <see cref="Note" /> objects.
    /// </summary>
    public class NoteUpdateCommand
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the tag id.
        /// </summary>
        /// <value>The tag id.</value>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public string BodyText { get; set; }

        public CategoryCreateCommand Category { get; set; }
    }
}