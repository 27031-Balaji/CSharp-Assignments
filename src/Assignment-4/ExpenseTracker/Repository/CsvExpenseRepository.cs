using System.Text;
using ExpenseTracker.Constant;
using ExpenseTracker.Enums;
using ExpenseTracker.Model;

namespace ExpenseTracker.Repository
{
    internal class CsvExpenseRepository : IExpenseRepository
    {
        private const string FilePath = CsvConstant.FilePath;
        private const string CsvHeader = CsvConstant.CsvHeader;
        private readonly List<FinancialRecord> _records;

        public CsvExpenseRepository()
        {
            this._records = new List<FinancialRecord>();
            this.LoadRecords();
        }

        public int RecordCount { get => this._records.Count; }

        public void AddRecord(FinancialRecord record)
        {
            this._records.Add(record);
            this.SaveRecords();
        }

        public IEnumerable<FinancialRecord> GetAllRecords()
        {
            List<FinancialRecord> duplicateRecords = this._records
                                                        .Select(record => record.Clone())
                                                        .ToList();

            return duplicateRecords;
        }

        public IEnumerable<FinancialRecord> GetAllIncomeRecords()
        {
            List<FinancialRecord> duplicateIncomeRecords = this._records
                                                    .Where(record => record is Income)
                                                    .Select(record => record.Clone())
                                                    .ToList();

            return duplicateIncomeRecords;
        }

        public IEnumerable<FinancialRecord> GetAllExpenseRecords()
        {
            List<FinancialRecord> duplicateExpenseRecords = this._records
                                                            .Where(record => record is Expense)
                                                            .Select(record => record.Clone())
                                                            .ToList();

            return duplicateExpenseRecords;
        }

        public IEnumerable<FinancialRecord> GetByDate(DateOnly date)
        {
            List<FinancialRecord> dateRecords = this._records
                                                .Where(record => record.Date == date)
                                                .Select(record => record.Clone())
                                                .ToList();

            return dateRecords;
        }

        public IEnumerable<FinancialRecord> GetByMonthAndYear(int month, int year)
        {
            List<FinancialRecord> monthAndYearRecords = this._records
                                                .Where(record => record.Date.Month == month && record.Date.Year == year)
                                                .Select(record => record.Clone())
                                                .ToList();

            return monthAndYearRecords;
        }

        public IEnumerable<FinancialRecord> GetByAmount(decimal amount)
        {
            List<FinancialRecord> amountRecords = this._records
                                                .Where(record => record.Amount == amount)
                                                .Select(record => record.Clone())
                                                .ToList();

            return amountRecords;
        }

        public IEnumerable<FinancialRecord> GetBySource(IncomeSource source)
        {
            List<FinancialRecord> sourceRecords = this._records
                                                .Where(record => record is Income income && income.Source == source)
                                                .Select(record => record.Clone())
                                                .ToList();

            return sourceRecords;
        }

        public IEnumerable<FinancialRecord> GetByCategory(ExpenseCategory category)
        {
            List<FinancialRecord> categoryRecords = this._records
                                                .Where(record => record is Expense expense && expense.Category == category)
                                                .Select(record => record.Clone())
                                                .ToList();

            return categoryRecords;
        }

        public FinancialRecord? GetById(string recordId)
        {
            return this._records.Find(record => record.Id.Equals(recordId, StringComparison.OrdinalIgnoreCase));
        }

        public void DeleteRecord(FinancialRecord record)
        {
            this._records.Remove(record);
            this.SaveRecords();
        }

        public void UpdateRecordDate(FinancialRecord record, DateOnly date)
        {
            record.Date = date;
            this.SaveRecords();
        }

        public void UpdateRecordAmount(FinancialRecord record, decimal amount)
        {
            record.Amount = amount;
            this.SaveRecords();
        }

        public void UpdateRecordSource(Income record, IncomeSource source)
        {
            record.Source = source;
            this.SaveRecords();
        }

        public void UpdateRecordCategory(Expense record, ExpenseCategory category)
        {
            record.Category = category;
            this.SaveRecords();
        }

        public void UpdateRecordDescription(FinancialRecord record, string? description)
        {
            record.Description = description;
            this.SaveRecords();
        }

        public bool RecordIdExists(string recordId)
        {
            return this._records.Any(record => record.Id == recordId);
        }

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
                this._records.Add(record);
            }
        }

        private void SaveRecords()
        {
            List<string> lines = new List<string> { CsvHeader };
            foreach (FinancialRecord record in this._records)
            {
                lines.Add(this.ConvertToCsv(record));
            }

            File.WriteAllLines(FilePath, lines);
        }

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

        private string CsvEscape(string value)
        {
            if (value.Contains(',') || value.Contains('"'))
            {
                value = value.Replace("\"", "\"\"");
                return $"\"{value}\"";
            }

            return value;
        }
    }
}