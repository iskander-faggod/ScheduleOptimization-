using System;
using System.Threading.Tasks;
using ScheduleOptimization.Models;
using ScheduleOptimization.Types;

namespace ScheduleOptimization.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<ReturnSchedule> OptimizeSchedule(DateTime startLessons, DateTime endLessons, int countPair);

        Task<int> PersonsPercent(Group @group);

        void ChangeLessonsStartAndEndTime(Lesson lesson, DateTime startLessons, DateTime endLessons);
    }
}