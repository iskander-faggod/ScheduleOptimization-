using System;

namespace ScheduleOptimization.Data.DTO
{
    public class CreateGroupArguments
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public int CourseNumber { get; set; }
    }
}