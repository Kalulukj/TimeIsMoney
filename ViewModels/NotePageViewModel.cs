using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TimeIsMoney.Models;
using TimeIsMoney.Views;

namespace TimeIsMoney.ViewModels;
public partial class NotePageViewModel : BaseNoteViewModel
{

    public ObservableCollection<Note> noteList
    {
        get;
    }

    public ObservableCollection<NoteCategory> nCategoryList
    {
        get;
    }

    public ObservableCollection<NoteGroup> noteGroup { get; set; } = new ObservableCollection<NoteGroup>();

    public NotePageViewModel(INavigation navigation)
    {
        noteList = new ObservableCollection<Note>();
        nCategoryList = new ObservableCollection<NoteCategory>();
        Navigation = navigation;
    }

    [RelayCommand]
    private async void OnAddNote()
    {
        await Shell.Current.GoToAsync(nameof(DetailNotePage));
    }

    [RelayCommand]
    private async void OnAddNoteCategory()
    {
        await Shell.Current.GoToAsync(nameof(NoteCategoryDetailPage));
    }

    public void OnAppearing()
    {
        IsBusy = true;
    }

    [RelayCommand]
    private async Task LoadNote()
    {
        IsBusy = true;
        try
        {
            noteList.Clear();
            var newNoteList = await App.NoteService.GetNoteAsync();
            foreach (var item in newNoteList)
            {
                noteList.Add(item);
            }
            if (noteList.Count == 0)
            {
                DateTime exampleDate = DateTime.Now;
                noteList.Add(new Note() { name = "Так могла выглядеть ваша заметка", text = "А здесь ее описание", 
                    noteId = -1, date = exampleDate.ToString(), categoryName = "Некоторая категория", nCategoryId = 0 });
            }
            nCategoryList.Clear();
            nCategoryList.Add(new NoteCategory() { nCategoryId = 0, name = "Без категории", color = "#757575" });
            var newCategoryList = await App.NoteService.GetNoteCategoryAsync();
            foreach (var item in newCategoryList)
            {
                nCategoryList.Add(item);
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
    private async void NoteTappedEdit(Note note)
    {
        if (note == null)
        {
            return;
        }
        if (note.noteId == -1)
        {
            await Shell.Current.DisplayAlert("Поубавь обороты!", "Это всего-лишь демонстрация.", "А так хотелось...");
            return;
        }
        await Navigation.PushAsync(new DetailNotePage(note));
    }

    [RelayCommand]
    private async void NoteCategoryTappedEdit(NoteCategory noteCategory)
    {
        if (noteCategory == null)
        {
            return;
        }
        else if (noteCategory.nCategoryId == 0)
        {
            await Shell.Current.DisplayAlert("Хорошая попытка", "Тебе бы очень этого хотелось, да?", "Ну ладно :(");
            return;
        }
        await Navigation.PushAsync(new NoteCategoryDetailPage(noteCategory));
    }
}
