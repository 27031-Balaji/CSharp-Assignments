using ExpenseTracker.Enums;
using ExpenseTracker.Enums;
using ExpenseTracker.Repository;

namespace ExpenseTracker.Service
{
    internal class ExpenseService
    {
        private readonly InMemoryExpenseRepository _repository;

        public ExpenseService(InMemoryExpenseRepository repository)
        {
            this._repository = repository;
        }

        public void AddIncome(DateOnly date, decimal amount, IncomeSource source, string? description)
        {
            string recordId = this.GenerateUniqueId();
            Income income = new Income(recordId, date, amount, description, source);
            this._repository.AddRecord(income);
            this._repository.Balance += amount;
        }

        public void AddExpense(DateOnly date, decimal amount, ExpenseCategory category, string? description)
        {
            string recordId = this.GenerateUniqueId();
            Expense expense = new Expense(recordId, date, amount, description, category);
            this._repository.AddRecord(expense);
            this._repository.Balance -= amount;
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
