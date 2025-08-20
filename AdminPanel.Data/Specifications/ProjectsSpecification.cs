
using AdminPanel.Data.Entities;

namespace AdminPanel.Data.Specifications
{
    public class ProjectsSpecification : BaseSpecification<Project>
    {
        public ProjectsSpecification(ProjectSpecParams projectParams)
            : base(p => 
                    (p.Name.ToLower().Contains(projectParams.Search.ToLower()) ||
                    (p.Description != null && p.Description.ToLower().Contains(projectParams.Search.ToLower())) ||
                    p.Status.ToString().Contains(projectParams.Search) ||
                    (p.Budget != null && p.Budget.ToString().Contains(projectParams.Search))) && 
                    (p.IsDeleted == false)
            )
        {
            AddOrderBy(x => x.Name);
            ApplyPaging(projectParams.PageSize * (projectParams.PageIndex - 1),
               projectParams.PageSize);

            if (projectParams.ColumnIndex != 0 && !string.IsNullOrEmpty(projectParams.Sort))
            {
                switch (projectParams.ColumnIndex)
                {
                    case 1: // Name
                        if (projectParams.Sort == "asc")
                            AddOrderBy(p => p.Name);
                        else
                            AddOrderByDescending(x => x.Name);
                        break;
                    case 2: // Description
                        if (projectParams.Sort == "asc")
                            AddOrderBy(p => p.Description);
                        else
                            AddOrderByDescending(x => x.Description);
                        break;
                    case 3: // Status
                        if (projectParams.Sort == "asc")
                            AddOrderBy(p => p.Status);
                        else
                            AddOrderByDescending(x => x.Status);
                        break;
                    case 4: // Start Date
                        if (projectParams.Sort == "asc")
                            AddOrderBy(p => p.StartDate);
                        else
                            AddOrderByDescending(x => x.StartDate);
                        break;
                    case 5: // End Date
                        if (projectParams.Sort == "asc")
                            AddOrderBy(p => p.EndDate);
                        else
                            AddOrderByDescending(x => x.EndDate);
                        break;
                    case 6: // Modified Date
                        if (projectParams.Sort == "asc")
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

        public ProjectsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Client);
        }
    }
}