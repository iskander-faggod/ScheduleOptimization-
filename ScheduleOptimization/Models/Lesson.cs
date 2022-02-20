using System;
using System.ComponentModel.DataAnnotations;

namespace ScheduleOptimization.Models
{
    public class Lesson
    {
        [Key] public Guid LessonId { get; set; }
        public DateTime StartLesson { get; set; }
        public DateTime EndLesson { get; set; }
        public Group Group { get; set; }
        public Person Coach { get; set; }
    }
}