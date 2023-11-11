using Syspotec.Core.DTOs;
using Syspotec.Core.entities;
using Syspotec.Core.Interfaces;

namespace Syspotec.Core.Services
{
    public class AssignedService : IAssignedService
    {
        private readonly IAssignedRepository _assignedRepository;
        public AssignedService(IAssignedRepository assignedRepository)
        {
            _assignedRepository = assignedRepository;
        }

        public Task<List<Assigned>> Get()
        {
            return _assignedRepository.Get();
        }

        public Task<Assigned> GetByID(int assigned_id)
        {
            return _assignedRepository.GetByID(assigned_id);
        }

        public Task<bool> Post(AssignedDTO assigned, DateTime date)
        {
            return _assignedRepository.Post(assigned, date);
        }

        public Task<bool> Delete(int assigned_id)
        {
            return _assignedRepository.Delete(assigned_id);
        }

        public Task<bool> Update(int assigned_id, Assigned assigned, DateTime date)
        {
            return _assignedRepository.Update(assigned_id, assigned, date);
        }
    }
}
