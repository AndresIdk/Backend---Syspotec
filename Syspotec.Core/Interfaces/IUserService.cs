﻿using Syspotec.Core.DTOs;
using Syspotec.Core.entities;

namespace Syspotec.Core.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserDTO>> Get();
        public Task<bool> Post(User user);
        public Task<bool> Delete(int user_id);
        public Task<bool> Update(int user_id, User user);
        public Task<User> GetByID(int user_id);
        public Task<bool> GetPWD(LoginUserDTO user);
    }
}
