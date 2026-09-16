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
        private readonly List<FinancialRecord> records;
        private CsvHandler csvHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvFinanceRepository"/> class.
        /// </summary>
        public CsvFinanceRepository()
        {
            this.records = new List<FinancialRecord>();
            Directory.CreateDirectory(Constant.TransactionFolderPath);
            this.csvHandler = new CsvHandler(string.Empty);
        }

        /// <summary>
        /// Loads the records from the file to the in-memory list for the specific user.
        /// </summary>
        /// <param name="userId">The ID of the user, for which the file contents should load.</param>
        public void LoadRecords(Guid userId)
        {
            this.records.Clear();
            string filePath = Path.Combine(Constant.TransactionFolderPath, $"{userId}.csv");
            this.csvHandler = new CsvHandler(filePath);
            this.ReadRecordsFromFile();
        }

        /// <summary>
        /// Adds a <see cref="FinancialRecord"/> to the repository and saves changes to the CSV file.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to add.</param>
        public void AddRecord(FinancialRecord record)
        {
            List<string> lines = new List<string>();
            if (!this.csvHandler.Exists())
            {
                lines.Add(Constant.FinanceCsvHeader);
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
                ? this.records.Select(this.CloneRecord)
                : this.records
                    .Where(filter)
                    .Select(this.CloneRecord);
        }

        /// <summary>
        /// Retrieves a <see cref="FinancialRecord"/> by its identifier.
        /// </summary>
        /// <param name="recordId">The record identifier to search for.</param>
        /// <returns>The matching <see cref="FinancialRecord"/> if found, else null.</returns>
        public FinancialRecord? GetById(string recordId)
        {
            FinancialRecord? record = this.FindOriginalRecord(recordId);
            return record is null ? null : this.CloneRecord(record);
        }

        /// <summary>
        /// Deletes the specified <see cref="FinancialRecord"/> from the repository and saves changes.
        /// </summary>
        /// <param name="record">The <see cref="FinancialRecord"/> to delete.</param>
        /// <returns>True if the delete operation is successful, else false.</returns>
        public bool DeleteRecord(FinancialRecord record)
        {
            FinancialRecord? originalRecord = this.FindOriginalRecord(record.Id);
            if (originalRecord == null)
            {
                return false;
            }

            this.records.Remove(originalRecord);
            this.SaveRecordsToFile();
            return true;
        }

        /// <summary>
        /// Updates the data of the specified <see cref="FinancialRecord"/> and saves the changes.
        /// </summary>
        /// <param name="record">The record to be updated.</param>
        /// <returns>True if the edit operation is successful, else false.</returns>
        public bool UpdateRecord(FinancialRecord record)
        {
            FinancialRecord? originalRecord = this.FindOriginalRecord(record.Id);
            if (originalRecord == null)
            {
                return false;
            }

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

            return true;
        }

        /// <summary>
        /// Deletes all records associated with the specified user identifier and saves changes.
        /// </summary>
        public void DeleteRecordsByUserId()
        {
            this.records.Clear();
            this.csvHandler.Delete();
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
        /// <returns>The original stored record, else null.</returns>
        private FinancialRecord? FindOriginalRecord(string recordId)
        {
            return this.records.FirstOrDefault(record => record.Id.Equals(recordId, StringComparison.OrdinalIgnoreCase));
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
            List<string> lines = new List<string> { Constant.FinanceCsvHeader };
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
            DateOnly date = DateOnly.Parse(values[1]);
            RecordType recordType = Enum.Parse<RecordType>(values[2]);
            string classification = values[3];
            decimal amount = decimal.Parse(values[4]);
            string? description = string.IsNullOrWhiteSpace(values[5]) ? string.Empty : values[5];

            FinancialRecord record;

            switch (recordType)
            {
                case RecordType.Income:
                    record = new Income(id, date, amount, description, Enum.Parse<IncomeSource>(classification));
                    break;

                case RecordType.Expense:
                    record = new Expense(id, date, amount, description, Enum.Parse<ExpenseCategory>(classification));
                    break;

                default:
                    throw new FormatException();
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
                _ => throw new InvalidOperationException()
            };
        }
    }
}