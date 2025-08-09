using AdminPanel.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Data.Entities.Identity
{
    public class User : IdentityUser, ISoftDeletable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted { get; set; }

        public string GetDisplayName() => FirstName + " " + LastName;
    }


}
