using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AEP_Task_Tracker.Models
{
    public class Chore
    {

        [Key]
        public int Id { get; set; }

        [Required]

        public string Name { get; set; }

        [Required]

        public string Description { get; set; }

        [Required]

        public Status Status { get; set; }

        [Required]

        public DateTime CompletedOn { get; set; }
    }

    public enum Status
    {
        ToDo,
        InProgress,
        Complete
    }
}
