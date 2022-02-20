using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScheduleOptimization.Data.DTO;
using ScheduleOptimization.Models;
using ScheduleOptimization.Services.Interfaces;

namespace ScheduleOptimization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return await _personService.GetAllPersons();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            return await _personService.GetPersonById(id);
        }
        
        
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(CreatePersonArguments personDto)
        {
            var person = new Person()
            {
                Entry = null,
                PersonId = Guid.NewGuid(),
                PersonType = personDto.PersonType
            };
            return await _personService.CreatePerson(person);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(Guid id)
        {
            return await _personService.DeletePerson(id);
        }

        private bool PersonExists(Guid id)
        {
            return _personService.PersonExists(id);
        }
    }
}
