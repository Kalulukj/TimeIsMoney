using DevExpress.Data.Filtering;
using TimeIsMoney.Models;
using DevExpress.Maui.Editors;
using TimeIsMoney.ViewModels;

namespace TimeIsMoney.Views;

public partial class ExpensePage : ContentPage
{
    ExpensePageViewModel expensePageViewModel;

    void CustomDayCellAppearance(object sender, CustomSelectableCellAppearanceEventArgs e)
    {
        if (e.Date.DayOfWeek == DayOfWeek.Saturday || e.Date.DayOfWeek == DayOfWeek.Sunday)
        {
            e.TextColor = Color.FromArgb("#F44848");
            if (e.IsTrailing)
                e.TextColor = Color.FromRgba(e.TextColor.Red, e.TextColor.Green, e.TextColor.Blue, 0.5);
        }
    }

    private void CustomDayOfWeekCellAppearance(object sender, CustomDayOfWeekCellAppearanceEventArgs e)
    {
        if (e.DayOfWeek == DayOfWeek.Saturday || e.DayOfWeek == DayOfWeek.Sunday)
            e.TextColor = Color.FromArgb("#F44848");
    }
    public ExpensePage()
    {
        InitializeComponent();
        this.BindingContext = expensePageViewModel = new ExpensePageViewModel(Navigation);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        expensePageViewModel.OnAppearing();

    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        if (myExpander.IsExpanded != true)
        {
            myExpander.IsExpanded = !myExpander.IsExpanded;
        }
        CalendarView.IsVisible = false;
        myEdit.IsVisible = !myEdit.IsVisible;
        myEdit.SelectedIndex = -1;
        emptyLabel.Text = "";
        ExpenseView.FilterExpression = CriteriaOperator.Parse("eCategoryId>=0");
    }

    private void myEdit_SelectionChanged(object sender, EventArgs e)
    {
        if (myEdit.SelectedItem is ExpenseCategory current)
        {
            ExpenseView.FilterExpression = new BinaryOperator("eCategoryId", current.eCategoryId);
            if (ExpenseView.VisibleItemCount <= 0) { emptyLabel.Text = "ѕохоже, что подобных расходов у вас еще не было..."; }
            else { emptyLabel.Text = ""; }
        }
    }
    private void planCalendar_SelectedDateChanged(object sender, EventArgs e)
    {
        if (expenseCalendar.SelectedDate == null) { return; }
        ExpenseView.FilterExpression = CriteriaOperator.Parse($"date=?", expenseCalendar.SelectedDate.ToString());
        if (ExpenseView.VisibleItemCount <= 0) { emptyLabel.Text = "ѕохоже, что в данный день вы не потратились..."; }
        else { emptyLabel.Text = ""; }
    }
    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        if (myExpander.IsExpanded != true)
        {
            myExpander.IsExpanded = !myExpander.IsExpanded;
        }
        myEdit.IsVisible = false;
        CalendarView.IsVisible = !CalendarView.IsVisible;
        ExpenseView.FilterExpression = CriteriaOperator.Parse("eCategoryId>=0");
        emptyLabel.Text = "";
        expenseCalendar.SelectedDate = null;
    }
}