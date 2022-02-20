using System;
using ScheduleOptimization.Types;

namespace ScheduleOptimization.Data.DTO
{
    public class AddCoachDto
    {
        public Guid PersonId { get; set; }
        public PersonType PersonType { get; set; }
    }
}