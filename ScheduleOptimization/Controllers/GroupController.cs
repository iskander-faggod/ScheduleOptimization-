using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleOptimization.DTO;
using ScheduleOptimization.Models;
using ScheduleOptimization.Services.Interfaces;

namespace ScheduleOptimization.Controllers
{
    [Route("api/Group")]
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(Guid id)
        {
            return await _groupService.GetGroupById(id);
        }
        
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(CreateGroupArguments groupDto)
        {
            var group = new Group()
            {
                CourseNumber = groupDto.CourseNumber,
                GroupId = Guid.NewGuid(),
                GroupName = groupDto.GroupName,
                Persons = new List<Person>()
            };
            return await _groupService.CreateGroup(group);
        }

        [HttpPost("add-student")]
        public async Task AddStudent([FromQuery]Guid personId, [FromQuery] Guid groupId)
        {
            await _groupService.AddStudentInGroup(personId, groupId);
        }
        
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
