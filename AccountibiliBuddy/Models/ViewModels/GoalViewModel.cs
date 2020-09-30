using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountibiliBuddy.Models.ViewModels
{
    public class GoalViewModel
    {
        public Goal Goal { get; set; }

        public List<SelectListItem> GoalTypes { get; set; } = new List<SelectListItem>();
    }
}