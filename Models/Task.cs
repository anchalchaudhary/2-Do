using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public class Task
    {
        public int TaskID { get; set; }
        public Nullable<int> ID { get; set; }
        public string Title { get; set; }
        public string TaskDetail { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<int> Completed { get; set; }
    }
}