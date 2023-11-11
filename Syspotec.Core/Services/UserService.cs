using System.Security.Cryptography;
using System.Text;
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

        public Task<bool> Post(User user)
        {
            return _userRepository.Post(user);
        }
        public Task<bool> Delete(int user_id)
        {
            return _userRepository.Delete(user_id);
        }
        public Task<bool> Update(int user_id, User user)
        {
            return _userRepository.Update(user_id, user);
        }

        public async Task<bool> GetPWD(LoginUserDTO user)
        {
            string response = await _userRepository.GetPWD(user.nit);
            if (response != null)
            {
                var hashAlgorithm = SHA256.Create();
                var hash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(user.password));
                var pwd_hash = Convert.ToBase64String(hash);

                if (pwd_hash.CompareTo(response) == 0) { return true; }

                return false;
            }
            else
            {
                return false;
            }
            
        }
    }
}
