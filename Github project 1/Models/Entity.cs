using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Github_project_1.Models
{
    public class Entity
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public bool IsCompleted { get; set; }
    }
}