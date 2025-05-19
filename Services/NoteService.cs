using TimeIsMoney.Models;

namespace TimeIsMoney.Services;

public class NoteService : INoteRepository, IPlanRepository, IExpenseRepository
{
    public SQLiteAsyncConnection _database;

    public NoteService(string dbPath)
    {
        /*  Shell.Current.DisplayAlert("Как насчет синхронизации?",
  $"Найдены несинхронизированные данные от {File.GetLastWriteTime(dbPath).ToString()}." +
  "\nВосстановить?", "Да", "Нет");*/
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Note>().Wait();
        _database.CreateTableAsync<NoteCategory>().Wait();
        _database.CreateTableAsync<Plan>().Wait();
        _database.CreateTableAsync<Expense>().Wait();
        _database.CreateTableAsync<ExpenseCategory>().Wait();
    }

    public async Task<bool> AddUpdateNoteAsync(Note note)
    {
        if (note.noteId > 0)
        {
            await _database.UpdateAsync(note);
        }
        else
        {
            await _database.InsertAsync(note);
        }
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteNoteAsync(int noteId)
    {
        await _database.DeleteAsync<Note>(noteId);
        return await Task.FromResult(true);
    }

    public async Task<Note> GetNoteAsync(int noteId)
    {
        return await _database.Table<Note>().Where(n => n.noteId == noteId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Note>> GetNoteAsync()
    {
        return await Task.FromResult(await _database.Table<Note>().ToListAsync());
    }

    public async Task<bool> AddUpdateNoteCategoryAsync(NoteCategory noteCategory)
    {
        if (noteCategory.nCategoryId > 0)
        {
            await _database.UpdateAsync(noteCategory);
        }
        else
        {
            await _database.InsertAsync(noteCategory);
        }
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteNoteCategoryAsync(int nCategoryId)
    {
        await _database.DeleteAsync<NoteCategory>(nCategoryId);
        return await Task.FromResult(true);
    }

    public async Task<NoteCategory> GetNoteCategoryAsync(int nCategoryId)
    {
        return await _database.Table<NoteCategory>().Where(n => n.nCategoryId == nCategoryId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<NoteCategory>> GetNoteCategoryAsync()
    {
        return await Task.FromResult(await _database.Table<NoteCategory>().ToListAsync());
    }

    //PLAN

    public async Task<bool> AddUpdatePlanAsync(Plan plan)
    {
        if (plan.planId > 0)
        {
            await _database.UpdateAsync(plan);
        }
        else
        {
            await _database.InsertAsync(plan);
        }
        return await Task.FromResult(true);
    }

    public async Task<bool> DeletePlanAsync(int planId)
    {
        await _database.DeleteAsync<Plan>(planId);
        return await Task.FromResult(true);
    }

    public async Task<Plan> GetPlanAsync(int planId)
    {
        return await _database.Table<Plan>().Where(n => n.planId == planId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Plan>> GetPlanAsync()
    {
        return await Task.FromResult(await _database.Table<Plan>().ToListAsync());
    }

    //Expense

    public async Task<bool> AddUpdateExpenseAsync(Expense expense)
    {
        if (expense.expenseId > 0)
        {
            await _database.UpdateAsync(expense);
        }
        else
        {
            await _database.InsertAsync(expense);
        }
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteExpenseAsync(int expenseId)
    {
        await _database.DeleteAsync<Expense>(expenseId);
        return await Task.FromResult(true);
    }

    public async Task<Expense> GetExpenseAsync(int expenseId)
    {
        return await _database.Table<Expense>().Where(n => n.expenseId == expenseId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Expense>> GetExpenseAsync()
    {
        return await Task.FromResult(await _database.Table<Expense>().ToListAsync());
    }

    //ExpenseCategory

    public async Task<bool> AddUpdateExpenseCategoryAsync(ExpenseCategory expenseCategory)
    {
        if (expenseCategory.eCategoryId > 0)
        {
            await _database.UpdateAsync(expenseCategory);
        }
        else
        {
            await _database.InsertAsync(expenseCategory);
        }
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteExpenseCategoryAsync(int eCategoryId)
    {
        await _database.DeleteAsync<ExpenseCategory>(eCategoryId);
        return await Task.FromResult(true);
    }

    public async Task<ExpenseCategory> GetExpenseCategoryAsync(int eCategoryId)
    {
        return await _database.Table<ExpenseCategory>().Where(n => n.eCategoryId == eCategoryId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ExpenseCategory>> GetExpenseCategoryAsync()
    {
        return await Task.FromResult(await _database.Table<ExpenseCategory>().ToListAsync());
    }
}