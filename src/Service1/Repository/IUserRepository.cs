using System;
using System.Collections.Generic;

namespace Service1
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(Guid id);
        void Add(User user);
    }
}