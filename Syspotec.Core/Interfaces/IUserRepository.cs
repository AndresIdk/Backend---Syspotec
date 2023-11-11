using Syspotec.Core.DTOs;
using Syspotec.Core.entities;

namespace Syspotec.Core.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<UserDTO>> Get();
        public Task<bool> Post(User user);
        public Task<bool> Delete(int nit);
        public Task<bool> Update(int nit, User user);
        public Task<User> GetByID(int nit);
        public Task<string> GetPWD(int nit);
    }
}
