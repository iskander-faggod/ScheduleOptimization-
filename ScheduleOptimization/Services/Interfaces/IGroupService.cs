using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScheduleOptimization.Models;

namespace ScheduleOptimization.Services.Interfaces
{
    public interface IGroupService
    {
        Task<List<Group>> GetAllGroups();
        Task<ActionResult<Group>> GetGroupById(Guid groupId);
        Task<ActionResult<Group>> CreateGroup(Group group);
        Task<ActionResult<Group>> DeleteGroup(Guid groupId);
        Task AddStudentInGroup(Guid studentId, Guid groupId);
        bool GroupExists(Guid groupId);
    }
}