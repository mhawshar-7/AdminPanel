using AdminPanel.Data.Entities;
using AdminPanel.Data.Entities.Identity;

namespace AdminPanel.Data.Specifications
{
    public class UsersSpecification : BaseSpecification<User>
    {
        public UsersSpecification(UserSpecParams clientParams)
            : base(p => !p.IsDeleted && (
                     string.IsNullOrEmpty(clientParams.Search) ||
                     p.FirstName.ToLower().Contains(clientParams.Search) ||
                     p.LastName.ToLower().Contains(clientParams.Search) ||
                    (p.Email != null && p.Email.ToLower().Contains(clientParams.Search)) ||
                    (p.UserName != null && p.UserName.ToLower().Contains(clientParams.Search))))
        {
            ApplyPaging(clientParams.PageSize * (clientParams.PageIndex - 1), clientParams.PageSize);

            if (!string.IsNullOrEmpty(clientParams.Sort))
            {
                switch (clientParams.ColumnIndex)
                {
                    case 0: // FirstName
                        if (clientParams.Sort == "asc") AddOrderBy(p => p.FirstName); else AddOrderByDescending(x => x.FirstName);
                        break;
                    case 1: // LastName
                        if (clientParams.Sort == "asc") AddOrderBy(p => p.LastName); else AddOrderByDescending(x => x.LastName);
                        break;
                    case 2: // Email
                        if (clientParams.Sort == "asc") AddOrderBy(p => p.Email); else AddOrderByDescending(x => x.Email);
                        break;
                    case 3: // UserName
                        if (clientParams.Sort == "asc") AddOrderBy(p => p.UserName); else AddOrderByDescending(x => x.UserName);
                        break;
                    default:
                        AddOrderBy(n => n.FirstName);
                        break;
                }
            }
            else
            {
                AddOrderBy(n => n.FirstName);
            }
        }

        public UsersSpecification(string id) : base(x => x.Id == id && !x.IsDeleted)
        {
        }
    }
}