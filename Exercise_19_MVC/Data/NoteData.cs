using Exercise_21.Models;
using System.Collections.Generic;

namespace Exercise_21.Data
{

    public class NoteData : INoteData
    {
        private readonly PhoneBookContext context;

        public NoteData(PhoneBookContext Context)
        {
            context = Context;
        }

        public void AddNote(Note note)
        {
            context.Notes.Add(note);
            context.SaveChanges();
        }

        public IEnumerable<Note> GetNotes()
        {
            return context.Notes;
        }

        public void RemoveNote(Note note)
        {
            context.Notes.Remove(note);
            context.SaveChanges();
        }

        public void UpdateNote(Note note)
        {
            context.Notes.Update(note);
            context.SaveChanges();
        }
    }
}
