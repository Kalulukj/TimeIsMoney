using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeIsMoney.Models;

namespace TimeIsMoney.Services;
internal interface INoteRepository
{
    Task<bool> AddUpdateNoteAsync(Note note);
    Task<bool> DeleteNoteAsync(int noteId);
    Task<Note> GetNoteAsync(int noteId);
    Task<IEnumerable<Note>> GetNoteAsync();

    Task<bool> AddUpdateNoteCategoryAsync(NoteCategory nCategory);
    Task<bool> DeleteNoteCategoryAsync(int nCategoryId);
    Task<NoteCategory> GetNoteCategoryAsync(int nCategoryId);
    Task<IEnumerable<NoteCategory>> GetNoteCategoryAsync();
}
