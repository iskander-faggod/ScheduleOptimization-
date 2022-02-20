using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScheduleOptimization.Context;
using ScheduleOptimization.Models;
using ScheduleOptimization.Services.Interfaces;
using ScheduleOptimization.Types;

namespace ScheduleOptimization.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ScheduleContext _context;

        public ScheduleService(ScheduleContext context)
        {
            _context = context;
        }

        public async Task<ReturnSchedule> OptimizeSchedule(DateTime startLessons, DateTime endLessons, int countPair)
        {
            var allLessons = _context.Lessons;
            var headToHeadLessons = new List<Lesson>();
            var remoteLessons = new List<Lesson>();
            foreach (var lesson in allLessons)
            {
                if (PersonsPercent(lesson.Group) <= 10)
                {
                    headToHeadLessons.Add(lesson);
                }
                else
                {
                    remoteLessons.Add(lesson);
                }
            }

            foreach (var headToHeadLesson in headToHeadLessons.GetRange(1, countPair))
            {
                ChangeLessonsStartAndEndTime(headToHeadLesson, startLessons, endLessons);
            }

            foreach (var remoteLesson in remoteLessons.GetRange(1, countPair))
            {
                ChangeLessonsStartAndEndTime(remoteLesson, startLessons, endLessons);
            }

            return new ReturnSchedule(headToHeadLessons, remoteLessons);

        }

        public void ChangeLessonsStartAndEndTime(Lesson lesson, DateTime startLessons, DateTime endLessons)
        {
            var newStart = lesson.StartLesson.AddMinutes(20);
            if (newStart < startLessons)
                throw new ArgumentException($"Lesson start time must be more then start of lessons time");
            lesson.StartLesson = newStart;
            var newEnd = newStart.AddHours(1.5);
            if (newEnd > endLessons)
                throw new ArgumentException($"End lesson time must be more then start of lessons time");
            lesson.EndLesson = newEnd;
        }
        
        public int PersonsPercent(Group @group)
        {
            // Считаем сколько людей всего в стенах университета 
            var countOfPersons = _context.Entries
                .Where(x => x.Status == Status.Entered)
                .ToArray()
                .Length;
            int percentOfPeople = 0;
            foreach (var person in group.Persons)
            {
                if (@group.CourseNumber is 1 or 2 or 3 or 4)
                {
                    // В группе бакалаврорв не должно быть аспирантов, магистрантов и лекторов
                    var strangers = group.Persons.Where(x =>
                            x.PersonType is PersonType.Master or PersonType.Postgraduate or PersonType.Lectures)
                        .ToList()
                        .Count;
                    if (strangers > 0) throw new ArgumentException("In group can't be a mentors");
                    // Возвращаю сколько процентов группа занимает от общего кол-во человек в Университете 
                    // + 1 т.к на Lesson еще будет ментор
                    percentOfPeople = (group.Persons.Count + 1) % countOfPersons;
                }

                else if (group.CourseNumber is 5 or 6)
                {
                    // В группе магистрантов не должно быть аспирантов, бакалавров и лекторов
                    var strangers = group.Persons.Where(x =>
                            x.PersonType is PersonType.Postgraduate or PersonType.Lectures or PersonType.Bachelor)
                        .ToList()
                        .Count;
                    if (strangers > 0) throw new ArgumentException("In group can't be a strangers");
                    percentOfPeople = (group.Persons.Count + 1) % countOfPersons;
                }

                else if (group.CourseNumber is 7 or 8)
                {
                    // В группе аспирантов не должно быть магистрантов, бакалавров и лекторов
                    var strangers = group.Persons.Where(x =>
                            x.PersonType is PersonType.Master or PersonType.Lectures or PersonType.Bachelor)
                        .ToList()
                        .Count;
                    if (strangers > 0) throw new ArgumentException("In group can't be a strangers");
                    percentOfPeople = (group.Persons.Count + 1) % countOfPersons;
                }
            }

            return percentOfPeople;
        }
    }
}