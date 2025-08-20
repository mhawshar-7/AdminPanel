using AdminPanel.Data.Entities;
using AdminPanel.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Data.Specifications
{
    public class ProjectCountSpecification : BaseSpecification<Project>
    {
        public ProjectCountSpecification(ProjectSpecParams projectParams)
            : base(x =>
                    (x.Name.ToLower().Contains(projectParams.Search.ToLower()) ||
                    (x.Description != null && x.Description.ToLower().Contains(projectParams.Search.ToLower())) ||
                    x.Status.ToString().Contains(projectParams.Search) ||
                    (x.Budget != null && x.Budget.ToString().Contains(projectParams.Search))) &&
                    (x.IsDeleted == false) && (!projectParams.ClientId.HasValue || x.ClientId == projectParams.ClientId))
        {
        }
    }
}
