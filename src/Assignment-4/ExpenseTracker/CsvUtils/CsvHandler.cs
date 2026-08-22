using System.Text;

namespace ExpenseTracker.CsvUtils
{
    internal class CsvHandler
    {
        private readonly string filePath;

        public CsvHandler(string filePath)
        {
            this.filePath = filePath;
        }

        public List<string> Read()
        {
            return !this.Exists() ? new List<string>() : File.ReadAllLines(this.filePath).ToList();
        }

        public void Write(List<string> lines)
        {
            File.WriteAllLines(this.filePath, lines);
        }

        public void Append(List<string> lines)
        {
            File.AppendAllLines(this.filePath, lines);
        }

        public bool Exists()
        {
            return File.Exists(this.filePath);
        }

        public string CsvEscape(string value)
        {
            if (value.Contains(',') || value.Contains('"'))
            {
                value = value.Replace("\"", "\"\"");
                return $"\"{value}\"";
            }

            return value;
        }

        public List<string> ParseCsvLine(string line)
        {
            List<string> fields = new List<string>();
            StringBuilder field = new StringBuilder();
            bool insideQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char ch = line[i];
                if (ch == '"')
                {
                    if (insideQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        field.Append('"');
                        i++;
                    }
                    else
                    {
                        insideQuotes = !insideQuotes;
                    }
                }
                else if (ch == ',' && !insideQuotes)
                {
                    fields.Add(field.ToString());
                    field.Clear();
                }
                else
                {
                    field.Append(ch);
                }
            }

            fields.Add(field.ToString());

            return fields;
        }
    }
}