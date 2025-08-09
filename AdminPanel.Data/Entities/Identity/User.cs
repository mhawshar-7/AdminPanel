using AdminPanel.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Data.Entities.Identity
{
    public class User : IdentityUser, ISoftDeletable
    {
        public User(string firstName, string lastName, string email, string userName):base(userName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("message", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("message", nameof(lastName));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("message", nameof(email));
            }

            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted { get; set; }

        public string GetDisplayName() => FirstName + " " + LastName;
    }


}
