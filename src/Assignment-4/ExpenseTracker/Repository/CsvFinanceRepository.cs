using System.Text;
using ExpenseTracker.ConstantLiteral;
using ExpenseTracker.Enums;
using ExpenseTracker.Model;

namespace ExpenseTracker.Repository
{
    /// <summary>
    /// Provides a CSV based repository for storing and retrieving <see cref="FinancialRecord"/> instances.
    /// </summary>
    internal class CsvFinanceRepository : IRepository
    {
        private const string FilePath = Constant.FilePath;
        private const string CsvHeader = Constant.CsvHeader;
        private readonly List<FinancialRecord> records;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvFinanceRepository"/> class.
        /// </summary>
        public CsvFinanceRepository()
        {
            this.records = new List<FinancialRecord>();
            this.ReadRecordsFromFile();
        }

        /// <summary>
        /// Gets the total number of stored <see cref="FinancialRecord"/> instances.
        /// </summary>
        /// <value>The number of records currently stored.</value>
        public int RecordCount { get => this.records.Count; }

        /// <summary>
        /// Adds a <see cref="FinancialRecord"/> to the repository and saves changes to the CSV file.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to add.</param>
        public void AddRecord(FinancialRecord record)
        {
            bool fileExists = File.Exists(FilePath);
            List<string> lines = new List<string>();
            if (!fileExists)
            {
                lines.Add(CsvHeader);
            }

            lines.Add(this.ConvertToCsv(record));
            File.AppendAllLines(FilePath, lines);
            this.records.Add(record);
        }

        /// <summary>
        /// Retrieves all stored records with the specific filter.
        /// </summary>
        /// <param name="filter">The filter to search for.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> representing the filtered records.</returns>
        public IEnumerable<FinancialRecord> GetRecords(Func<FinancialRecord, bool>? filter = null)
        {
            return ((filter == null)
                ? this.records
                : this.records.Where(filter))
                .Select(this.CloneRecord);
        }

        /// <summary>
        /// Retrieves a <see cref="FinancialRecord"/> by its identifier.
        /// </summary>
        /// <param name="recordId">The record identifier to search for.</param>
        /// <returns>The matching <see cref="FinancialRecord"/> if found.</returns>
        public FinancialRecord? GetById(string recordId)
        {
            FinancialRecord? record = this.FindOriginalRecord(recordId);
            return record is null ? null : this.CloneRecord(record);
        }

        /// <summary>
        /// Deletes the specified <see cref="FinancialRecord"/> from the repository and saves changes.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to delete.</param>
        public void DeleteRecord(FinancialRecord record)
        {
            FinancialRecord originalRecord = this.FindOriginalRecord(record.Id);
            this.records.Remove(originalRecord);
            this.SaveRecordsToFile();
        }

        /// <summary>
        /// Updates the date of the specified <see cref="FinancialRecord"/> and saves changes.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <param name="date">The new date value.</param>
        public void UpdateRecordDate(FinancialRecord record, DateOnly date)
        {
            FinancialRecord originalRecord = this.FindOriginalRecord(record.Id);
            originalRecord.Date = date;
            this.SaveRecordsToFile();
        }

        /// <summary>
        /// Updates the amount of the specified <see cref="FinancialRecord"/> and saves changes.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <param name="amount">The new amount value.</param>
        public void UpdateRecordAmount(FinancialRecord record, decimal amount)
        {
            FinancialRecord originalRecord = this.FindOriginalRecord(record.Id);
            originalRecord.Amount = amount;
            this.SaveRecordsToFile();
        }

        /// <summary>
        /// Updates the source of the specified <see cref="Income"/> and saves changes.
        /// </summary>
        /// <param name="record">The <see cref="Income"/> record to update.</param>
        /// <param name="source">The new <see cref="IncomeSource"/>.</param>
        public void UpdateRecordSource(Income record, IncomeSource source)
        {
            Income originalRecord = (Income)this.FindOriginalRecord(record.Id);
            originalRecord.Source = source;
            this.SaveRecordsToFile();
        }

        /// <summary>
        /// Updates the category of the specified <see cref="Expense"/> and saves changes.
        /// </summary>
        /// <param name="record">The <see cref="Expense"/> record to update.</param>
        /// <param name="category">The new <see cref="ExpenseCategory"/>.</param>
        public void UpdateRecordCategory(Expense record, ExpenseCategory category)
        {
            Expense originalRecord = (Expense)this.FindOriginalRecord(record.Id);
            originalRecord.Category = category;
            this.SaveRecordsToFile();
        }

