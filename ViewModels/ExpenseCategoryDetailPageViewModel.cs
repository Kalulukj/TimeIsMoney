using System.Collections.ObjectModel;
using TimeIsMoney.Models;

namespace TimeIsMoney.ViewModels;

public partial class ExpenseCategoryDetailPageViewModel : BaseNoteViewModel
{
    public ObservableCollection<ColorCircle> origin_list
    {
        get; set;
    } = new ObservableCollection<ColorCircle>()
    {
        new ColorCircle() { strColor = "#42A5F5", color = Color.FromArgb("#42A5F5"), colorId = 0 },
        new ColorCircle() { strColor = "#EC407A", color = Color.FromArgb("#EC407A"), colorId = 1 },
        new ColorCircle() { strColor = "#26C6DA", color = Color.FromArgb("#26C6DA"), colorId = 2 },
        new ColorCircle() { strColor = "#AB47BC", color = Color.FromArgb("#AB47BC"), colorId = 3 },
        new ColorCircle() { strColor = "#EF5350", color = Color.FromArgb("#EF5350"), colorId = 4 },
        new ColorCircle() { strColor = "#D59E17", color = Color.FromArgb("#D59E17"), colorId = 5 },
        new ColorCircle() { strColor = "#FFA726", color = Color.FromArgb("#FFA726"), colorId = 6 },
        new ColorCircle() { strColor = "#66BB6A", color = Color.FromArgb("#66BB6A"), colorId = 7 },
        new ColorCircle() { strColor = "#8D6E63", color = Color.FromArgb("#8D6E63"), colorId = 8 },
        new ColorCircle() { strColor = "#78909C", color = Color.FromArgb("#78909C"), colorId = 9 },
        new ColorCircle() { strColor = "#26A69A", color = Color.FromArgb("#26A69A"), colorId = 10 },
        new ColorCircle() { strColor = "#5C6BC0", color = Color.FromArgb("#5C6BC0"), colorId = 11 },
        new ColorCircle() { strColor = "#7E57C2", color = Color.FromArgb("#7E57C2"), colorId = 12 },
        new ColorCircle() { strColor = "#FF7043", color = Color.FromArgb("#FF7043"), colorId = 13 },
        new ColorCircle() { strColor = "#00C853", color = Color.FromArgb("#00C853"), colorId = 14 },
        new ColorCircle() { strColor = "#8BC34A", color = Color.FromArgb("#8BC34A"), colorId = 15 },
        new ColorCircle() { strColor = "#FF8A80", color = Color.FromArgb("#FF8A80"), colorId = 16 },
        new ColorCircle() { strColor = "#FF80AB", color = Color.FromArgb("#FF80AB"), colorId = 17 }
    };

    public ObservableCollection<Expense> expenseList
    {
        get;
    }

    public ExpenseCategoryDetailPageViewModel()
    {
        expenseList = new ObservableCollection<Expense>();
        ExpenseCategory = new ExpenseCategory();
    }

    [RelayCommand]
    public async void SaveExpenseCategory()
    {
        var expenseCategory = ExpenseCategory;
        if (String.IsNullOrWhiteSpace(expenseCategory.name)) { return; }
        if (MyColor == null)
        {
            expenseCategory.color = SelectedCategoryColor;
        }
        else
        {
            expenseCategory.color = MyColor;
        }
        if (expenseCategory.eCategoryId > 0)
        {
            expenseList.Clear();
            var newExpenseList = await App.NoteService.GetExpenseAsync();
            foreach (var item in newExpenseList)
            {
                if (item.eCategoryId == expenseCategory.eCategoryId)
                {
                    item.color = expenseCategory.color;
                    item.name = expenseCategory.name;
                    await App.NoteService.AddUpdateExpenseAsync(item);
                }
            }
        }
        await App.NoteService.AddUpdateExpenseCategoryAsync(expenseCategory);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    public async void OnCancel()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async void ExpenseCategoryTappedDelete(ExpenseCategory expenseCategory)
    {
        if (expenseCategory.eCategoryId == 0 || expenseCategory == null)
        {
            await Shell.Current.DisplayAlert("Танос, разлогинься!", "Удалить еще не созданное? Да ты сама неотвратимость", "Я сделаю это.. потом");
            return;
        }
        if (expenseCategory.eCategoryId > 0)
        {
            expenseList.Clear();
            var newExpenseList = await App.NoteService.GetExpenseAsync();

            foreach (var item in newExpenseList)
            {
                if (item.eCategoryId == expenseCategory.eCategoryId)
                {
                    await App.NoteService.DeleteExpenseAsync(item.expenseId);
                }
            }
        }
        await App.NoteService.DeleteExpenseCategoryAsync(expenseCategory.eCategoryId);
        await Shell.Current.GoToAsync("..");
    }
}