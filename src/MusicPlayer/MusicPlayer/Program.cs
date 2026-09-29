namespace MusicPlayer
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    internal class Program
    {
        private static readonly Dictionary<string, MusicalNote> Notes = new Dictionary<string, MusicalNote>
        {
            { "C", new MusicalNote("C", (int)261.63) },
            { "C#", new MusicalNote("C#", (int)271.18) },
            { "D", new MusicalNote("D", (int)293.66) },
            { "D#", new MusicalNote("D#", (int)311.13) },
            { "E", new MusicalNote("E", (int)329.63) },
            { "F", new MusicalNote("F", (int)349.23) },
            { "F#", new MusicalNote("F#", (int)369.99) },
            { "G", new MusicalNote("G", (int)392.00) },
            { "G#", new MusicalNote("G#", (int)415.30) },
            { "A", new MusicalNote("A", (int)440.00) },
            { "A#", new MusicalNote("A#", (int)466.16) },
            { "B", new MusicalNote("B", (int)493.88) },
        };

        private static readonly int MusicNoteDuration = 500;

        /// <summary>
        /// The main method is the method that runs when the application is run.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter a sequence of notes:");
            Console.WriteLine("Example: C D E F G");

            string input = Console.ReadLine() ?? string.Empty;
            List<MusicalNote> inputNotesSequence = ParseNotes(input);

            Console.WriteLine("\nPlaying sequence...");
            PlaySequence(inputNotesSequence);
            Console.WriteLine("\nFinished.");
            Console.ReadKey();
        }

        /// <summary>
        /// Parses the input given by the user to a list of notes.
        /// </summary>
        /// <param name="input">The input string given by the user.</param>
        /// <returns>The musical note list.</returns>
        private static List<MusicalNote> ParseNotes(string input)
        {
            List<MusicalNote> sequence = new List<MusicalNote>();
            string[] tokens = input.Split(' ');

            foreach (string token in tokens)
            {
                if (Notes.TryGetValue(token, out MusicalNote? note))
                {
                    sequence.Add(note);
                }
                else
                {
                    Console.WriteLine($"Invalid note ignored: {token}");
                }
            }

            return sequence;
        }

        /// <summary>
        /// Plays the musical note sequence.
        /// </summary>
        /// <param name="sequence">The sequence to be played.</param>
        private static void PlaySequence(List<MusicalNote> sequence)
        {
            foreach (MusicalNote note in sequence)
            {
                Console.WriteLine($"Playing {note.Name}");
                Console.Beep(note.Frequency, MusicNoteDuration);
            }
        }
    }
}