        /// <summary>
        /// Updates the description of the specified <see cref="FinancialRecord"/> and saves changes.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to update.</param>
        /// <param name="description">The new description value.</param>
        public void UpdateRecordDescription(FinancialRecord record, string? description)
        {
            FinancialRecord originalRecord = this.FindOriginalRecord(record.Id);
            originalRecord.Description = description;
            this.SaveRecordsToFile();
        }

        /// <summary>
        /// Determines whether a record identifier already exists in the repository.
        /// </summary>
        /// <param name="recordId">The record identifier to check.</param>
        /// <returns>True if the record exists, otherwise false.</returns>
        public bool RecordIdExists(string recordId)
        {
            return this.records.Any(record => record.Id == recordId);
        }

        /// <summary>
        /// Retrieves the original record stored in the repository.
        /// </summary>
        /// <param name="recordId">The identifier of the record.</param>
        /// <returns>The original stored record.</returns>
        private FinancialRecord FindOriginalRecord(string recordId)
        {
            return this.records.First(record =>
                record.Id.Equals(recordId, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Loads records from the CSV file into the in-memory list.
        /// </summary>
        private void ReadRecordsFromFile()
        {
            if (!File.Exists(FilePath))
            {
                return;
            }

            string[] lines = File.ReadAllLines(FilePath);
            foreach (string line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                FinancialRecord record = this.ParseToRecord(line);
                this.records.Add(record);
            }
        }

        /// <summary>
        /// Saves the in-memory records to the CSV file.
        /// </summary>
        private void SaveRecordsToFile()
        {
            List<string> lines = new List<string> { CsvHeader };
            foreach (FinancialRecord record in this.records)
            {
                lines.Add(this.ConvertToCsv(record));
            }

            File.WriteAllLines(FilePath, lines);
        }

        /// <summary>
        /// Parses a CSV line into a <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="line">The CSV line to parse. The parsing doesn't raise any exceptions because the inputs are already validated.</param>
        /// <returns>A <see cref="FinancialRecord"/> instance representing the parsed line.</returns>
        private FinancialRecord ParseToRecord(string line)
        {
            List<string> values = this.ParseCsvLine(line);
            string id = values[0];
            DateOnly date = DateOnly.Parse(values[1]);
            string type = values[2];
            string classification = values[3];
            decimal amount = decimal.Parse(values[4]);
            string? description = string.IsNullOrWhiteSpace(values[5]) ? string.Empty : values[5];

            FinancialRecord record;

            if (type == RecordType.Income.ToString())
            {
                record = new Income(id, date, amount, description, RecordType.Income, Enum.Parse<IncomeSource>(classification));
            }
            else
            {
                record = new Expense(id, date, amount, description, RecordType.Expense, Enum.Parse<ExpenseCategory>(classification));
            }

            return record;
        }

        /// <summary>
        /// Parses a CSV line into its constituent fields, handling commas and quotes.
        /// </summary>
        /// <param name="line">The CSV line to split.</param>
        /// <returns>A list of field values extracted from the line.</returns>
        private List<string> ParseCsvLine(string line)
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

        /// <summary>
        /// Converts a <see cref="FinancialRecord"/> into a CSV line.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to convert.</param>
        /// <returns>A CSV-converted string representing the record.</returns>
        private string ConvertToCsv(FinancialRecord record)
        {
            return string.Join(
                ",",
                this.CsvEscape(record.Id),
                this.CsvEscape(record.Date.ToString("dd/MM/yyyy")),
                this.CsvEscape(record.Type.ToString()),
                this.CsvEscape(record.Classification),
                record.Amount.ToString(),
                this.CsvEscape(record.Description ?? string.Empty));
        }

        /// <summary>
        /// Escapes a value for CSV output by quoting and doubling embedded quotes when necessary.
        /// </summary>
        /// <param name="value">The field value to escape.</param>
        /// <returns>The escaped field value suitable for CSV.</returns>
        private string CsvEscape(string value)
        {
            if (value.Contains(',') || value.Contains('"'))
            {
                value = value.Replace("\"", "\"\"");
                return $"\"{value}\"";
            }

            return value;
        }

        /// <summary>
        /// Creates a copy of the specified financial record.
        /// </summary>
        /// <param name="record">The financial record to copy.</param>
        /// <returns>A new instance of the same type as the provided record.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the record type is unknown.</exception>
        private FinancialRecord CloneRecord(FinancialRecord record)
        {
            return record switch
            {
                Income income => new Income(income.Id, income.Date, income.Amount, income.Description, income.Type, income.Source),
                Expense expense => new Expense(expense.Id, expense.Date, expense.Amount, expense.Description, expense.Type, expense.Category),
                _ => throw new InvalidOperationException("Unknown record type.")
            };
        }
    }
}