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
    public class PlanController : Controller
    {
        private readonly OnlineRechargeApplicationContext _context;

        public PlanController(OnlineRechargeApplicationContext context)
        {
            _context = context;
        }

        // GET: Plan
        public async Task<IActionResult> Index()
        {
              return _context.PlanModel != null ? 
                          View(await _context.PlanModel
                          .Include(x => x.ServiceProvider)
                          .ToListAsync()) :
                          Problem("Entity set 'OnlineRechargeApplicationContext.PlanModel'  is null.");
        }

        // GET: Plan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlanModel == null)
            {
                return NotFound();
            }

            var planModel = await _context.PlanModel.Include(x => x.ServiceProvider)
                .FirstOrDefaultAsync(m => m.PlanId == id);
            if (planModel == null)
            {
                return NotFound();
            }

            return View(planModel);
        }

        // GET: Plan/Create
        public async Task<IActionResult> Create()
        {
            List<ServiceProviderModel> service = await _context.ServiceProviderModel.ToListAsync();
            ViewBag.services = service;
            return View();
        }

        // POST: Plan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            PlanModel plan = new PlanModel();
            plan.PlanName = ""+form["planname"];
            plan.PlanPrice = Convert.ToInt32(form["price"]);
            plan.PlanValidity = Convert.ToInt32(form["validity"]);
            plan.PlanDescription = ""+(form["description"]);

            int id = Convert.ToInt32(form["service"]);

            var service = await _context.ServiceProviderModel
                .FirstOrDefaultAsync(m => m.ServiceProviderId == id);
            plan.ServiceProvider = service;
            if (ModelState.IsValid)
            {
                _context.Add(plan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        // GET: Plan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlanModel == null)
            {
                return NotFound();
            }

            var planModel = await _context.PlanModel.FindAsync(id);
            if (planModel == null)
            {
                return NotFound();
            }
            return View(planModel);
        }

        // POST: Plan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("PlanId,PlanName,PlanPrice,PlanValidity,PlanDescription")] PlanModel planModel)
        {
            int id = planModel.PlanId;
            if (id != planModel.PlanId)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(planModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanModelExists(planModel.PlanId))
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

        // GET: Plan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlanModel == null)
            {
                return NotFound();
            }

            var planModel = await _context.PlanModel
                .FirstOrDefaultAsync(m => m.PlanId == id);
            if (planModel == null)
            {
                return NotFound();
            }

            return View(planModel);
        }

        // POST: Plan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("PlanId")] PlanModel planModels)
        {
            int id = planModels.PlanId;
            if (_context.PlanModel == null)
            {
                return Problem("Entity set 'OnlineRechargeApplicationContext.PlanModel'  is null.");
            }
            var planModel = await _context.PlanModel.FindAsync(id);
            if (planModel != null)
            {
                _context.PlanModel.Remove(planModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanModelExists(int id)
        {
          return (_context.PlanModel?.Any(e => e.PlanId == id)).GetValueOrDefault();
        }
    }
}
