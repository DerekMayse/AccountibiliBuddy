using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountibiliBuddy.Models
{
    public class Goal
    {
        [Key]
        public int GoalId { get; set; }

        [Required]
        [StringLength(55)]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }


        public bool CompletionStatus { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }



    }
}