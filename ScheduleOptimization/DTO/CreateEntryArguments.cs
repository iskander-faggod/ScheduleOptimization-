using System;
using ScheduleOptimization.Types;

namespace ScheduleOptimization.DTO
{
    public class CreateEntryArguments
    {
        public Guid EntryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CardId { get; set; }
        public Status Status { get; set; }
    }
}