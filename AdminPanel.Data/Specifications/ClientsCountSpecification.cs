using AdminPanel.Data.Entities;
using AdminPanel.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Data.Specifications
{
    public class ClientsCountSpecification : BaseSpecification<Client>
    {
        public ClientsCountSpecification(ClientSpecParams clientParams)
            : base(x =>
                    (x.Name.ToLower().Contains(clientParams.Search.ToLower()) ||
                    (x.Email != null && x.Email.ToLower().Contains(clientParams.Search.ToLower())) ||
                    (x.Address != null && x.Address.ToLower().Contains(clientParams.Search.ToLower())) ||
                    (x.Phone != null && x.Phone.ToLower().Contains(clientParams.Search.ToLower()))) &&
                    (x.IsDeleted == false))
        {
        }
    }
}
