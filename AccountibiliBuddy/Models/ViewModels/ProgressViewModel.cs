using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountibiliBuddy.Models.ViewModels
{
    public class ProgressViewModel
    {
        public Goal Goal { get; set; }

       

        public List<Goal> Goals { get; set; } = new List<Goal>();

        public List<Goal> WeeklyGoals { get; set; } = new List<Goal>();

        public List<Goal> MonthlyGoals { get; set; } = new List<Goal>();


        public double DailyProgressCounter { get; set; }

        public string DailyProgressPercent { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public double WeeklyProgressCounter { get; set; }

        public string WeeklyProgressPercent { get; set; }

        public double MonthlyProgressCounter { get; set; }

        public string MonthlyProgressPercent { get; set; }

        public int PointValueForCurrentDay { get; set; }

        public double AveragePoints { get; set; }

        public double NumberOfGoalsForWeek { get; set; }

        public double NumberOfCompletedGoalsForWeek { get; set;  }

        public double NumberOfGoalsForMonth { get; set; }

        public double NumberOfCompletedGoalsForMonth { get; set; }
    }
}
