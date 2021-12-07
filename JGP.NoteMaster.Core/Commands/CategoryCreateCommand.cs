namespace JGP.NoteMaster.Core.Commands
{
    using System;

    /// <summary>
    ///     A class representing a command for creation of <see cref="Category" /> objects.
    /// </summary>
    public class CategoryCreateCommand
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
    }
}