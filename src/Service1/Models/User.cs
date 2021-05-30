using System;

namespace Service1
{
    public class User
    {
        #region Properties
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        #endregion
        
        #region Controctors
        public User() {}

        public User(Guid id, string firstName, string lastName, string country)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Country = country;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
        #endregion
    }
}