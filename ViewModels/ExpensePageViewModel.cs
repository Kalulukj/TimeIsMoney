using System.Collections.ObjectModel;
using TimeIsMoney.Models;
using TimeIsMoney.Views;

namespace TimeIsMoney.ViewModels;

public partial class ExpensePageViewModel : BaseNoteViewModel
{
    public ObservableCollection<Expense> expenseList
    {
        get;
    }

    public ObservableCollection<ExpenseCategory> eCategoryList
    {
        get;
    }

    public ExpensePageViewModel(INavigation navigation)
    {
        expenseList = new ObservableCollection<Expense>();
        eCategoryList = new ObservableCollection<ExpenseCategory>();
        Navigation = navigation;
    }

    [RelayCommand]
    private async void OnAddExpense()
    {
        await Shell.Current.GoToAsync(nameof(ExpenseDetailPage));
    }

    [RelayCommand]
    private async void OnAddExpenseCategory()
    {
        await Shell.Current.GoToAsync(nameof(ExpenseCategoryDetailPage));
    }

    [RelayCommand]
    private async void OnViewChart()
    {
        if (expenseList.Count > 0)
        {
            if (expenseList[0].expenseId < 0)
            {
                await Shell.Current.DisplayAlert("Вы точно этого хотите?", "Смотреть на пустую диаграмму? Вы уверены? Вы ТОЧНО уверены?", "Мне нужно подумать...");

                return;
            }
        }
        await Shell.Current.GoToAsync(nameof(ChartPage));
    }

    public void OnAppearing()
    {
        IsBusy = true;
    }

    [RelayCommand]
    private async Task LoadExpense()
    {
        IsBusy = true;
        try
        {
            expenseList.Clear();
            eCategoryList.Clear();
            eCategoryList.Add(new ExpenseCategory() { eCategoryId = 0, name = "Без категории", color = "#757575" });
            var newExpenseList = await App.NoteService.GetExpenseAsync();
            foreach (var item in newExpenseList)
            {
                expenseList.Add(item);
            }
            if (expenseList.Count == 0)
            {
                DateTime exampleDate = DateTime.Now;
                expenseList.Add(new Expense()
                {
                    name = "Но у вас их нет. Поздравляю.",
                    text = "Так выглядят расходы",
                    expenseId = -1,
                    date = exampleDate.Date.ToString(),
                    time = exampleDate.TimeOfDay.ToString(),
                    eCategoryId = 0,
                    cost = 50
                });
            }
            var newExpenseCategoryList = await App.NoteService.GetExpenseCategoryAsync();
            foreach (var item in newExpenseCategoryList)
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

    [RelayCommand]
    private async void ExpenseTappedEdit(Expense expense)
    {
        if (expense == null)
        {
            return;
        }
        if (expense.expenseId == -1)
        {
            await Shell.Current.DisplayAlert("Поубавь обороты!", "Это всего-лишь демонстрация.", "А так хотелось...");
            return;
        }
        await Navigation.PushAsync(new ExpenseDetailPage(expense));
    }

    [RelayCommand]
    private async void ExpenseCategoryTappedEdit(ExpenseCategory expenseCategory)
    {
        if (expenseCategory == null)
        {
            return;
        }
        else if (expenseCategory.eCategoryId == 0)
        {
            await Shell.Current.DisplayAlert("Хорошая попытка", "Тебе бы очень этого хотелось, да?", "Ну ладно :(");
            return;
        }
        await Navigation.PushAsync(new ExpenseCategoryDetailPage(expenseCategory));
    }
}