using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleOptimization.Models;

namespace ScheduleOptimization.Services.Interfaces
{
    public interface IEntryService
    {
        Task<List<Entry>> GetAllEntries();
        Task<ActionResult<Entry>> GetEntryById(Guid entryId);
        Task<ActionResult<Entry>> CreateEntry(Entry entry);
        Task<ActionResult<Entry>> DeleteEntry(Guid entryId);
        Task In(Guid personId, Guid entryId);
        Task Out(Guid personId, Guid entryId);
        bool EntryExists(Guid entryId); 
    }
}