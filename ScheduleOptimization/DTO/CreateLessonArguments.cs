using System;

namespace ScheduleOptimization.DTO
{
    public class CreateLessonArguments
    {
        public Guid LessonId { get; set; }
        public DateTime StartLesson { get; set; }
        public DateTime EndLesson { get; set; }
    }
}