using TimeIsMoney.Models;
using TimeIsMoney.ViewModels;

namespace TimeIsMoney.Views;

public partial class DetailNotePage : ContentPage
{
    private DetailNotePageViewModel detailNotePageViewModel;

    public Note Note
    {
        get; set;
    }

    public DetailNotePage()
    {
        InitializeComponent();
        this.BindingContext = detailNotePageViewModel = new DetailNotePageViewModel();
        ((DetailNotePageViewModel)BindingContext).SelectedCategoryColor = "#757575";
        ((DetailNotePageViewModel)BindingContext).SelectedCategoryId = 0;
        ((DetailNotePageViewModel)BindingContext).SelectedCategoryName = "Без категории";
    }

    public DetailNotePage(Note note)
    {
        InitializeComponent();
        this.BindingContext = detailNotePageViewModel = new DetailNotePageViewModel();

        if (note != null)
        {
            ((DetailNotePageViewModel)BindingContext).Note = note;
            ((DetailNotePageViewModel)BindingContext).SelectedCategoryColor = "#757575";
            ((DetailNotePageViewModel)BindingContext).SelectedCategoryId = 0;
            ((DetailNotePageViewModel)BindingContext).SelectedCategoryName = "Без категории";
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        detailNotePageViewModel.OnAppearing();
    }

    private void myEdit_SelectionChanged(object sender, EventArgs e)
    {
        if (myEdit.SelectedItem is NoteCategory current)
        {
            ((DetailNotePageViewModel)BindingContext).SelectedCategoryColor = current.color;
            ((DetailNotePageViewModel)BindingContext).SelectedCategoryId = current.nCategoryId;
            ((DetailNotePageViewModel)BindingContext).SelectedCategoryName = current.name;
        }
    }
}