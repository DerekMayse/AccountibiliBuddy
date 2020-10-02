using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountibiliBuddy.Models.ViewModels
{
    public class ProgressViewModel
    {
        public Goal Goal { get; set; }
        public List<Goal> Goals { get; set; } = new List<Goal>();

        public double ProgressCounter { get; set; }

        public string ProgressPercent { get; set; }

        public DateTime Date { get; set; }
    }
}
