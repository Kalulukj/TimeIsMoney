using TimeIsMoney.Models;
using TimeIsMoney.ViewModels;

namespace TimeIsMoney.Views;

public partial class NotePage : ContentPage
{
    private NotePageViewModel notePageViewModel;

    public NotePage()
    {
        InitializeComponent();
        this.BindingContext = notePageViewModel = new NotePageViewModel(Navigation);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        notePageViewModel.OnAppearing();
        myEdit.SelectedIndex = -1;
        NoteView.FilterExpression = CriteriaOperator.Parse("nCategoryId>=0");
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        myExpander.IsExpanded = !myExpander.IsExpanded;
        myEdit.SelectedIndex = -1;
        emptyLabel.Text = "";
        NoteView.FilterExpression = CriteriaOperator.Parse("nCategoryId>=0");
    }

    private void myEdit_SelectionChanged(object sender, EventArgs e)
    {
        if (myEdit.SelectedItem is NoteCategory current)
        {
            NoteView.FilterExpression = new BinaryOperator("nCategoryId", current.nCategoryId);
            if (NoteView.VisibleItemCount <= 0) { emptyLabel.Text = "Похоже, что таких заметок у вас еще нет..."; }
            else { emptyLabel.Text = ""; }
        }
    }
}