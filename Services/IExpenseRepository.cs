using TimeIsMoney.Models;

namespace TimeIsMoney.Services;

internal interface IExpenseRepository
{
    Task<bool> AddUpdateExpenseAsync(Expense expense);

    Task<bool> DeleteExpenseAsync(int expenseId);

    Task<Expense> GetExpenseAsync(int expenseId);

    Task<IEnumerable<Expense>> GetExpenseAsync();

    Task<bool> AddUpdateExpenseCategoryAsync(ExpenseCategory expenseCategory);

    Task<bool> DeleteExpenseCategoryAsync(int eCategoryId);

    Task<ExpenseCategory> GetExpenseCategoryAsync(int eCategoryId);

    Task<IEnumerable<ExpenseCategory>> GetExpenseCategoryAsync();
}