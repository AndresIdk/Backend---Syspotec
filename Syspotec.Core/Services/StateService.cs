using Syspotec.Core.entities;
using Syspotec.Core.Interfaces;

namespace Syspotec.Core.Services
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepository;
        public StateService(IStateRepository stateRepository) 
        {
            _stateRepository = stateRepository;
        }
        public Task<List<State>> Get()
        {
            return _stateRepository.Get();
        }

        public Task<State> GetByID(int state_id)
        {
            return _stateRepository.GetByID(state_id);
        }

        public Task<bool> Post(State state)
        {
            return _stateRepository.Post(state);
        }
        public Task<bool> Delete(int state_id)
        {
            return _stateRepository.Delete(state_id);
        }
        public Task<bool> Update(int state_id, State state)
        {
            return _stateRepository.Update(state_id, state);
        }
    }
}
