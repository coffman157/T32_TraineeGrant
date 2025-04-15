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
    public class TrainingRecController : Controller
    {
        private readonly BumcOrgContext _context;

        public TrainingRecController(BumcOrgContext context)
        {
            _context = context;
        }

        // GET: TrainingRec
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrainingRecords.ToListAsync());
        }

        // GET: TrainingRec/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecord = await _context.TrainingRecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingRecord == null)
            {
                return NotFound();
            }

            return View(trainingRecord);
        }

        // GET: TrainingRec/Create
        public IActionResult Create()
        {
            string buid = HttpContext.Session.GetString("buid");
            var trainee = _context.People.Where(a => a.Buid == buid).FirstOrDefault();
            ViewBag.buid = trainee.Buid;
            ViewBag.personid = trainee.Id;
            ViewBag.firstname = trainee.Firstname;

            ViewBag.lastname = trainee.Lastname;
            return View();
        }

        // POST: TrainingRec/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Personid,Buid,Researchfaculty,Trainingstartmm,Trainingstartyy,Trainingendmm,Trainingendyy,Stilltrainee,Status,Statusother,Presentations")] TrainingRecord trainingRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingRecord);
        }

        // GET: TrainingRec/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string buid = HttpContext.Session.GetString("buid");
            var trainee = _context.People.Where(a => a.Buid == buid).FirstOrDefault();
            ViewBag.buid = trainee.Buid;
            ViewBag.personid = trainee.Id;
            ViewBag.firstname = trainee.Firstname;

            ViewBag.lastname = trainee.Lastname;
            var trainingRecord = await _context.TrainingRecords.FindAsync(id);
            if (trainingRecord == null)
            {
                return NotFound();
            }
            return View(trainingRecord);
        }

        // POST: TrainingRec/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Personid,Buid,Researchfaculty,Trainingstartmm,Trainingstartyy,Trainingendmm,Trainingendyy,Stilltrainee,Status,Statusother,Presentations")] TrainingRecord trainingRecord)
        {
            if (id != trainingRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingRecordExists(trainingRecord.Id))
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
            return View(trainingRecord);
        }

        // GET: TrainingRec/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecord = await _context.TrainingRecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingRecord == null)
            {
                return NotFound();
            }

            return View(trainingRecord);
        }

        // POST: TrainingRec/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingRecord = await _context.TrainingRecords.FindAsync(id);
            if (trainingRecord != null)
            {
                _context.TrainingRecords.Remove(trainingRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingRecordExists(int id)
        {
            return _context.TrainingRecords.Any(e => e.Id == id);
        }
    }
}
