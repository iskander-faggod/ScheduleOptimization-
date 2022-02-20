using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleOptimization.DTO;
using ScheduleOptimization.Services.Interfaces;
using ScheduleOptimization.Types;

namespace ScheduleOptimization.Controllers
{
    [Route("api/Schedule")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost]
        public async Task<ReturnSchedule> GetPersons([FromBody] GetOptimizeScheduleDto scheduleDto)
        {
            return await _scheduleService.OptimizeSchedule(
                scheduleDto.StartLessons,
                scheduleDto.EndLessons,
                scheduleDto.CountPair);
        }
    }
}