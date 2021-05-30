using System;
using System.Collections.Generic;
using System.Linq;

namespace Service1
{
    public class UserRepository : IUserRepository
    {
        private List<User> users;

        public UserRepository()
        {
            users = new List<User>{
                new User(Guid.Parse("e0504001-d73d-4d22-9eef-77c1e2a68314"),"Amin", "Ziagham", "Norway"),
                new User(Guid.Parse("bc03b020-a657-4c18-a4cd-045750ed5e7a"), "David", "Johansen", "United States")
            };
        }

        public void Add(User user)
        {
            users.Add(user);
        }

        public User Get(Guid id)
        {
            var user = users.Where(x=>x.Id.Equals(id));
            if (user == null)
                return new User();
            return user.FirstOrDefault();
        }

        public IEnumerable<User> GetAll()
        {
            return users.ToList();
        }
    }
}