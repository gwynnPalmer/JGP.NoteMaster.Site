namespace JGP.NoteMaster.Core.Commands
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     A class representing a command for creation of <see cref="Tag" /> objects.
    /// </summary>
    public class TagCreateCommand
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public Guid? Id { get; set; }

        /// <summary>
        ///     Gets or sets the category id.
        /// </summary>
        /// <value>The category id.</value>
        public Guid CategoryId { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public List<NoteCreateCommand> Notes { get; set; }
    }
}