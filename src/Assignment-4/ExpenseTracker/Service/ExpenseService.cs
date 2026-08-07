using ExpenseTracker.Enums;
using ExpenseTracker.Model;
using ExpenseTracker.Repository;

namespace ExpenseTracker.Service
{
    internal class ExpenseService
    {
        private readonly ExpenseRepository _repository;

        public ExpenseService(ExpenseRepository repository)
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

        public List<FinancialRecord> GetAllRecords()
        {
            return this._repository.GetAllRecords();
        }

        public List<FinancialRecord> GetIncomeRecords()
        {
            return this._repository.GetAllIncomeRecords();
        }

        public List<FinancialRecord> GetExpenseRecords()
        {
            return this._repository.GetAllExpenseRecords();
        }

        public List<FinancialRecord> SearchByDate(DateOnly date)
        {
            return this._repository.GetByDate(date);
        }

        public List<FinancialRecord> SearchByAmount(decimal amount)
        {
            return this._repository.GetByAmount(amount);
        }

        public List<FinancialRecord> SearchBySource(IncomeSource source)
        {
            return this._repository.GetBySource(source);
        }

        public List<FinancialRecord> SearchByCategory(ExpenseCategory category)
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
