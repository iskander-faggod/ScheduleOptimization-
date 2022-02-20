using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScheduleOptimization.Context;
using ScheduleOptimization.Models;
using ScheduleOptimization.Services.Interfaces;
using ScheduleOptimization.Types;

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
        
        public async Task<ActionResult<Lesson>> AddCoach(Guid lessonId, Person person)
        {
            if (person.PersonType ==  PersonType.Bachelor)
                throw new ArgumentException($"{nameof(person)} can't be coach");
            var lesson = await _context.Lessons.FindAsync(lessonId);
            // Магистры и аспиранты не могут вести пары сами у себя.
            if (lesson.Group.Persons[0].PersonType == PersonType.Master && person.PersonType == PersonType.Master)
                throw new ArgumentException($"{nameof(person)} can't be coach");
            if (lesson.Group.Persons[0].PersonType == PersonType.Postgraduate && person.PersonType == PersonType.Postgraduate)
                throw new ArgumentException($"{nameof(person)} can't be coach");
            lesson.Coach = person;
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