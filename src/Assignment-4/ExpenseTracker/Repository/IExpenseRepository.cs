using ExpenseTracker.Enums;
using ExpenseTracker.Model;

namespace ExpenseTracker.Repository
{
    internal interface IExpenseRepository
    {
        int RecordCount { get; }

        void AddRecord(FinancialRecord record);

        IEnumerable<FinancialRecord> GetAllRecords();

        IEnumerable<FinancialRecord> GetAllIncomeRecords();

        IEnumerable<FinancialRecord> GetAllExpenseRecords();

        IEnumerable<FinancialRecord> GetByDate(DateOnly date);

        IEnumerable<FinancialRecord> GetByAmount(decimal amount);

        IEnumerable<FinancialRecord> GetBySource(IncomeSource source);

        IEnumerable<FinancialRecord> GetByCategory(ExpenseCategory category);

        FinancialRecord? GetById(string recordId);

        void DeleteRecord(FinancialRecord record);

        void UpdateRecordDate(FinancialRecord record, DateOnly date);

        void UpdateRecordAmount(FinancialRecord record, decimal amount);

        void UpdateRecordSource(Income record, IncomeSource source);

        void UpdateRecordCategory(Expense record, ExpenseCategory category);

        void UpdateRecordDescription(FinancialRecord record, string? description);

        bool RecordIdExists(string recordId);
    }
}