using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using TimeIsMoney.Models;
using TimeIsMoney.Views;

namespace TimeIsMoney.ViewModels;
public partial class CalendarPageViewModel : BaseNoteViewModel
{
    public ObservableCollection<Plan> planList
    {
        get;
    }
    public CalendarPageViewModel(INavigation navigation)
    {
        planList = new ObservableCollection<Plan>();
        PlanCalendarDays = new List<Plan>();
        Navigation = navigation;
    }

    [RelayCommand]
    private async void OnAddPlan()
    {
        await Shell.Current.GoToAsync(nameof(PlanDetailPage));
    }

    public void OnAppearing()
    {
        IsBusy = true;
    }

    [RelayCommand]
    private async Task LoadPlan()
    {
        IsBusy = true;
        try
        {
            planList.Clear();
            PlanCalendarDays.Clear();
            var newPlanList = await App.NoteService.GetPlanAsync();
            foreach (var item in newPlanList)
            {
                planList.Add(item);
                PlanCalendarDays.Add(item);
            }
            if (planList.Count == 0) {
                DateTime exampleDate = DateTime.Now;
                planList.Add(new Plan() { name = "Так выглядят планы", text="Но у вас их нет :)", planId = -1, date = exampleDate.Date.ToString(), time = exampleDate.TimeOfDay.ToString() });
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
    private async void PlanTappedEdit(Plan plan)
    {
        if (plan == null)
        {
            return;
        }
        if (plan.planId == -1) {
            await Shell.Current.DisplayAlert("Поубавь обороты!", "Это всего-лишь демонстрация.", "А так хотелось...");
            return;
        }
        await Navigation.PushAsync(new PlanDetailPage(plan));
    }
}
