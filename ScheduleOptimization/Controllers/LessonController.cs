using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScheduleOptimization.Context;
using ScheduleOptimization.DTO;
using ScheduleOptimization.Models;
using ScheduleOptimization.Services.Interfaces;

namespace ScheduleOptimization.Controllers
{
    [Route("api/Lesson")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons()
        {
            return await _lessonService.GetAllLessons();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetLesson(Guid id)
        {
            return await _lessonService.GetLessonById(id);
        }

        [HttpPost]
        public async Task<ActionResult<Lesson>> PostLesson(CreateLessonArguments lessonDto)
        {
            var lesson = new Lesson()
            {
                EndLesson = lessonDto.EndLesson,
                Group = null,
                LessonId = Guid.NewGuid(),
                StartLesson = lessonDto.StartLesson,
            };
            if (lesson.StartLesson > lesson.EndLesson)
                throw new ArgumentException(
                    $"Start of lesson - {lesson.StartLesson}" +
                    $"can't be more then end of lesson - {lesson.EndLesson} ");
            return await _lessonService.CreateLesson(lesson);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Lesson>> DeleteLesson(Guid id)
        {
            return await _lessonService.DeleteLesson(id);
        }

        private bool LessonExists(Guid id)
        {
            return _lessonService.LessonExists(id);
        }
    }
}