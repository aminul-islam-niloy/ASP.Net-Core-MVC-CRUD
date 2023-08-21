using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD.Data;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class PersonInfoesController : Controller
    {
        private readonly CRUDContext _context;

        public PersonInfoesController(CRUDContext context)
        {
            _context = context;
        }

        // GET: PersonInfoes
        public async Task<IActionResult> Index()
        {
              return _context.PersonInfo != null ? 
                          View(await _context.PersonInfo.ToListAsync()) :
                          Problem("Entity set 'CRUDContext.PersonInfo'  is null.");
        }

        // GET: PersonInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PersonInfo == null)
            {
                return NotFound();
            }

            var personInfo = await _context.PersonInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personInfo == null)
            {
                return NotFound();
            }

            return View(personInfo);
        }

        // GET: PersonInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Phone,Email")] PersonInfo personInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personInfo);
        }

        // GET: PersonInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PersonInfo == null)
            {
                return NotFound();
            }

            var personInfo = await _context.PersonInfo.FindAsync(id);
            if (personInfo == null)
            {
                return NotFound();
            }
            return View(personInfo);
        }

        // POST: PersonInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Phone,Email")] PersonInfo personInfo)
        {
            if (id != personInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonInfoExists(personInfo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(personInfo);
        }

        // GET: PersonInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PersonInfo == null)
            {
                return NotFound();
            }

            var personInfo = await _context.PersonInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personInfo == null)
            {
                return NotFound();
            }

            return View(personInfo);
        }

        // POST: PersonInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PersonInfo == null)
            {
                return Problem("Entity set 'CRUDContext.PersonInfo'  is null.");
            }
            var personInfo = await _context.PersonInfo.FindAsync(id);
            if (personInfo != null)
            {
                _context.PersonInfo.Remove(personInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonInfoExists(int id)
        {
          return (_context.PersonInfo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
