using System;

namespace ScheduleOptimization.Data.DTO
{
    public class GetOptimizeScheduleDto
    {
        public DateTime StartLessons { get; set; }
        public DateTime EndLessons { get; set; }
        public int CountPair { get; set; }
    }
}