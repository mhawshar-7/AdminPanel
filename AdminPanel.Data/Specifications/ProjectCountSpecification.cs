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
            : base(x => (x.IsDeleted == false) && (!projectParams.ClientId.HasValue || x.ClientId == projectParams.ClientId))
        {
            if (!string.IsNullOrWhiteSpace(projectParams.Search))
            {
                var raw = projectParams.Search.Trim();

                if (DateTime.TryParse(raw, out var searchDate))
                {
                    AddCriteria(p =>
                        p.StartDate.Date == searchDate.Date ||
                        (p.EndDate.HasValue && p.EndDate.Value.Date == searchDate.Date));
                }
                else
                {
                    AddCriteria(p => p.Name.ToLower().Contains(raw.ToLower()) ||
                    (p.Description != null && p.Description.ToLower().Contains(raw.ToLower())) ||
                    p.Status.ToString().Contains(projectParams.Search) ||
                    (p.Budget != null && p.Budget.ToString().Contains(raw)));
                }
            }
        }
    }
}
