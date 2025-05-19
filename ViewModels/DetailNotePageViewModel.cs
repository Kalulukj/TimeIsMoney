using System.Collections.ObjectModel;
using TimeIsMoney.Models;

namespace TimeIsMoney.ViewModels;

public partial class DetailNotePageViewModel : BaseNoteViewModel
{
    public ObservableCollection<NoteCategory> nCategoryList
    {
        get;
    }

    public DetailNotePageViewModel()
    {
        nCategoryList = new ObservableCollection<NoteCategory>();
        Note = new Note();
    }

    [RelayCommand]
    public async void SaveNote()
    {
        var note = Note;
        if (note.text == null || note == null)
        {
            await Shell.Current.DisplayAlert("Я так много прошу?", "Здесь всего одно ОБЯЗАТЕЛЬНОЕ поле, и это - текст.", "Понятно");
            return;
        }
        DateTime now = DateTime.Now;
        note.date = now.ToString();
        note.color = SelectedCategoryColor;
        note.nCategoryId = SelectedCategoryId;
        note.categoryName = SelectedCategoryName;
        await App.NoteService.AddUpdateNoteAsync(note);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task LoadNCategory()
    {
        IsBusy = true;
        try
        {
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

    public void OnAppearing()
    {
        IsBusy = true;
    }

    [RelayCommand]
    public async void OnCancel()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async void NoteTappedDelete(Note note)
    {
        if (note.noteId == 0 || note == null)
        {
            await Shell.Current.DisplayAlert("Танос, разлогинься!", "Удалить еще не созданное? Да ты сама неотвратимость", "Я сделаю это.. потом");
            return;
        }
        await App.NoteService.DeleteNoteAsync(note.noteId);
        await Shell.Current.GoToAsync("..");
    }
}