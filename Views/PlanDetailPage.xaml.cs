using TimeIsMoney.Models;
using TimeIsMoney.ViewModels;

namespace TimeIsMoney.Views;

public partial class PlanDetailPage : ContentPage
{
    public Plan Plan
    {
        get; set;
    }

    public PlanDetailPage()
    {
        InitializeComponent();
        BindingContext = new PlanDetailPageViewModel();
    }

    public PlanDetailPage(Plan plan)
    {
        InitializeComponent();
        this.BindingContext = new PlanDetailPageViewModel();

        if (plan != null)
        {
            ((PlanDetailPageViewModel)BindingContext).Plan = plan;
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        planName.HasError = string.IsNullOrWhiteSpace(planName.Text);
        planDate.HasError = string.IsNullOrWhiteSpace(planDate.Date.ToString());
        planTime.HasError = string.IsNullOrWhiteSpace(planTime.Time.ToString());
        planDate.HeightRequest = 70; planTime.HeightRequest = 70;
        ((PlanDetailPageViewModel)BindingContext).SavePlan();
    }
}