using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleOptimization.Models;

namespace ScheduleOptimization.Services.Interfaces
{
    public interface IPersonService
    {
        Task<List<Person>> GetAllPersons();
        Task<ActionResult<Person>> GetPersonById(Guid personId);
        Task<ActionResult<Person>> CreatePerson(Person person);
        Task<ActionResult<Person>> DeletePerson(Guid personId);
        bool PersonExists(Guid personId);
    }
}