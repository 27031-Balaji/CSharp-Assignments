using System.Text;

namespace ExpenseTracker.CsvUtils
{
    /// <summary>
    /// Simple CSV read/write helper that performs file operations for CSV data.
    /// </summary>
    internal class CsvHandler
    {
        private readonly string filePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvHandler"/> class.
        /// </summary>
        /// <param name="filePath">The path of the CSV file to read and write.</param>
        public CsvHandler(string filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// Reads all lines from the configured CSV file.
        /// </summary>
        /// <returns>
        /// A list containing the file lines.
        /// </returns>
        public List<string> Read()
        {
            return !this.Exists() ? new List<string>() : File.ReadAllLines(this.filePath).ToList();
        }

        /// <summary>
        /// Writes in the CSV file with the provided lines.
        /// </summary>
        /// <param name="lines">The lines to write to the file.</param>
        public void Write(List<string> lines)
        {
            File.WriteAllLines(this.filePath, lines);
        }

        /// <summary>
        /// Appends the provided lines to the CSV file, creating the file if it does not exist.
        /// </summary>
        /// <param name="lines">The lines to append to the file.</param>
        public void Append(List<string> lines)
        {
            File.AppendAllLines(this.filePath, lines);
        }

        /// <summary>
        /// Determines whether the CSV file exists.
        /// </summary>
        /// <returns>True when the file exists, otherwise false.</returns>
        public bool Exists()
        {
            return File.Exists(this.filePath);
        }

        /// <summary>
        /// Escapes a single CSV field value by quoting it when it contains commas or quotes.
        /// </summary>
        /// <param name="value">The field value to escape.</param>
        /// <returns>The CSV-escaped field string.</returns>
        public string CsvEscape(string value)
        {
            if (value.Contains(',') || value.Contains('"'))
            {
                value = value.Replace("\"", "\"\"");
                return $"\"{value}\"";
            }

            return value;
        }

        /// <summary>
        /// Parses a single CSV line into individual field values.
        /// </summary>
        /// <param name="line">The CSV line to parse.</param>
        /// <returns>A <see cref="List{string}"/> of parsed field values.</returns>
        public List<string> ParseCsvLine(string line)
        {
            List<string> fields = new List<string>();
            StringBuilder field = new StringBuilder();
            bool insideQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char ch = line[i];
                /**
                     * If currently inside quotes and double quotes are found, add to part and skip the next character.
                     * If currently inside quotes, but not a double quote, then it is the closing quote.
                     * If not inside quotes, then it must be an opening quote.
                */
                if (ch == '"')
                {
                    if (insideQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        field.Append('"');
                        i++; // Skip double quote
                    }
                    else
                    {
                        insideQuotes = !insideQuotes;
                    }
                }

                /**
                     * If currently inside quotes, then add comma to part.
                     * Otherwise, treat it as a delimiter.
                */
                else if (ch == ',' && !insideQuotes)
                {
                    // Add part to list of parts, and clear the string builder
                    fields.Add(field.ToString());
                    field.Clear();
                }
                else
                {
                    field.Append(ch);
                }
            }

            fields.Add(field.ToString());

            if (insideQuotes)
            {
                throw new FormatException();
            }

            return fields;
        }
    }
}