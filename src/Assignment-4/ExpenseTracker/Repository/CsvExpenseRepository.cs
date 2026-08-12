using System.Text;
using ExpenseTracker.Constant;
using ExpenseTracker.Enums;
using ExpenseTracker.Model;

namespace ExpenseTracker.Repository
{
    /// <summary>
    /// Provides a CSV based repository for storing and retrieving <see cref="FinancialRecord"/> instances.
    /// </summary>
    internal class CsvExpenseRepository : IExpenseRepository
    {
        private const string FilePath = CsvConstant.FilePath;
        private const string CsvHeader = CsvConstant.CsvHeader;
        private readonly List<FinancialRecord> records;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvExpenseRepository"/> class.
        /// </summary>
        public CsvExpenseRepository()
        {
            this.records = new List<FinancialRecord>();
            this.LoadRecords();
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
        /// Retrieves all stored <see cref="FinancialRecord"/> instances as a cloned list.
        /// </summary>
        /// <returns>A list of <see cref="FinancialRecord"/> containing cloned records.</returns>
        public IEnumerable<FinancialRecord> GetAllRecords()
        {
            IEnumerable<FinancialRecord> duplicateRecords = this.records
                                                        .Select(this.CloneRecord);

            return duplicateRecords;
        }

        /// <summary>
        /// Retrieves all stored income records.
        /// </summary>
        /// <returns>A list of <see cref="FinancialRecord"/> representing income records.</returns>
        public IEnumerable<FinancialRecord> GetAllIncomeRecords()
        {
            IEnumerable<FinancialRecord> duplicateIncomeRecords = this.records
                                                    .Where(record => record is Income)
                                                    .Select(this.CloneRecord);

            return duplicateIncomeRecords;
        }

        /// <summary>
        /// Retrieves all stored expense records.
        /// </summary>
        /// <returns>A list of <see cref="FinancialRecord"/> representing expense records.</returns>
        public IEnumerable<FinancialRecord> GetAllExpenseRecords()
        {
            IEnumerable<FinancialRecord> duplicateExpenseRecords = this.records
                                                            .Where(record => record is Expense)
                                                            .Select(this.CloneRecord);

            return duplicateExpenseRecords;
        }

        /// <summary>
        /// Retrieves records that match the specified date.
        /// </summary>
        /// <param name="date">The date value to filter records by.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> that occur on the specified date.</returns>
        public IEnumerable<FinancialRecord> GetByDate(DateOnly date)
        {
            IEnumerable<FinancialRecord> dateRecords = this.records
                                                .Where(record => record.Date == date)
                                                .Select(this.CloneRecord);

            return dateRecords;
        }

        /// <summary>
        /// Retrieves records that match the specified month and year.
        /// </summary>
        /// <param name="month">The month to filter by.</param>
        /// <param name="year">The year to filter by.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> that occur within the specified month and year.</returns>
        public IEnumerable<FinancialRecord> GetByMonthAndYear(int month, int year)
        {
            IEnumerable<FinancialRecord> monthAndYearRecords = this.records
                                                .Where(record => record.Date.Month == month && record.Date.Year == year)
                                                .Select(this.CloneRecord);

            return monthAndYearRecords;
        }

        /// <summary>
        /// Retrieves records that match the specified amount.
        /// </summary>
        /// <param name="amount">The amount to filter records by.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> with the specified amount.</returns>
        public IEnumerable<FinancialRecord> GetByAmount(decimal amount)
        {
            IEnumerable<FinancialRecord> amountRecords = this.records
                                                    .Where(record => record.Amount == amount)
                                                    .Select(this.CloneRecord);

            return amountRecords;
        }

        /// <summary>
        /// Retrieves income records that match the specified <see cref="IncomeSource"/>.
        /// </summary>
        /// <param name="source">The <see cref="IncomeSource"/> to filter by.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> representing incomes with the specified source.</returns>
        public IEnumerable<FinancialRecord> GetBySource(IncomeSource source)
        {
            IEnumerable<FinancialRecord> sourceRecords = this.records
                                                    .Where(record => record is Income income && income.Source == source)
                                                    .Select(this.CloneRecord);

            return sourceRecords;
        }

        /// <summary>
        /// Retrieves expense records that match the specified <see cref="ExpenseCategory"/>.
        /// </summary>
        /// <param name="category">The <see cref="ExpenseCategory"/> to filter by.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> representing expenses in the specified category.</returns>
        public IEnumerable<FinancialRecord> GetByCategory(ExpenseCategory category)
        {
            IEnumerable<FinancialRecord> categoryRecords = this.records
                                                    .Where(record => record is Expense expense && expense.Category == category)
                                                    .Select(this.CloneRecord);

            return categoryRecords;
        }

        /// <summary>
        /// Retrieves a <see cref="FinancialRecord"/> by its identifier.
        /// </summary>
        /// <param name="recordId">The record identifier to search for.</param>
        /// <returns>The matching <see cref="FinancialRecord"/> if found.</returns>
        public FinancialRecord? GetById(string recordId)
        {
            FinancialRecord? record = this.records.Find(record => record.Id.Equals(recordId, StringComparison.OrdinalIgnoreCase));
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
            this.SaveRecords();
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
            this.SaveRecords();
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
            this.SaveRecords();
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
            this.SaveRecords();
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
            this.SaveRecords();
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
            this.SaveRecords();
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
        private void LoadRecords()
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
        private void SaveRecords()
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
        /// <param name="line">The CSV line to parse.</param>
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

            if (type.Equals("Income", StringComparison.OrdinalIgnoreCase))
            {
                return new Income(id, date, amount, description, Enum.Parse<IncomeSource>(classification));
            }
            else
            {
                return new Expense(id, date, amount, description, Enum.Parse<ExpenseCategory>(classification));
            }
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
                this.CsvEscape(record.Type),
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
                Income income => new Income(income.Id, income.Date, income.Amount, income.Description, income.Source),
                Expense expense => new Expense(expense.Id, expense.Date, expense.Amount, expense.Description, expense.Category),
                _ => throw new InvalidOperationException("Unknown record type.")
            };
        }
    }
}