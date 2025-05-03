using ASP.NET_MVC.Data;
using ASP.NET_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_MVC.Controllers;

public class ItemsController : Controller
{
   private readonly MyAppContext _context;
   public ItemsController(MyAppContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var items = await _context.Items.Include(s => s.SerialNumber)
            .Include(c=>c.Category)
            .Include(ic => ic.ItemClients)
            .ThenInclude(ic => ic.Client)
            .ToListAsync();
        return View(items);
    }

    public IActionResult Create()
    {
        ViewData["Categories"]= new SelectList(_context.Categories, "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Id, Name, Price, CategoryId")] Item item)
    {
        if (ModelState.IsValid)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(item);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        ViewData["Categories"]= new SelectList(_context.Categories, "Id", "Name");

        var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
        return View(item);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Price, CategoryId")] Item item)
    {
        if (ModelState.IsValid)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
      
        return View(item);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item != null)
        {
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index)); 

    }
    
}