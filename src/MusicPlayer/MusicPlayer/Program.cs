namespace MusicPlayer
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    internal class Program
    {
        private static readonly Dictionary<string, MusicalNote> Notes = new Dictionary<string, MusicalNote>
        {
            { "C", new MusicalNote("C", 262) },
            { "C#", new MusicalNote("C#", 277) },
            { "D", new MusicalNote("D", 294) },
            { "D#", new MusicalNote("D#", 311) },
            { "E", new MusicalNote("E", 330) },
            { "F", new MusicalNote("F", 349) },
            { "F#", new MusicalNote("F#", 370) },
            { "G", new MusicalNote("G", 392) },
            { "G#", new MusicalNote("G#", 415) },
            { "A", new MusicalNote("A", 440) },
            { "A#", new MusicalNote("A#", 466) },
            { "B", new MusicalNote("B", 494) },
        };

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
                Console.Beep(note.Frequency, 500);
            }
        }
    }
}