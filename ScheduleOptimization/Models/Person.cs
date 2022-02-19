using System;
using System.ComponentModel.DataAnnotations;
using ScheduleOptimization.Types;

namespace ScheduleOptimization.Models
{
    public class Person
    {
        [Key]
        public Guid PersonId { get; set; }
        public PersonType PersonType { get; set; }
    }
}