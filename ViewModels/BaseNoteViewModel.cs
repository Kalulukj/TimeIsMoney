using TimeIsMoney.Models;

namespace TimeIsMoney.ViewModels;

public partial class BaseNoteViewModel : BaseViewModel
{
    [ObservableProperty]
    private Note _note;

    [ObservableProperty]
    private Plan _plan;

    [ObservableProperty]
    private Expense _expense;

    [ObservableProperty]
    private ExpenseCategory _expenseCategory;

    [ObservableProperty]
    private NoteCategory _noteCategory;

    [ObservableProperty]
    private string _selectedCategoryColor;

    [ObservableProperty]
    private int _selectedCategoryId;

    [ObservableProperty]
    private string _selectedCategoryName;

    [ObservableProperty]
    private List<Plan> _planCalendarDays;

    [ObservableProperty]
    private string _myColor;

    [ObservableProperty]
    private DateTime _now;

    public INavigation Navigation
    {
        get; set;
    }
}