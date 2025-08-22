
using AdminPanel.Data.Entities;

namespace AdminPanel.Data.Specifications
{
    public class ClientsSpecification : BaseSpecification<Client>
    {
        public ClientsSpecification(ClientSpecParams clientParams)
            : base(p =>
                    (p.Name.ToLower().Contains(clientParams.Search.ToLower()) ||
                    (p.Email != null && p.Email.ToLower().Contains(clientParams.Search.ToLower())) ||
                    (p.Address != null && p.Address.ToLower().Contains(clientParams.Search.ToLower())) ||
                    (p.Phone != null && p.Phone.ToLower().Contains(clientParams.Search.ToLower()))) &&
                    (p.IsDeleted == false)
            )
        {
            ApplyPaging(clientParams.PageSize * (clientParams.PageIndex - 1),
               clientParams.PageSize);

            if (!string.IsNullOrEmpty(clientParams.Sort))
            {
                switch (clientParams.ColumnIndex)
                {
                    case 0: // Name
                        if (clientParams.Sort == "asc")
                            AddOrderBy(p => p.Name);
                        else
                            AddOrderByDescending(x => x.Name);
                        break;
                    case 1: // Description
                        if (clientParams.Sort == "asc")
                            AddOrderBy(p => p.Email);
                        else
                            AddOrderByDescending(x => x.Email);
                        break;
                    case 2: // Status
                        if (clientParams.Sort == "asc")
                            AddOrderBy(p => p.Address);
                        else
                            AddOrderByDescending(x => x.Address);
                        break;
                    case 3: // Start Date
                        if (clientParams.Sort == "asc")
                            AddOrderBy(p => p.Phone);
                        else
                            AddOrderByDescending(x => x.Phone);
                        break;
                    case 5: // Modified Date
                        if (clientParams.Sort == "asc")
                            AddOrderBy(p => p.ModifiedDate);
                        else
                            AddOrderByDescending(x => x.ModifiedDate);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public ClientsSpecification(int id) : base(x => x.Id == id)
        {
        }
    }
}