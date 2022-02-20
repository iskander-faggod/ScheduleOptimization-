using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScheduleOptimization.Context;
using ScheduleOptimization.Models;
using ScheduleOptimization.Services.Interfaces;

namespace ScheduleOptimization.Services
{
    public class PersonService : IPersonService
    {
        private readonly ScheduleContext _context;
        public PersonService(ScheduleContext context)
        {
            _context = context;
        }
        
        public async Task<List<Person>> GetAllPersons()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<ActionResult<Person>> GetPersonById(Guid personId)
        {
            if (personId == Guid.Empty)
                throw new ArgumentException($"{nameof(personId)} can't be empty");
            var person = await _context.Persons.FindAsync(personId);
            if (person is null)
                return new NotFoundResult();
            return person;
        }

        public async Task<ActionResult<Person>> CreatePerson(Person person)
        {
            if (person is null)
                throw new ArgumentException($"{nameof(person)} can't be null");
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<ActionResult<Person>> DeletePerson(Guid personId)
        {
            if (personId == Guid.Empty)
                throw new ArgumentException($"{nameof(personId)} can't be empty");
            var person = await _context.Persons.FindAsync(personId);
            if (person is null) 
                throw new ArgumentException($"{nameof(person)} can't be null");
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public bool PersonExists(Guid personId)
        {
            if (personId == Guid.Empty)
                throw new ArgumentException($"{nameof(personId)} can't be empty");
            return _context.Persons.Any(e => e.PersonId == personId);
        }
    }
}