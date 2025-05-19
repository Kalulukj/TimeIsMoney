using System.Collections.ObjectModel;
using TimeIsMoney.Models;
using TimeIsMoney.ViewModels;

namespace TimeIsMoney.Views;

public partial class ChartPage : ContentPage
{
    ChartPageViewModel chartPageViewModel;
    private static bool flagDiagramm = false;
    public ChartPage()
	{
		InitializeComponent();
        this.BindingContext = chartPageViewModel = new ChartPageViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        chartPageViewModel.OnAppearing();
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        switch (flagDiagramm)
        {
            case false:
                {
                    pieDiagramm.DataSource = ((ChartPageViewModel)BindingContext).groupList2;
                    pieHint.PointTextPattern = "{L}: {V}";
                    titleLabel.Text = "Диаграмма (количество)";

                    flagDiagramm = true;
                    break;
                }
            case true:
                {
                    pieDiagramm.DataSource = ((ChartPageViewModel)BindingContext).groupList;
                    pieHint.PointTextPattern = "{L}: {V} BYN";
                    titleLabel.Text = "Диаграмма (сумма)";

                    flagDiagramm = false;
                    break;
                }
        }
    }
}