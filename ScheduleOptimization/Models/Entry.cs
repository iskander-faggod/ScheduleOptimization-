using System;
using System.ComponentModel.DataAnnotations;
using ScheduleOptimization.Types;

namespace ScheduleOptimization.Models
{
    public class Entry
    {
        [Key]
        public Guid EntryId { get; set; }= Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid CardId { get; set; }
        public Status Status { get; set; }
    }
}