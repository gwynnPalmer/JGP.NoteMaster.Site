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
            Id = command.Id;
            TagId = command.TagId;
            Body = command.Body;
        }

        /// <summary>
        ///     Gets the id.
        /// </summary>
        /// <value>The id.</value>
        public Guid Id { get; protected set; }

        /// <summary>
        ///     Gets the tag id.
        /// </summary>
        /// <value>The tag id.</value>
        public Guid TagId { get; protected set; }

        /// <summary>
        ///     Gets the body.
        /// </summary>
        /// <value>The body.</value>
        public string Body { get; protected set; }

        /// <summary>
        ///     Gets or sets the tag.
        /// </summary>
        /// <value>The tag.</value>
        public Tag Tag { get; set; }


        #region DOMAIN METHODS

        public void Update(NoteUpdateCommand command)
        {
            _ = command ?? throw new ArgumentNullException(nameof(command));
            Id = command.Id;
            TagId = command.TagId;
            Body = command.Body;
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
                   && TagId == note.TagId
                   && Body == note.Body
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
                   && TagId == note.TagId
                   && Body == note.Body
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