using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScheduleOptimization.Context;
using ScheduleOptimization.Models;
using ScheduleOptimization.Services.Interfaces;

namespace ScheduleOptimization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            return await _groupService.GetAllGroups();
        }

        // GET: api/Group/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(Guid id)
        {
            return await _groupService.GetGroupById(id);
        }
        
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(Group @group)
        {
            return await _groupService.CreateGroup(group);
        }

        // DELETE: api/Group/5
        [HttpDelete("{id}")]
        public async Task DeleteGroup(Guid id)
        {
            await _groupService.DeleteGroup(id);
        }

        private bool GroupExists(Guid id)
        {
            return _groupService.GroupExists(id);
        }
    }
}
