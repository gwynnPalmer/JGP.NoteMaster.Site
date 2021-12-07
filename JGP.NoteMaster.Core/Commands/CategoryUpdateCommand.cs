namespace JGP.NoteMaster.Core.Commands
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     A class representing a command for updates to <see cref="Category" /> objects.
    /// </summary>
    public class CategoryUpdateCommand
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the tags.
        /// </summary>
        /// <value>The tags.</value>
        public List<TagCreateCommand> Tags { get; set; }

    }
}