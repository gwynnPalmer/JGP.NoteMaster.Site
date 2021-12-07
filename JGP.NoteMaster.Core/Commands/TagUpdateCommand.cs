namespace JGP.NoteMaster.Core.Commands
{
    using System;

    /// <summary>
    ///     A class representing a command for updates to <see cref="Tag" /> objects.
    /// </summary>
    public class TagUpdateCommand
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public Guid Id { get; set; }

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
    }
}