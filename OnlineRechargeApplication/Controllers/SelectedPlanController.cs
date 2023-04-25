using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineRechargeApplication.Data;
using OnlineRechargeApplication.Models;

namespace OnlineRechargeApplication.Controllers
{
    public class SelectedPlanController : Controller
    {
        private readonly OnlineRechargeApplicationContext _context;

        public SelectedPlanController(OnlineRechargeApplicationContext context)
        {
            _context = context;
        }

        // GET: SelectedPlan
        public async Task<IActionResult> Index()
        {
              return _context.SelectedPlanModel != null ? 
                          View(await _context.SelectedPlanModel
                          .Include(x => x.plan)
                          .Include(x => x.customer)
                          .ToListAsync()) :
                          Problem("Entity set 'OnlineRechargeApplicationContext.SelectedPlanModel'  is null.");
        }

        // GET: SelectedPlan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SelectedPlanModel == null)
            {
                return NotFound();
            }

            var selectedPlanModel = await _context.SelectedPlanModel
                .FirstOrDefaultAsync(m => m.SelectedPlanId == id);
            if (selectedPlanModel == null)
            {
                return NotFound();
            }

            return View(selectedPlanModel);
        }

        // GET: SelectedPlan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SelectedPlan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SelectedPlanId")] SelectedPlanModel selectedPlanModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(selectedPlanModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(selectedPlanModel);
        }

        // GET: SelectedPlan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SelectedPlanModel == null)
            {
                return NotFound();
            }

            var selectedPlanModel = await _context.SelectedPlanModel.FindAsync(id);
            if (selectedPlanModel == null)
            {
                return NotFound();
            }
            return View(selectedPlanModel);
        }

        // POST: SelectedPlan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SelectedPlanId")] SelectedPlanModel selectedPlanModel)
        {
            if (id != selectedPlanModel.SelectedPlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(selectedPlanModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SelectedPlanModelExists(selectedPlanModel.SelectedPlanId))
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
            return View(selectedPlanModel);
        }

        // GET: SelectedPlan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SelectedPlanModel == null)
            {
                return NotFound();
            }

            var selectedPlanModel = await _context.SelectedPlanModel
                .FirstOrDefaultAsync(m => m.SelectedPlanId == id);
            if (selectedPlanModel == null)
            {
                return NotFound();
            }

            return View(selectedPlanModel);
        }

        // POST: SelectedPlan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SelectedPlanModel == null)
            {
                return Problem("Entity set 'OnlineRechargeApplicationContext.SelectedPlanModel'  is null.");
            }
            var selectedPlanModel = await _context.SelectedPlanModel.FindAsync(id);
            if (selectedPlanModel != null)
            {
                _context.SelectedPlanModel.Remove(selectedPlanModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SelectedPlanModelExists(int id)
        {
          return (_context.SelectedPlanModel?.Any(e => e.SelectedPlanId == id)).GetValueOrDefault();
        }
    }
}
