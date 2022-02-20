using System.Collections.Generic;
using ScheduleOptimization.Models;

namespace ScheduleOptimization.Types
{
    public class ReturnSchedule
    {
        private List<Lesson> _headToHeadLessons;
        private List<Lesson> _remoteLessons;

        public ReturnSchedule(List<Lesson> headToHeadLessons, List<Lesson> remoteLessons)
        {
            _headToHeadLessons = headToHeadLessons;
            _remoteLessons = remoteLessons;
        }
    }
}