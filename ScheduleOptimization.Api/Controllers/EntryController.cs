using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleOptimization.Data.DTO;
using ScheduleOptimization.Models;
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
        public async Task<ActionResult<Entry>> PostEntry([FromBody] CreateEntryArguments entryDto)
        {
            var entry = new Entry()
            {
                EntryId = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                CardId = entryDto.CardId,
                Status = entryDto.Status
            };
            await _entryService.CreateEntry(entry);
            return entry;
        }
        
        [HttpPost("in")]
        public async Task EntryIn([FromQuery] Guid personId, [FromQuery] Guid entryId)
        {
            await _entryService.In(personId, entryId);
        }
        
        [HttpPost("out")]
        public async Task EntryOut([FromQuery] Guid personId, [FromQuery] Guid entryId)
        {
            await _entryService.Out(personId, entryId);
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
