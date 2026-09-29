namespace MusicPlayer
{
    /// <summary>
    /// Used to represent a single musical note.
    /// </summary>
    internal class MusicalNote
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MusicalNote"/> class.
        /// </summary>
        /// <param name="name">The name of the note.</param>
        /// <param name="frequency">The frequency of the note.</param>
        public MusicalNote(string name, int frequency)
        {
            this.Name = name;
            this.Frequency = frequency;
        }

        /// <summary>
        /// Gets the name of the note.
        /// </summary>
        /// <value>The name of the note.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the frequency of the note.
        /// </summary>
        /// <value>The frequency of the note.</value>
        public int Frequency { get; }
    }
}
