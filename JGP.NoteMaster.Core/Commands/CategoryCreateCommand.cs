namespace JGP.NoteMaster.Core.Commands
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A class representing a command for creation of <see cref="Category" /> objects.
    /// </summary>
    public class CategoryCreateCommand
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid? Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public List<NoteCreateCommand> Notes { get; set; }
    }
}