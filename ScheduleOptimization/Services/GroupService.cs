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
    public class GroupService : IGroupService
    {
        private readonly ScheduleContext _context;
        public GroupService(ScheduleContext context)
        {
            _context = context;
        }
        
        public async Task<List<Group>> GetAllGroups()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<ActionResult<Group>> GetGroupById(Guid groupId)
        {
            if (groupId == Guid.Empty)
                throw new ArgumentException($"{nameof(groupId)} can't be empty");
            var group = await _context.Groups.FindAsync(groupId);
            if (group is null)
                return new NotFoundResult();
            return group;
        }

        public async Task<ActionResult<Group>> CreateGroup(Group @group)
        {
            if (group is null)
                throw new ArgumentException($"{nameof(group)} can't be null");
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<ActionResult<Group>> DeleteGroup(Guid groupId)
        {
            if (groupId == Guid.Empty)
                throw new ArgumentException($"{nameof(groupId)} can't be empty");
            var group = await _context.Groups.FindAsync(groupId);
            if (group is null) 
                throw new ArgumentException($"{nameof(group)} can't be null");
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public bool GroupExists(Guid groupId)
        {
            if (groupId == Guid.Empty)
                throw new ArgumentException($"{nameof(groupId)} can't be empty");
            return _context.Groups.Any(e => e.GroupId == groupId);
        }
    }
}