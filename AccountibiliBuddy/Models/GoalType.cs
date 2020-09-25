using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountibiliBuddy.Models
{
    public class GoalType
    {
        [Key]
        public int GoalTypeId { get; set; }
        [Required]
        public string Type { get; set; }

        public int PointValue { get; set; }
    }
}