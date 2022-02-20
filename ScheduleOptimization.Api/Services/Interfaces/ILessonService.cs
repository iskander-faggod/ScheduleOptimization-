using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleOptimization.Models;

namespace ScheduleOptimization.Services.Interfaces
{
    public interface ILessonService
    {
        Task<List<Lesson>> GetAllLessons();
        Task<ActionResult<Lesson>> GetLessonById(Guid lessonId);
        Task<ActionResult<Lesson>> CreateLesson(Lesson lesson);
        Task<ActionResult<Lesson>> DeleteLesson(Guid lessonId);
        Task<ActionResult<Lesson>> AddCoach(Guid lessonId, Person person);
        Task<ActionResult<Lesson>> AddGroupToLesson(Guid lessonId, Guid groupId);
        bool LessonExists(Guid lessonId);
    }
}