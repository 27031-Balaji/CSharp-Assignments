using ExpenseTracker.ConstantLiteral;
using ExpenseTracker.CsvUtils;
using ExpenseTracker.Enums;
using ExpenseTracker.Model;

namespace ExpenseTracker.Repository
{
    /// <summary>
    /// Provides a CSV based repository for storing and retrieving <see cref="FinancialRecord"/> instances.
    /// </summary>
    internal class CsvFinanceRepository : IFinanceRepository
    {
        private const string FilePath = Constant.FinanceFilePath;
        private const string CsvHeader = Constant.FinanceCsvHeader;
        private readonly List<FinancialRecord> records;
        private readonly CsvHandler csvHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvFinanceRepository"/> class.
        /// </summary>
        public CsvFinanceRepository()
        {
            this.records = new List<FinancialRecord>();
            this.csvHandler = new CsvHandler(FilePath);
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
            List<string> lines = new List<string>();
            if (!this.csvHandler.Exists())
            {
                lines.Add(CsvHeader);
            }

            lines.Add(this.ConvertToCsv(record));
            this.csvHandler.Append(lines);
            this.records.Add(record);
        }

        /// <summary>
        /// Retrieves all stored records with the specific filter.
        /// </summary>
        /// <param name="filter">The filter to search for.</param>
        /// <returns>A list of <see cref="FinancialRecord"/> representing the filtered records.</returns>
        public IEnumerable<FinancialRecord> GetRecords(Func<FinancialRecord, bool>? filter = null)
        {
            return (filter == null)
                ? this.records
                : this.records.Where(filter);
        }

        /// <summary>
        /// Retrieves a <see cref="FinancialRecord"/> by its identifier.
        /// </summary>
        /// <param name="recordId">The record identifier to search for.</param>
        /// <returns>The matching <see cref="FinancialRecord"/> if found.</returns>
        public FinancialRecord? GetById(string recordId)
        {
            FinancialRecord? record = this.FindOriginalRecord(recordId);
            return record is null ? null : record;
        }

        /// <summary>
        /// Deletes the specified <see cref="FinancialRecord"/> from the repository and saves changes.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to delete.</param>
        public void DeleteRecord(FinancialRecord record)
        {
            this.records.Remove(record);
            this.SaveRecordsToFile();
        }

        public void DeleteRecordsByUserId(Guid userId) {

        }

        /// <summary>
        /// Updates the data of the specified <see cref="FinancialRecord"/> and saves the changes.
        /// </summary>
        /// <param name="record">The record to be updated.</param>
        public void UpdateRecord(FinancialRecord record)
        {
            FinancialRecord originalRecord = this.FindOriginalRecord(record.Id);
            originalRecord.Date = record.Date;
            originalRecord.Amount = record.Amount;
            originalRecord.Description = record.Description;

            if (originalRecord is Income originalIncome && record is Income updatedIncome)
            {
                originalIncome.Source = updatedIncome.Source;
            }

            if (originalRecord is Expense originalExpense && record is Expense updatedExpense)
            {
                originalExpense.Category = updatedExpense.Category;
            }

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
            List<string> lines = this.csvHandler.Read();
            foreach (string line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                this.records.Add(this.ParseToRecord(line));
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

            this.csvHandler.Write(lines);
        }

        /// <summary>
        /// Parses a CSV line into a <see cref="FinancialRecord"/>.
        /// </summary>
        /// <param name="line">The CSV line to parse. The parsing doesn't raise any exceptions because the inputs are already validated.</param>
        /// <returns>A <see cref="FinancialRecord"/> instance representing the parsed line.</returns>
        private FinancialRecord ParseToRecord(string line)
        {
            List<string> values = this.csvHandler.ParseCsvLine(line);
            string id = values[0];
            Guid userId = Guid.Parse(values[1]);
            DateOnly date = DateOnly.Parse(values[2]);
            string type = values[3];
            string classification = values[4];
            decimal amount = decimal.Parse(values[5]);
            string? description = string.IsNullOrWhiteSpace(values[6]) ? string.Empty : values[5];

            FinancialRecord record;

            if (type == RecordType.Income.ToString())
            {
                record = new Income(id, userId, date, amount, description, RecordType.Income, Enum.Parse<IncomeSource>(classification));
            }
            else
            {
                record = new Expense(id, userId, date, amount, description, RecordType.Expense, Enum.Parse<ExpenseCategory>(classification));
            }

            return record;
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
                this.csvHandler.CsvEscape(record.Id),
                this.csvHandler.CsvEscape(record.Date.ToString("dd/MM/yyyy")),
                this.csvHandler.CsvEscape(record.Type.ToString()),
                this.csvHandler.CsvEscape(record.Classification),
                record.Amount.ToString(),
                this.csvHandler.CsvEscape(record.Description ?? string.Empty));
        }
    }
}