using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TimeIsMoney.Models;
using Plugin.LocalNotification;

namespace TimeIsMoney.ViewModels;
public partial class PlanDetailPageViewModel : BaseNoteViewModel
{
    public PlanDetailPageViewModel()
    {
        Plan = new Plan();
        Now = DateTime.Today;
    }
    [RelayCommand]
    public async void SavePlan()
    {
        var plan = Plan;     
        if (String.IsNullOrWhiteSpace(plan.name) || 
            String.IsNullOrWhiteSpace(plan.date) || 
            String.IsNullOrWhiteSpace(plan.time)) { return; }
        await App.NoteService.AddUpdatePlanAsync(plan);
        var newDate = DateTime.Parse(plan.date).AddHours(DateTime.Parse(plan.time).Hour).
            AddMinutes(DateTime.Parse(plan.time).Minute);
        if ((newDate - DateTime.Now).Minutes > 60) { newDate = newDate.Subtract(new TimeSpan(0, 60, 0)); }

        var request = new NotificationRequest
            {
                NotificationId = 1000,
                Title = $"{plan.name} - {plan.getFormatTime}",
                Subtitle = "Напоминание",
                Description = plan.text,
                BadgeNumber = 1,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = newDate,
                    RepeatType = NotificationRepeat.TimeInterval,
                    NotifyRepeatInterval = TimeSpan.FromMinutes(10)
                }
            };

        await LocalNotificationCenter.Current.Show(request);
        
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    public async void OnCancel()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async void PlanTappedDelete(Plan plan)
    {
        if (plan.planId == 0 || plan == null)
        {
            await Shell.Current.DisplayAlert("Танос, разлогинься!", "Удалить еще не созданное? Да ты сама неотвратимость.", "Я сделаю это.. потом");
            return;
        }

        LocalNotificationCenter.Current.ClearAll();
        LocalNotificationCenter.Current.CancelAll();
        await App.NoteService.DeletePlanAsync(plan.planId);
        await Shell.Current.GoToAsync("..");
    }
}
