using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AccountibiliBuddy.Data;
using AccountibiliBuddy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AccountibiliBuddy.Models.ViewModels;

namespace AccountibiliBuddy.Controllers
{
    public class GoalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public GoalsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        [Authorize]
        // GET: Goals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Goal.Include(p => p.User);

            var user = await GetCurrentUserAsync();

            var userCheck = await _context.Goal.Where(g => g.UserId == user.Id && g.Date.Date == DateTime.Now.Date && g.Date.Month == DateTime.Now.Month && g.Date.Day == DateTime.Now.Day ).ToListAsync();

            if (userCheck.Count() < 1)
            {
                //redirect to a page that tells them 
                //and gives them an option to create a payment type
                return View("NoGoals");
            }

            return View(userCheck);
        }



        [Authorize]
        // GET: Goals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goal = await _context.Goal
                .FirstOrDefaultAsync(m => m.GoalId == id);
            if (goal == null)
            {
                return NotFound();
            }

            return View(goal);
        }
        [Authorize]
        // GET: Goals/Create
        public IActionResult Create()
        {
            //instance  of view model
            GoalViewModel ViewModel = new GoalViewModel();

            //accessing the view 
            ViewModel.GoalTypes = _context.GoalType.Select(gt => new SelectListItem

            {
                Text = gt.Type,
                Value = gt.GoalTypeId.ToString()
            }
        ).ToList();

            ViewModel.GoalTypes.Insert(0, new SelectListItem() { Value = "0", Text = "Choose A Goal Type" });

            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View(ViewModel);
        }

           
        
        // POST: Goals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( GoalViewModel viewModel)
        {
            //removes the user and userid so the model state doesnt think that the information is invalid
            ModelState.Remove("Goal.User");
            ModelState.Remove("Goal.UserId");
            if (ModelState.IsValid)
            {
                //gets the current user
                var user = await GetCurrentUserAsync();
                //grabs the current user's id
                viewModel.Goal.UserId = user.Id;
                //sets the completion status to false
                viewModel.Goal.CompletionStatus = false;
                _context.Add(viewModel.Goal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", goal.UserId);
            //ViewData["GoalTypeId"] = new SelectList(_context.GoalType, "Id", "Id", goal.GoalTypeId);
            return View(viewModel.Goal);
        }
        [Authorize]
        // GET: Goals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goal = await _context.Goal.FindAsync(id);
            if (goal == null)
            {
                return NotFound();
            }
            return View(goal);
        }

        // POST: Goals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GoalId,Name,Date,CompletionStatus,UserId")] Goal goal)
        {
            if (id != goal.GoalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(goal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoalExists(goal.GoalId))
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
            return View(goal);
        }


        // GET: Goals/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goal = await _context.Goal
                .FirstOrDefaultAsync(m => m.GoalId == id);
            if (goal == null)
            {
                return NotFound();
            }

            return View(goal);
        }

        // POST: Goals/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var goal = await _context.Goal.FindAsync(id);
            _context.Goal.Remove(goal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoalExists(int id)
        {
            return _context.Goal.Any(e => e.GoalId == id);
        }

       
        public async Task<IActionResult> Complete(int id, Goal goal)
        {
            var Complete = await _context.Goal.FindAsync(id);
            Complete.CompletionStatus = true;
            _context.Update(Complete);
            await _context.SaveChangesAsync();

            return Ok(Complete);

        }

   

    }
}
