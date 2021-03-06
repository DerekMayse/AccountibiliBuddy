﻿using System;
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
            ProgressViewModel ViewModel = new ProgressViewModel();

            var applicationDbContext = _context.Goal.Include(p => p.User);

            var user = await GetCurrentUserAsync();

            var userCheck = await _context.Goal.Where(g => g.UserId == user.Id && g.Date.Date == DateTime.Now.Date && g.Date.Month == DateTime.Now.Month && g.Date.Day == DateTime.Now.Day ).ToListAsync();

            if (userCheck.Count() < 1)
            {
                //redirect to a page that tells them 
                //and gives them an option to create a payment type
                return View("NoGoals");
            }


            var AllGoals = _context.Goal.Include(p => p.User).ToList();

            var Currentuser = await GetCurrentUserAsync();

            var GoalsForCurrentDay = await _context.Goal.Where(g => g.UserId == Currentuser.Id && g.Date.Date == DateTime.Now.Date && g.Date.Month == DateTime.Now.Month && g.Date.Day == DateTime.Now.Day).ToListAsync();

            double TotalNumberOfGoals = GoalsForCurrentDay.Count();

            double NumberOfCompletedGoals = GoalsForCurrentDay.Where(g => g.CompletionStatus == true).Count();

            double CounterforProgressBar = (NumberOfCompletedGoals / TotalNumberOfGoals) * 100;

            ViewModel.Goals = userCheck;

            ViewModel.DailyProgressCounter = CounterforProgressBar;
            ViewModel.DailyProgressPercent = $"{Math.Round(CounterforProgressBar)}%";

            return View(ViewModel);
        }



        [Authorize]
        // GET: Goals/Details/5
        public async Task<IActionResult> Details(int? id)
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
            GoalViewModel ViewModel = new GoalViewModel();

            ViewModel.Goal = await _context.Goal
            .FirstOrDefaultAsync(m => m.GoalId == id);

            ViewModel.GoalTypes = _context.GoalType.Select(gt => new SelectListItem

            {
                Text = gt.Type,
                Value = gt.GoalTypeId.ToString()
            }
    ).ToList();

            return View(ViewModel);
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

            ViewModel.GoalTypes.Insert(0, new SelectListItem() { Value = "-1", Text = "Choose A Goal Type" });

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
            if (ModelState.IsValid )
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
       
            return View(viewModel);
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
            GoalViewModel ViewModel = new GoalViewModel();

            ViewModel.Goal = await _context.Goal
            .FirstOrDefaultAsync(m => m.GoalId == id);
            


            ViewModel.GoalTypes = _context.GoalType.Select(gt => new SelectListItem

            {
                Text = gt.Type,
                Value = gt.GoalTypeId.ToString()
            }
    ).ToList();

            return View(ViewModel);
        }

        // POST: Goals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  GoalViewModel viewModel)
        {
            if (id != viewModel.Goal.GoalId)
            {
                return NotFound();
            }

            ModelState.Remove("Goal.User");
            ModelState.Remove("Goal.UserId");
            
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await GetCurrentUserAsync();
                    //grabs the current user's id
                    viewModel.Goal.UserId = user.Id;
                    viewModel.Goal.CompletionStatus = viewModel.Goal.CompletionStatus;



                    _context.Update(viewModel.Goal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoalExists(viewModel.Goal.GoalId))
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
            ViewData["GoalTypeId"] = new SelectList(_context.GoalType, "Id", "Id", viewModel.Goal.GoalTypeId);
            return View(viewModel);
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



        public async Task<IActionResult> ChooseDate(DateTime DateSearch)
        {
            ProgressViewModel ViewModel = new ProgressViewModel();

            ViewData["UserDateInput"] = DateSearch;

            var applicationDbContext = _context.Goal.Include(p => p.User);

            var user = await GetCurrentUserAsync();

            var searchDate = applicationDbContext.Where(g => g.UserId == user.Id && g.Date.Day == DateSearch.Day && g.Date.Month == DateSearch.Month && g.Date.Year == DateSearch.Year ).ToList();

            double TotalNumberOfGoals = searchDate.Count();

            double NumberOfCompletedGoals = searchDate.Where(g => g.CompletionStatus == true).Count();

            double CounterforProgressBar = (NumberOfCompletedGoals / TotalNumberOfGoals) * 100;

            ViewModel.Goals = searchDate;

            ViewModel.DailyProgressCounter = CounterforProgressBar;
            ViewModel.DailyProgressPercent = $"{Math.Round(CounterforProgressBar)}%";
            ViewModel.Date = DateSearch;

            return View(ViewModel);

        }

        public async Task<IActionResult> Statistics(Goal Goal, GoalType GoalType)
        {
            ProgressViewModel ViewModel = new ProgressViewModel();

            var AllGoalsWithUsers = _context.Goal.Include(p => p.User)
                .Include(g => g.GoalType)
                .ToList();
                

            var Currentuser = await GetCurrentUserAsync();

            var date = DateTime.Now.Date;

            var Weeklyresult = Enumerable.Range(1, 7)
              .Select(day => date.Date.AddDays(-day));

            var Monthlyresult = Enumerable.Range(1, 30)
              .Select(day => date.Date.AddDays(-day));


            var GoalsForCurrentWeek = await _context.Goal.Include(g => g.GoalType).Where(g => g.UserId == Currentuser.Id && Weeklyresult.Contains(g.Date))     
                .ToListAsync();

            var GoalsForCurrentMonth = await _context.Goal.Include(g => g.GoalType).Where(g => g.UserId == Currentuser.Id && Monthlyresult.Contains(g.Date))
                .ToListAsync();

            ViewModel.WeeklyGoals = GoalsForCurrentWeek;

            ViewModel.MonthlyGoals = GoalsForCurrentMonth;

            double TotalNumberOfWeeklyGoals = GoalsForCurrentWeek.Count();

            double NumberOfCompletedGoalsForWeek = GoalsForCurrentWeek.Where(g => g.CompletionStatus == true).Count();

            double CounterforWeeklyProgressBar = (NumberOfCompletedGoalsForWeek / TotalNumberOfWeeklyGoals) * 100;

            double TotalNumberOfMonthlyGoals = GoalsForCurrentMonth.Count();

            double NumberOfCompletedGoalsForMonth = GoalsForCurrentMonth.Where(g => g.CompletionStatus == true).Count();

            double CounterforMonthlyProgressBar = (NumberOfCompletedGoalsForMonth / TotalNumberOfMonthlyGoals) * 100;



            var AllGoals = _context.Goal.Include(p => p.User ).Include(g => g.GoalType).ToList();

            var GoalsForCurrentDay = await _context.Goal.Include(g => g.GoalType).Where(g => g.UserId == Currentuser.Id && g.Date.Date == DateTime.Now.Date && g.Date.Month == DateTime.Now.Month && g.Date.Day == DateTime.Now.Day).ToListAsync();

            int TotalNumberOfGoals = GoalsForCurrentDay.Count();

            int NumberOfCompletedGoals = GoalsForCurrentDay.Where(g => g.CompletionStatus == true).Count();

            int DailyPointCounterStartingPoint = 0;

            int WeeklyPointCounterStartingPoint = 0;

            

             foreach (Goal goal in GoalsForCurrentDay)
            {
                if(goal.CompletionStatus == true)
                {
                    DailyPointCounterStartingPoint += goal.GoalType.PointValue;
                }
            };

            foreach (Goal goal in GoalsForCurrentWeek)
            {
                if (goal.CompletionStatus == true)
                {
                    WeeklyPointCounterStartingPoint += goal.GoalType.PointValue;
                }
            };


            var WeeklyPointAverage = WeeklyPointCounterStartingPoint / 7;

            ViewModel.AveragePoints = WeeklyPointAverage;

            ViewModel.NumberOfGoalsForWeek = TotalNumberOfWeeklyGoals;
            ViewModel.NumberOfCompletedGoalsForWeek = NumberOfCompletedGoalsForWeek;

            ViewModel.NumberOfGoalsForMonth = TotalNumberOfMonthlyGoals;
            ViewModel.NumberOfCompletedGoalsForMonth = NumberOfCompletedGoalsForMonth;

            ViewModel.WeeklyProgressCounter = CounterforWeeklyProgressBar;
            ViewModel.WeeklyProgressPercent = $"{Math.Round(CounterforWeeklyProgressBar)}%";


            ViewModel.MonthlyProgressCounter = CounterforMonthlyProgressBar;
            ViewModel.MonthlyProgressPercent = $"{Math.Round(CounterforMonthlyProgressBar)}%";


            ViewModel.PointValueForCurrentDay = DailyPointCounterStartingPoint;

            return View(ViewModel);

        }

  


    }
}
