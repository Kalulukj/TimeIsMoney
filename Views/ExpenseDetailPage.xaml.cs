using System.Collections.ObjectModel;
using System.Globalization;
using TimeIsMoney.Models;
using TimeIsMoney.ViewModels;
using Microsoft.Maui.Platform;

namespace TimeIsMoney.Views;

public partial class ExpenseDetailPage : ContentPage
{
    ExpenseDetailPageViewModel expenseDetailPageViewModel;
    public Expense Expense
    {
        get; set;
    }

    public ExpenseDetailPage()
    {
        InitializeComponent();
        this.BindingContext = expenseDetailPageViewModel = new ExpenseDetailPageViewModel();
        ((ExpenseDetailPageViewModel)BindingContext).SelectedCategoryColor = "#757575";
        ((ExpenseDetailPageViewModel)BindingContext).SelectedCategoryId = 0;
        ((ExpenseDetailPageViewModel)BindingContext).SelectedCategoryName = "Без категории";
    }

    public ExpenseDetailPage(Expense expense)
    {
        InitializeComponent();
        this.BindingContext = expenseDetailPageViewModel = new ExpenseDetailPageViewModel();

        if (expense != null)
        {
            ((ExpenseDetailPageViewModel)BindingContext).Expense = expense;
            ((ExpenseDetailPageViewModel)BindingContext).SelectedCategoryColor = "#757575";
            ((ExpenseDetailPageViewModel)BindingContext).SelectedCategoryId = 0;
            ((ExpenseDetailPageViewModel)BindingContext).SelectedCategoryName = "Без категории";
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        expenseDetailPageViewModel.OnAppearing();
    }

    private void myEdit_SelectionChanged(object sender, EventArgs e)
    {
        if (myEdit.SelectedItem is ExpenseCategory current)
        {
            ((ExpenseDetailPageViewModel)BindingContext).SelectedCategoryColor = current.color;
            ((ExpenseDetailPageViewModel)BindingContext).SelectedCategoryId = current.eCategoryId;
            ((ExpenseDetailPageViewModel)BindingContext).SelectedCategoryName = current.name;
        }
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        expenseName.HasError = string.IsNullOrWhiteSpace(expenseName.Text);
        expenseDate.HasError = string.IsNullOrWhiteSpace(expenseDate.Date.ToString());
        expenseTime.HasError = string.IsNullOrWhiteSpace(expenseTime.Time.ToString());
        expenseCost.HasError = (expenseCost.Value <= 0);
        ((ExpenseDetailPageViewModel)BindingContext).SaveExpense();
    }
}