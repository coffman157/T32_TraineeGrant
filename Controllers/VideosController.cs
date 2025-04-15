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
    public class VideosController : Controller
    {
        private readonly BumcOrgContext _context;

        public VideosController(BumcOrgContext context)
        {
            _context = context;
        }

        // GET: Videos
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrainingRecordVideos.ToListAsync());
        }

        // GET: Videos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecordVideo = await _context.TrainingRecordVideos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingRecordVideo == null)
            {
                return NotFound();
            }

            return View(trainingRecordVideo);
        }

        // GET: Videos/Create
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

        // POST: Videos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Personid,Buid,Trainingrecordid,Authors,Title,Website,Dateuploaded,Url")] TrainingRecordVideo trainingRecordVideo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingRecordVideo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingRecordVideo);
        }

        // GET: Videos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecordVideo = await _context.TrainingRecordVideos.FindAsync(id);
            if (trainingRecordVideo == null)
            {
                return NotFound();
            }
            return View(trainingRecordVideo);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Personid,Buid,Trainingrecordid,Authors,Title,Website,Dateuploaded,Url")] TrainingRecordVideo trainingRecordVideo)
        {
            if (id != trainingRecordVideo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingRecordVideo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingRecordVideoExists(trainingRecordVideo.Id))
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
            return View(trainingRecordVideo);
        }

        // GET: Videos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecordVideo = await _context.TrainingRecordVideos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingRecordVideo == null)
            {
                return NotFound();
            }

            return View(trainingRecordVideo);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingRecordVideo = await _context.TrainingRecordVideos.FindAsync(id);
            if (trainingRecordVideo != null)
            {
                _context.TrainingRecordVideos.Remove(trainingRecordVideo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingRecordVideoExists(int id)
        {
            return _context.TrainingRecordVideos.Any(e => e.Id == id);
        }
    }
}
