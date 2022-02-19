using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScheduleOptimization.Context;
using ScheduleOptimization.Models;
using ScheduleOptimization.Services;
using ScheduleOptimization.Services.Interfaces;

namespace ScheduleOptimization.Controllers
{
    [Route("api/Entry")]
    [ApiController]
    public class EntryController : ControllerBase
    {
        private readonly IEntryService _entryService;

        public EntryController(IEntryService entryService)
        {
            _entryService = entryService;
        }

        [HttpGet]
        public async Task<List<Entry>> GetEntries()
        {
            return await _entryService.GetAllEntries();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Entry>> GetEntry(Guid id)
        {
           return await _entryService.GetEntryById(id);
        }

        
        [HttpPost]
        public async Task<ActionResult<Entry>> PostEntry(Entry entry)
        {
            await _entryService.CreateEntry(entry);
            return entry;
        }

        [HttpDelete("{id}")]
        public async Task DeleteEntry(Guid id)
        {
            await _entryService.DeleteEntry(id);
        }

        private bool EntryExists(Guid id)
        {
            return _entryService.EntryExists(id);
        }
    }
}
