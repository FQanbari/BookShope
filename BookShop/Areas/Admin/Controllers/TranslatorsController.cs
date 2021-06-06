using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Models;
using BookShop.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TranslatorsController : Controller
    {
        private readonly BookShopContext _context;

        public TranslatorsController(BookShopContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Translator.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TranslatorCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Translator translator = new Translator()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                _context.Translator.Add(translator);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var translator = await _context.Translator.FirstOrDefaultAsync(p => p.Id == id);
                if (translator == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(translator);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Translator model)
        {
            if (ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var translator = await _context.Translator.FirstOrDefaultAsync(p => p.Id == id);
                if (translator == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(translator);
                }
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deleted(int? id)
        {
            var translator = await _context.Translator.FindAsync(id);
            _context.Translator.Remove(translator);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}