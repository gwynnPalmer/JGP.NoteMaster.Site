namespace JGP.NoteMaster.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using Commands;

    /// <summary>
    ///     A class representing a tag object.
    /// </summary>
    public class Tag
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Tag" /> class.
        /// </summary>
        protected Tag()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Tag" /> class.
        /// </summary>
        /// <param name="command">The Tag Create Command.</param>
        public Tag(TagCreateCommand command)
        {
            _ = command ?? throw new ArgumentNullException(nameof(command));
            Id = command.Id ?? Guid.NewGuid();
            CategoryId = command.CategoryId;
            Name = command.Name;

            if (command.Notes != null)
            {
                Notes = new List<Note>();
                foreach (var cmd in command.Notes)
                {
                    cmd.TagId = Id;
                    Notes.Add(new Note(cmd));
                }
            }
        }

        /// <summary>
        ///     Gets the id.
        /// </summary>
        /// <value>The id.</value>
        public Guid Id { get; protected set; }

        /// <summary>
        ///     Gets the category id.
        /// </summary>
        /// <value>The category id.</value>
        public Guid CategoryId { get; protected set; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }

        /// <summary>
        ///     Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public Category Category { get; set; }


        /// <summary>
        ///     Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public List<Note> Notes { get; set; }

        #region DOMAIN METHODS

        /// <summary>
        ///     Updates the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">command</exception>
        public void Update(TagUpdateCommand command)
        {
            _ = command ?? throw new ArgumentNullException(nameof(command));
            Id = command.Id;
            CategoryId = command.CategoryId;
            Name = command.Name;

            if (command.Notes != null)
            {
                Notes.AddRange(command.Notes.Select(x => new Note(x)).ToList());
            }
        }

        #endregion

        #region OVERRIDES & ESSENTIALS

        /// <summary>
        ///     Determines equality against the specified Tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns><c>true</c> if equal, <c>false</c> otherwise.</returns>
        public bool Equals(Tag tag)
        {
            if (tag is null) return false;

            return Id == tag.Id
                   && CategoryId == tag.CategoryId
                   && Name == tag.Name
                ;
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return obj is Tag tag
                   && Id == tag.Id
                   && CategoryId == tag.CategoryId
                   && Name == tag.Name
                ;
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true
            };
            return JsonSerializer.Serialize(this, options);
        }

        #endregion
    }
}