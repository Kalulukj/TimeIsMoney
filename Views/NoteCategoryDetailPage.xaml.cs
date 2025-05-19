using TimeIsMoney.Models;
using TimeIsMoney.ViewModels;

namespace TimeIsMoney.Views;

public partial class NoteCategoryDetailPage : ContentPage
{
    public NoteCategory NoteCategory
    {
        get; set;
    }

    public NoteCategoryDetailPage()
    {
        InitializeComponent();
        BindingContext = new NoteCategoryDetailPageViewModel();
        if (ColorPicker.SelectedItem is ColorCircle current)
            ((NoteCategoryDetailPageViewModel)BindingContext).MyColor = current.strColor;
    }

    public NoteCategoryDetailPage(NoteCategory noteCategory)
    {
        InitializeComponent();
        this.BindingContext = new NoteCategoryDetailPageViewModel();

        if (noteCategory != null)
        {
            ((NoteCategoryDetailPageViewModel)BindingContext).NoteCategory = noteCategory;
            ((NoteCategoryDetailPageViewModel)BindingContext).SelectedCategoryColor = noteCategory.color;
        }
    }

    private void ColorPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ColorPicker.SelectedItem is ColorCircle current)
            ((NoteCategoryDetailPageViewModel)BindingContext).MyColor = current.strColor;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        nCategoryName.HasError = string.IsNullOrWhiteSpace(nCategoryName.Text);
        ((NoteCategoryDetailPageViewModel)BindingContext).SaveNoteCategory();
    }
}