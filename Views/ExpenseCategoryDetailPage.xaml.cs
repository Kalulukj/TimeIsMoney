using TimeIsMoney.Models;
using TimeIsMoney.ViewModels;

namespace TimeIsMoney.Views;

public partial class ExpenseCategoryDetailPage : ContentPage
{
    public ExpenseCategory ExpenseCategory
    {
        get; set;
    }

    public ExpenseCategoryDetailPage()
    {
        InitializeComponent();
        BindingContext = new ExpenseCategoryDetailPageViewModel();
        if (ColorPicker.SelectedItem is ColorCircle current)
            ((ExpenseCategoryDetailPageViewModel)BindingContext).MyColor = current.strColor;
    }

    private void ColorPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ColorPicker.SelectedItem is ColorCircle current)
            ((ExpenseCategoryDetailPageViewModel)BindingContext).MyColor = current.strColor;
    }

    public ExpenseCategoryDetailPage(ExpenseCategory expenseCategory)
    {
        InitializeComponent();
        this.BindingContext = new ExpenseCategoryDetailPageViewModel();

        if (expenseCategory != null)
        {
            ((ExpenseCategoryDetailPageViewModel)BindingContext).ExpenseCategory = expenseCategory;
            ((ExpenseCategoryDetailPageViewModel)BindingContext).SelectedCategoryColor = expenseCategory.color;
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        eCategoryName.HasError = string.IsNullOrWhiteSpace(eCategoryName.Text);
        ((ExpenseCategoryDetailPageViewModel)BindingContext).SaveExpenseCategory();
    }
}