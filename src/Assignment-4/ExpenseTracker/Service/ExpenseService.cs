using ExpenseTracker.Enums;
using ExpenseTracker.Model;
using ExpenseTracker.Repository;

namespace ExpenseTracker.Service
{
    internal class ExpenseService
    {
        private readonly IExpenseRepository _repository;

        public ExpenseService(IExpenseRepository repository)
        {
            this._repository = repository;
        }

        public void AddIncome(DateOnly date, decimal amount, IncomeSource source, string? description)
        {
            string recordId = this.GenerateUniqueId();
            Income income = new Income(recordId, date, amount, description, source);
            this._repository.AddRecord(income);
        }

        public void AddExpense(DateOnly date, decimal amount, ExpenseCategory category, string? description)
        {
            string recordId = this.GenerateUniqueId();
            Expense expense = new Expense(recordId, date, amount, description, category);
            this._repository.AddRecord(expense);
        }

        public IEnumerable<FinancialRecord> GetAllRecords()
        {
            IEnumerable<FinancialRecord> records = this._repository.GetAllRecords();
            return records.OrderByDescending(record => record.Date).ToList();
        }

        public IEnumerable<FinancialRecord> GetIncomeRecords()
        {
            IEnumerable<FinancialRecord> incomeRecords = this._repository.GetAllIncomeRecords();
            return incomeRecords.OrderByDescending(record => record.Date).ToList();
        }

        public IEnumerable<FinancialRecord> GetExpenseRecords()
        {
            IEnumerable<FinancialRecord> expenseRecords = this._repository.GetAllExpenseRecords();
            return expenseRecords.OrderByDescending(record => record.Date).ToList();
        }

        public IEnumerable<FinancialRecord> SearchByDate(DateOnly date)
        {
            return this._repository.GetByDate(date);
        }

        public IEnumerable<FinancialRecord> SearchByAmount(decimal amount)
        {
            return this._repository.GetByAmount(amount);
        }

        public IEnumerable<FinancialRecord> SearchBySource(IncomeSource source)
        {
            return this._repository.GetBySource(source);
        }

        public IEnumerable<FinancialRecord> SearchByCategory(ExpenseCategory category)
        {
            return this._repository.GetByCategory(category);
        }

        public FinancialRecord? GetRecordById(string recordId)
        {
            return this._repository.GetById(recordId);
        }

        public void DeleteRecord(FinancialRecord record)
        {
            this._repository.DeleteRecord(record);
        }

        public void EditRecordDate(FinancialRecord record, DateOnly date)
        {
            this._repository.UpdateRecordDate(record, date);
        }

        public void EditRecordAmount(FinancialRecord record, decimal amount)
        {
            this._repository.UpdateRecordAmount(record, amount);
        }

        public void EditRecordSource(Income record, IncomeSource source)
        {
            this._repository.UpdateRecordSource(record, source);
        }

        public void EditRecordCategory(Expense record, ExpenseCategory category)
        {
            this._repository.UpdateRecordCategory(record, category);
        }

        public void EditRecordDescription(FinancialRecord record, string? description)
        {
            this._repository.UpdateRecordDescription(record, description);
        }

        public bool IsRecordListEmpty()
        {
            return this._repository.RecordCount == 0;
        }

        private string GenerateUniqueId()
        {
            string recordId;
            do
            {
                recordId = Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper();
            }
            while (this._repository.RecordIdExists(recordId));

            return recordId;
        }
    }
}
