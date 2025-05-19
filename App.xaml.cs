using System.ComponentModel;
using TimeIsMoney.Services;
using TimeIsMoney.Views;

namespace TimeIsMoney;

public partial class App : Application
{
    public static NoteService _noteService;

    public static NoteService NoteService
    {
        get
        {
            if (_noteService == null)
            {
                /* _noteService = new NoteService(Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), "time_is_money.db"));*/
                _noteService = new NoteService(Path.Combine("/storage/emulated/0/Documents", "time_is_money.db"));
            }
            return _noteService;
        }
    }

    public App()
    {
        InitializeComponent();
        LocalNotificationCenter.Current.NotificationActionTapped += Current_NotificationActionTapped;
        Routing.RegisterRoute(nameof(NotePage), typeof(NotePage));
        Routing.RegisterRoute(nameof(DetailNotePage), typeof(DetailNotePage));
        Routing.RegisterRoute(nameof(NoteCategoryDetailPage), typeof(NoteCategoryDetailPage));
        Routing.RegisterRoute(nameof(CalendarPage), typeof(CalendarPage));
        Routing.RegisterRoute(nameof(PlanDetailPage), typeof(PlanDetailPage));
        Routing.RegisterRoute(nameof(ExpensePage), typeof(ExpensePage));
        Routing.RegisterRoute(nameof(ExpenseDetailPage), typeof(ExpenseDetailPage));
        Routing.RegisterRoute(nameof(ExpenseCategoryDetailPage), typeof(ExpenseCategoryDetailPage));
        Routing.RegisterRoute(nameof(ChartPage), typeof(ChartPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        MainPage = new AppShell();
        SetTheme();

        // subscribe to changes in the settings
        SettingsService.Instance.PropertyChanged += OnSettingsPropertyChanged;
    }

    private void OnSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SettingsService.Theme))
        {
            SetTheme();
        }
    }

    private void SetTheme()
    {
        UserAppTheme = SettingsService.Instance?.Theme != null
                     ? SettingsService.Instance.Theme.AppTheme
                     : AppTheme.Unspecified;
    }

    private void Current_NotificationActionTapped(NotificationActionEventArgs e)
    {
        if (e.IsDismissed)
        {
        }
        if (e.IsTapped)
        {
            LocalNotificationCenter.Current.ClearAll();
            LocalNotificationCenter.Current.CancelAll();
            Shell.Current.GoToAsync(nameof(CalendarPage));
        }
    }
}