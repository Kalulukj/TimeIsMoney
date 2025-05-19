using TimeIsMoney.Models;

namespace TimeIsMoney.Services;

public partial class SettingsService : ObservableObject
{
    private static SettingsService _instance;
    public static SettingsService Instance => _instance ??= new SettingsService();

    private SettingsService()
    {
        Theme = Theme.System;
    }

    [ObservableProperty]
    private Theme _theme;
}