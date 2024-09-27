using Exercise_21.Data;
using Exercise_21.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace Exercise_21.Controllers
{
    public class NoteController : Controller
    {
        private readonly INoteData _noteData;

        public NoteController(INoteData noteData)
        {
            _noteData = noteData;
        }

        public IActionResult Index()
        {
            return View(_noteData.GetNotes());
        }

        // GET: Users/Details/5
        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var user = _noteData.GetNotes().ToList().FirstOrDefault(m => m.ID == id);
            if (user == null) return NotFound();
            return View(user);
        }

        // GET: Users/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,LastName,FirstName,Patronymic,Phone,Address,Description")] Note user)
        {
            if (ModelState.IsValid)
            {
                _noteData.AddNote(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var note = new ObservableCollection<Note>(_noteData.GetNotes()).FirstOrDefault(n => n.ID == id);
            if (note == null) return NotFound();
            return View(note);
        }

        // POST: Users/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,LastName,FirstName,Patronymic,Phone,Address,Description")] Note note)
        {
            if (id != note.ID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _noteData.UpdateNote(note);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.ID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        // GET: Users/Delete/5
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var note = new ObservableCollection<Note>(_noteData.GetNotes()).FirstOrDefault(m => m.ID == id);
            if (note == null) return NotFound();
            return View(note);
        }

        // POST: Users/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var note = new ObservableCollection<Note>(_noteData.GetNotes()).FirstOrDefault(m => m.ID == id);
            if (note != null)
            {
                _noteData.RemoveNote(note);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool NoteExists(int id)
        {
            return new ObservableCollection<Note>(_noteData.GetNotes()).Any(e => e.ID == id);
        }

    }
}
