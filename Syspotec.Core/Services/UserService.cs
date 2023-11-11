using Syspotec.Core.DTOs;
using Syspotec.Core.entities;
using Syspotec.Core.Interfaces;

namespace Syspotec.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<List<UserDTO>> Get()
        {
            return _userRepository.Get();
        }

        public Task<User> GetByID(int user_id)
        {
            return _userRepository.GetByID(user_id);
        }

        public Task<bool> Post(User user, string pwd_hash)
        {
            return _userRepository.Post(user, pwd_hash);
        }
        public Task<bool> Delete(int user_id)
        {
            return _userRepository.Delete(user_id);
        }
        public Task<bool> Update(int user_id, User user)
        {
            return _userRepository.Update(user_id, user);
        }
    }
}
