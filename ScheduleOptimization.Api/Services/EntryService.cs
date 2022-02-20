using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScheduleOptimization.Data.Context;
using ScheduleOptimization.Models;
using ScheduleOptimization.Services.Interfaces;
using ScheduleOptimization.Types;


namespace ScheduleOptimization.Services
{
    public class EntryService : IEntryService
    {
        private readonly ScheduleContext _context;
        public EntryService(ScheduleContext context)
        {
            _context = context;
        }

        public async Task<List<Entry>> GetAllEntries()
        {
            return await _context.Entries.ToListAsync();
        }

        public async Task<ActionResult<Entry>> GetEntryById(Guid entryId)
        {
            if (entryId == Guid.Empty)
                throw new ArgumentException($"{nameof(entryId)} can't be empty");
            var entry = await _context.Entries.FindAsync(entryId);
            if (entry is null)
                return new NotFoundResult();
            return entry;
        }

        public async Task<ActionResult<Entry>> CreateEntry(Entry entry)
        {
            if (entry is null)
                throw new ArgumentException($"{nameof(entry)} can't be null");
            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();
            return new OkResult();
        }
        
        public async Task<ActionResult<Entry>> DeleteEntry(Guid entryId)
        {
            if (entryId == Guid.Empty)
                throw new ArgumentException($"{nameof(entryId)} can't be empty");
            var entry = await _context.Entries.FindAsync(entryId);
            if (entry is null) 
                throw new ArgumentException($"{nameof(entry)} can't be null");
            _context.Entries.Remove(entry);
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public async Task In(Guid personId, Guid entryId)
        {
            if (personId == Guid.Empty)
                throw new ArgumentException($"{nameof(personId)} can't be empty");
            if (entryId == Guid.Empty)
                throw new ArgumentException($"{nameof(entryId)} can't be empty");
            var person = await _context.Persons.FindAsync(personId);
            if (person is null)
                throw new ArgumentException($"{nameof(person)} can't be null");
            var entry = await _context.Entries.FindAsync(entryId);
            if (entry is null) 
                throw new ArgumentException($"{nameof(entry)} can't be null");
            person.Entry = entry;
            entry.Status = Status.Entered;
            await _context.SaveChangesAsync();
        }
        
        public async Task Out(Guid personId, Guid entryId)
        {
            if (personId == Guid.Empty)
                throw new ArgumentException($"{nameof(personId)} can't be empty");
            if (entryId == Guid.Empty)
                throw new ArgumentException($"{nameof(entryId)} can't be empty");
            var person = await _context.Persons.FindAsync(personId);
            if (person is null)
                throw new ArgumentException($"{nameof(person)} can't be null");
            var entry = await _context.Entries.FindAsync(entryId);
            if (entry is null) 
                throw new ArgumentException($"{nameof(entry)} can't be null");
            entry.Status = Status.Out;
            await _context.SaveChangesAsync();
        }

        public bool EntryExists(Guid entryId)
        {
            if (entryId == Guid.Empty)
                throw new ArgumentException($"{nameof(entryId)} can't be empty");
            return _context.Entries.Any(e => e.EntryId == entryId);
        }
    }
}