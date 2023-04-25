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
    public class ServiceProviderController : Controller
    {
        private readonly OnlineRechargeApplicationContext _context;

        public ServiceProviderController(OnlineRechargeApplicationContext context)
        {
            _context = context;
        }

        // GET: ServiceProvider
        public async Task<IActionResult> Index()
        {
              return _context.ServiceProviderModel != null ? 
                          View(await _context.ServiceProviderModel.ToListAsync()) :
                          Problem("Entity set 'OnlineRechargeApplicationContext.ServiceProviderModel'  is null.");
        }

        // GET: ServiceProvider/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ServiceProviderModel == null)
            {
                return NotFound();
            }

            var serviceProviderModel = await _context.ServiceProviderModel
                .FirstOrDefaultAsync(m => m.ServiceProviderId == id);
            if (serviceProviderModel == null)
            {
                return NotFound();
            }

            return View(serviceProviderModel);
        }

        // GET: ServiceProvider/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServiceProvider/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceProviderId,ServiceProviderName")] ServiceProviderModel serviceProviderModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceProviderModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceProviderModel);
        }

        // GET: ServiceProvider/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ServiceProviderModel == null)
            {
                return NotFound();
            }

            var serviceProviderModel = await _context.ServiceProviderModel.FindAsync(id);
            if (serviceProviderModel == null)
            {
                return NotFound();
            }
            return View(serviceProviderModel);
        }

        // POST: ServiceProvider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceProviderId,ServiceProviderName")] ServiceProviderModel serviceProviderModel)
        {
            if (id != serviceProviderModel.ServiceProviderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceProviderModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceProviderModelExists(serviceProviderModel.ServiceProviderId))
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
            return View(serviceProviderModel);
        }

        // GET: ServiceProvider/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ServiceProviderModel == null)
            {
                return NotFound();
            }

            var serviceProviderModel = await _context.ServiceProviderModel
                .FirstOrDefaultAsync(m => m.ServiceProviderId == id);
            if (serviceProviderModel == null)
            {
                return NotFound();
            }

            return View(serviceProviderModel);
        }

        // POST: ServiceProvider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ServiceProviderModel == null)
            {
                return Problem("Entity set 'OnlineRechargeApplicationContext.ServiceProviderModel'  is null.");
            }
            var serviceProviderModel = await _context.ServiceProviderModel.FindAsync(id);
            if (serviceProviderModel != null)
            {
                _context.ServiceProviderModel.Remove(serviceProviderModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceProviderModelExists(int id)
        {
          return (_context.ServiceProviderModel?.Any(e => e.ServiceProviderId == id)).GetValueOrDefault();
        }
    }
}
