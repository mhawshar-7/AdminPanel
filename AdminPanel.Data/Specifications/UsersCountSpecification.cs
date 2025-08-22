using AdminPanel.Data.Entities.Identity;

namespace AdminPanel.Data.Specifications
{
    public class UsersCountSpecification : BaseSpecification<User>
    {
        public UsersCountSpecification(UserSpecParams clientParams)
            : base(x => !x.IsDeleted && (
                     string.IsNullOrEmpty(clientParams.Search) ||
                     x.FirstName.ToLower().Contains(clientParams.Search) ||
                     x.LastName.ToLower().Contains(clientParams.Search) ||
                    (x.Email != null && x.Email.ToLower().Contains(clientParams.Search)) ||
                    (x.UserName != null && x.UserName.ToLower().Contains(clientParams.Search))))
        {
        }
    }
}
