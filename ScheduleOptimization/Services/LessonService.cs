using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScheduleOptimization.Context;
using ScheduleOptimization.Models;
using ScheduleOptimization.Services.Interfaces;

namespace ScheduleOptimization.Services
{
    public class LessonService : ILessonService
    {
        private readonly ScheduleContext _context;
        public LessonService(ScheduleContext context)
        {
            _context = context;
        }
        
        public async Task<List<Lesson>> GetAllLessons()
        {
            return await _context.Lessons.ToListAsync();
        }

        public async Task<ActionResult<Lesson>> GetLessonById(Guid lessonId)
        {
            if (lessonId == Guid.Empty)
                throw new ArgumentException($"{nameof(lessonId)} can't be empty");
            var lesson = await _context.Lessons.FindAsync(lessonId);
            if (lesson is null)
                return new NotFoundResult();
            return lesson;
        }
        
        public async Task<ActionResult<Lesson>> CreateLesson(Lesson @lesson)
        {
            if (lesson is null)
                throw new ArgumentException($"{nameof(Lesson)} can't be null");
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<ActionResult<Lesson>> DeleteLesson(Guid lessonId)
        {
            if (lessonId == Guid.Empty)
                throw new ArgumentException($"{nameof(lessonId)} can't be empty");
            var lesson = await _context.Lessons.FindAsync(lessonId);
            if (lesson is null) 
                throw new ArgumentException($"{nameof(lesson)} can't be null");
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public bool LessonExists(Guid lessonId)
        {
            if (lessonId == Guid.Empty)
                throw new ArgumentException($"{nameof(lessonId)} can't be empty");
            return _context.Lessons.Any(e => e.LessonId == lessonId);
        }
    }
}