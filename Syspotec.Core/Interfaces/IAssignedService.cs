using Syspotec.Core.DTOs;
using Syspotec.Core.entities;

namespace Syspotec.Core.Interfaces
{
    public interface IAssignedService
    {
        public Task<List<Assigned>> Get();
        public Task<bool> Post(AssignedDTO assigned, DateTime date);
        public Task<bool> Delete(int assigned_id);
        public Task<bool> Update(int assigned_id, Assigned assigned, DateTime date);
        public Task<List<Assigned>> GetByNIT(int assigned_nit);
        public Task<Assigned> GetByID(int assigned_id);
    }
}
