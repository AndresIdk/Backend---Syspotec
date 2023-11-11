using Syspotec.Core.entities;

namespace Syspotec.Core.Interfaces
{
    public interface IStateService
    {
        public Task<List<State>> Get();
        public Task<bool> Post(State state);
        public Task<bool> Delete(int state_id);
        public Task<bool> Update(int state_id, State state);
        public Task<State> GetByID(int state_id);
    }
}
