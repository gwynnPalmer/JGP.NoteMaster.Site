namespace JGP.NoteMaster.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using Commands;

    /// <summary>
    ///     A class representing a category object.
    /// </summary>
    public class Category
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Category" /> class.
        /// </summary>
        protected Category()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Category" /> class.
        /// </summary>
        /// <param name="command">The Category Create Command.</param>
        public Category(CategoryCreateCommand command)
        {
            _ = command ?? throw new ArgumentNullException(nameof(command));
            Id = Guid.NewGuid();
            Name = command.Name;

            if (command.Tags != null)
            {
                Tags = new List<Tag>();
                foreach (var cmd in command.Tags)
                {
                    cmd.CategoryId = Id;
                    Tags.Add(new Tag(cmd));
                }
            }
        }

        /// <summary>
        ///     Gets the id.
        /// </summary>
        /// <value>The id.</value>
        public Guid Id { get; protected set; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }


        /// <summary>
        ///     Gets or sets the tags.
        /// </summary>
        /// <value>The tags.</value>
        public List<Tag> Tags { get; set; }

        #region DOMAIN METHODS

        /// <summary>
        ///     Updates the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">command</exception>
        public void Update(CategoryUpdateCommand command)
        {
            _ = command ?? throw new ArgumentNullException(nameof(command));
            Id = command.Id;
            Name = command.Name;

            if (command.Tags != null)
            {
                Tags = command.Tags.Select(x => new Tag(x)).ToList();
            }
        }

        #endregion

        #region OVERRIDES & ESSENTIALS

        /// <summary>
        ///     Determines equality against the specified Category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns><c>true</c> if equal, <c>false</c> otherwise.</returns>
        public bool Equals(Category category)
        {
            if (category is null) return false;

            return Id == category.Id
                   && Name == category.Name
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

            return obj is Category category
                   && Id == category.Id
                   && Name == category.Name
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