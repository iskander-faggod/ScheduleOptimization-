using System;
using System.Collections.Generic;
using ScheduleOptimization.Models;

namespace ScheduleOptimization.DTO
{
    public class CreateGroupArguments
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public int CourseNumber { get; set; }
    }
}