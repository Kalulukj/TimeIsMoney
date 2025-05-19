using System.Collections.ObjectModel;
using TimeIsMoney.Models;

namespace TimeIsMoney.ViewModels;

public partial class ExpenseDetailPageViewModel : BaseNoteViewModel
{
    public ObservableCollection<ExpenseCategory> eCategoryList
    {
        get;
    }

    public ExpenseDetailPageViewModel()
    {
        eCategoryList = new ObservableCollection<ExpenseCategory>();
        Expense = new Expense();
        Now = DateTime.Now;
    }

    [RelayCommand]
    public async void SaveExpense()
    {
        var expense = Expense;
        if (String.IsNullOrWhiteSpace(expense.text) ||
    String.IsNullOrWhiteSpace(expense.date) ||
    String.IsNullOrWhiteSpace(expense.time) || expense.cost <= 0) { return; }
        expense.color = SelectedCategoryColor;
        expense.eCategoryId = SelectedCategoryId;
        expense.name = SelectedCategoryName;
        await App.NoteService.AddUpdateExpenseAsync(expense);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task LoadECategory()
    {
        IsBusy = true;
        try
        {
            eCategoryList.Clear();
            eCategoryList.Add(new ExpenseCategory() { eCategoryId = 0, name = "Без категории", color = "#757575" });
            var newPlanList = await App.NoteService.GetExpenseCategoryAsync();
            foreach (var item in newPlanList)
            {
                eCategoryList.Add(item);
            }
        }
        catch (Exception)
        {
        }
        finally
        {
            IsBusy = false;
        }
    }

    public void OnAppearing()
    {
        IsBusy = true;
    }

    [RelayCommand]
    public async void OnCancel()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async void ExpenseTappedDelete(Expense expense)
    {
        if (expense.expenseId == 0 || expense == null)
        {
            await Shell.Current.DisplayAlert("Танос, разлогинься!", "Удалить еще не созданное? Да ты сама неотвратимость.", "Я сделаю это.. потом");
            return;
        }
        await App.NoteService.DeleteExpenseAsync(expense.expenseId);
        await Shell.Current.GoToAsync("..");
    }
}