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
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        public bool CompletionStatus { get; set; }

        [Required]  
        
        [RegularExpression("([1-9][0-9]*)",
         ErrorMessage = "Please Choose A Goal Type")]

        public int GoalTypeId { get; set; }

        public GoalType GoalType { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }



    }
}