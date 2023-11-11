using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syspotec.Core.entities;

namespace Syspotec.Core.Interfaces
{
    public interface IStateRepository
    {
        public Task<List<State>> Get();
        public Task<bool> Post(State state);
        public Task<bool> Delete(int state_id);
        public Task<bool> Update(int state_id, State state);
        public Task<State> GetByID(int state_id);
    }
}
