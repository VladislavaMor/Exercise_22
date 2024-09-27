using Exercise_21.Models;
using System.Collections.Generic;

namespace Exercise_21.Data
{
    public interface INoteData
    {
        IEnumerable<Note> GetNotes();
        void AddNote(Note note);
        void RemoveNote(Note note);
        void UpdateNote(Note note);

    }
}
