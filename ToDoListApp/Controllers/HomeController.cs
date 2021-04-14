using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Data;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly ToDoListDbContext _context;

        public HomeController(ToDoListDbContext context)
        {
            _context = context;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        
        public async Task<IActionResult> IndexAsync()
        {   
            
            return View(await _context.ToDoItems.ToListAsync());
        }
        // GET: ToDoItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItems
                .FirstOrDefaultAsync(m => m.ToDoItemID == id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }
        // GET: ToDoItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ToDoItemID,UserEmail,Title,Description,AddedDate,DueDate,Done,DoneDate")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoItem);
                await _context.SaveChangesAsync();
                toDoItem.UserEmail = User.Identity.Name;
                toDoItem.AddedDate = DateTime.Now.ToString();
                toDoItem.DueDate = DateTime.Now.AddDays(1).ToString();
                toDoItem.DoneDate = "Unknown";
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDoItem);
        }
        // GET: ToDoItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }
            return View(toDoItem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ToDoItemID,UserEmail,Title,Description,AddedDate,DueDate,Done,DoneDate")] ToDoItem toDoItem)
        {
            if (id != toDoItem.ToDoItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoItem);
                    await _context.SaveChangesAsync();
                    toDoItem.UserEmail = User.Identity.Name;
                    await _context.SaveChangesAsync();
                    if (toDoItem.Done)
                    {
                        toDoItem.DoneDate = DateTime.Now.ToString();
                        await _context.SaveChangesAsync();
                    }
                    else {
                        toDoItem.DoneDate = "Unknown";
                        await _context.SaveChangesAsync();
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoItemExists(toDoItem.ToDoItemID))
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
            return View(toDoItem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Done([Bind("ToDoItemID")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    toDoItem = await _context.ToDoItems.FirstOrDefaultAsync(m => m.ToDoItemID == toDoItem.ToDoItemID);
                    _context.Update(toDoItem);
                    toDoItem.UserEmail = User.Identity.Name;
                    toDoItem.DoneDate = DateTime.Now.ToString();
                    toDoItem.Done = true;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoItemExists(toDoItem.ToDoItemID))
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
            return RedirectToAction(nameof(Index));
        }

        // GET: ToDoItems/Delete/5
        public async Task<IActionResult> DeleteToDo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItems
                .FirstOrDefaultAsync(m => m.ToDoItemID == id);
            if (toDoItem == null)
            {
                return NotFound();
            }
            return View(toDoItem);
        }
        //DeleteTodo
        [HttpPost, ActionName("DeleteToDo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("ToDoItemID")] ToDoItem toDoItem)
        {

            toDoItem = await _context.ToDoItems.FirstOrDefaultAsync(m => m.ToDoItemID == toDoItem.ToDoItemID);
            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoItemExists(int id)
        {
            return _context.ToDoItems.Any(e => e.ToDoItemID == id);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}



// GET: ToDoItems/Delete/5
//public async Task<IActionResult> Delete(int? id)
//{
//    if (id == null)
//    {
//        return NotFound();
//    }

//    var toDoItem = await _context.ToDoItems
//        .FirstOrDefaultAsync(m => m.ToDoItemID == id);
//    if (toDoItem == null)
//    {
//        return NotFound();
//    }
//    return View(toDoItem);
//}
// POST: ToDoItems/Delete/5
//[HttpPost, ActionName("Delete")]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> DeleteConfirmed(int id)
//{
//    var toDoItem = await _context.ToDoItems.FindAsync(id);
//    _context.ToDoItems.Remove(toDoItem);
//    await _context.SaveChangesAsync();
//    return RedirectToAction(nameof(Index));
//}