using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ToDoList.Models
{
    public class TaskList
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ItemDescription { get; set; }
        public DateTime DueDate { get; set; }
        public bool Complete { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
