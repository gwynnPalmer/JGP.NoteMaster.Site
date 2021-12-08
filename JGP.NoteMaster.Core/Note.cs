namespace JGP.NoteMaster.Core
{
    using System;
    using System.Text.Json;
    using Commands;

    /// <summary>
    ///     A class representing a note object.
    /// </summary>
    public class Note
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Note" /> class.
        /// </summary>
        protected Note()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Note" /> class.
        /// </summary>
        /// <param name="command">The Note Create Command.</param>
        public Note(NoteCreateCommand command)
        {
            _ = command ?? throw new ArgumentNullException(nameof(command));
            CategoryId = command.CategoryId;

            if (command.Id != null)
            {
                Id = (Guid)command.Id;
            }
            else
            {
                Id = Guid.NewGuid();
            }

            if (command.Category != null)
            {
                Category = new Category(command.Category);
            }

            BodyText = command.BodyText;
        }

        /// <summary>
        ///     Gets the id.
        /// </summary>
        /// <value>The id.</value>
        public Guid Id { get; protected set; }

        public Guid CategoryId { get; protected set; }

        /// <summary>
        ///     Gets the body.
        /// </summary>
        /// <value>The body.</value>
        public string BodyText { get; protected set; }

        public Category Category { get; set; }


        #region DOMAIN METHODS

        /// <summary>
        ///     Updates the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentNullException">command</exception>
        public void Update(NoteUpdateCommand command)
        {
            _ = command ?? throw new ArgumentNullException(nameof(command));
            Id = command.Id;
            CategoryId = command.CategoryId;
            BodyText = command.BodyText;
        }

        #endregion

        #region OVERRIDES & ESSENTIALS

        /// <summary>
        ///     Determines equality against the specified Note.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <returns><c>true</c> if equal, <c>false</c> otherwise.</returns>
        public bool Equals(Note note)
        {
            if (note is null) return false;

            return Id == note.Id
                   && CategoryId == note.CategoryId
                   && BodyText == note.BodyText
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

            return obj is Note note
                   && Id == note.Id
                   && CategoryId == note.CategoryId
                   && BodyText == note.BodyText
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