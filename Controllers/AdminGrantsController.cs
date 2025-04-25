using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T32_TraineeGrant;

namespace T32_TraineeGrant.Controllers
{
    public class AdminGrantsController : Controller
    {
        private readonly BumcOrgContext _context;

        public AdminGrantsController(BumcOrgContext context)
        {
            _context = context;
        }

        // GET: AdminGrants
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrainingGrants.ToListAsync());
        }

        // GET: AdminGrants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingGrant = await _context.TrainingGrants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingGrant == null)
            {
                return NotFound();
            }

            return View(trainingGrant);
        }

        // GET: AdminGrants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminGrants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] TrainingGrant trainingGrant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingGrant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingGrant);
        }

        // GET: AdminGrants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingGrant = await _context.TrainingGrants.FindAsync(id);
            if (trainingGrant == null)
            {
                return NotFound();
            }
            return View(trainingGrant);
        }

        // POST: AdminGrants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] TrainingGrant trainingGrant)
        {
            if (id != trainingGrant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingGrant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingGrantExists(trainingGrant.Id))
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
            return View(trainingGrant);
        }

        // GET: AdminGrants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingGrant = await _context.TrainingGrants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingGrant == null)
            {
                return NotFound();
            }

            return View(trainingGrant);
        }

        // POST: AdminGrants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingGrant = await _context.TrainingGrants.FindAsync(id);
            if (trainingGrant != null)
            {
                _context.TrainingGrants.Remove(trainingGrant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingGrantExists(int id)
        {
            return _context.TrainingGrants.Any(e => e.Id == id);
        }
    }
}
