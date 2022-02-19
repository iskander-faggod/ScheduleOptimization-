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
        
        public bool EntryExists(Guid entryId)
        {
            if (entryId == Guid.Empty)
                throw new ArgumentException($"{nameof(entryId)} can't be empty");
            return _context.Entries.Any(e => e.EntryId == entryId);
        }
    }
}