using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Task
    {
        public int TaskID { get; set; }
        public Nullable<int> ID { get; set; }
        [Required(ErrorMessage = "Enter Title")]
        public string Title { get; set; }
        public string TaskDetail { get; set; }
        [Required(ErrorMessage = "Enter Start Date")]
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        [Required(ErrorMessage = "Enter End Date")]
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<int> Completed { get; set; }
        [Required(ErrorMessage = "Select Priority")]
        public Nullable<int> PriorityID { get; set; }

        public string Priority { get; set; }

    }
}