using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScheduleOptimization.Models
{
    public class Group
    {
        [Key]
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public int CourseNumber { get; set; }
        public List<Person> Students { get; set; } = new List<Person>();
    }
}