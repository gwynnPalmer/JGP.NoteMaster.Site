namespace JGP.NoteMaster.Core.Commands
{
    using System;

    /// <summary>
    ///     A class representing a command for creation of <see cref="Note" /> objects.
    /// </summary>
    public class NoteCreateCommand
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public Guid? Id { get; set; }

        /// <summary>
        ///     Gets or sets the tag id.
        /// </summary>
        /// <value>The tag id.</value>
        public Guid TagId { get; set; }

        /// <summary>
        ///     Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public string Body { get; set; }
    }
}