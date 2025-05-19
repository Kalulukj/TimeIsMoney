using DevExpress.Data.Filtering;
using DevExpress.Maui.Editors;
using TimeIsMoney.ViewModels;

namespace TimeIsMoney.Views;

public partial class CalendarPage : ContentPage
{
    CalendarPageViewModel calendarPageViewModel;
    void CustomDayCellAppearance(object sender, CustomSelectableCellAppearanceEventArgs e)
    {
        if (e.Date.DayOfWeek == DayOfWeek.Saturday || e.Date.DayOfWeek == DayOfWeek.Sunday)
        {
            e.TextColor = Color.FromArgb("#F44848");
            if (e.IsTrailing)
                e.TextColor = Color.FromRgba(e.TextColor.Red, e.TextColor.Green, e.TextColor.Blue, 0.5);
        }

        foreach (var item in ((CalendarPageViewModel)BindingContext).PlanCalendarDays)
        {
            if (e.Date.ToString().Equals(item.date))
            {
                e.FontAttributes = FontAttributes.Bold;
                e.TextColor = Color.FromArgb("#fb8500");
            }
        }
    }

    private void CustomDayOfWeekCellAppearance(object sender, CustomDayOfWeekCellAppearanceEventArgs e)
    {
        if (e.DayOfWeek == DayOfWeek.Saturday || e.DayOfWeek == DayOfWeek.Sunday)
            e.TextColor = Color.FromArgb("#F44848");
    }

    public CalendarPage()
    {
        InitializeComponent();
        this.BindingContext = calendarPageViewModel = new CalendarPageViewModel(Navigation);
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        calendarPageViewModel.OnAppearing();
        planCalendar.SelectedDate = null;
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        calendarExpander.IsExpanded = !calendarExpander.IsExpanded;
        PlanView.FilterExpression = CriteriaOperator.Parse("planId>=-1");
        emptyLabel.Text = "";
        planCalendar.SelectedDate = null;
    }

    private void planCalendar_SelectedDateChanged(object sender, EventArgs e)
    {
        if (planCalendar.SelectedDate == null) { return; }
        PlanView.FilterExpression = CriteriaOperator.Parse($"date=?", planCalendar.SelectedDate.ToString());
        if (PlanView.VisibleItemCount <= 0) { emptyLabel.Text = "ѕохоже, что на данную дату у вас планов нет..."; }
        else { emptyLabel.Text = ""; }
    }
}